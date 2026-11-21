using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SchoolEscapeRunnerGame
{
    public partial class Form1 : Form
    {
        // 캐릭터 상태 목록
        enum PlayerState
        {
            Run,
            Jump,
            Crouch,
            Landing
        }

        private PlayerState state = PlayerState.Run;

        // 애니메이션 틱 -> 각 애니메이션의 프레임 결정
        private int animationTick = 0;
        private int jumpTick = 0;
        private int landingTick = 0;

        // 점프 최대 높이 및 속도 설정
        private float jumpHeight = 300f; // 최대 점프 높이
        private float gravity = 1.7f;   // 중력가속도
        private float jumpSpeed;        // 점프 시작 속도

        private int riseFrames;
        private int ground;

        private bool shiftHeld = false;

        // 장애물 이동 속도 및 회피 횟수 (점수 대신 속도 증가용)
        private int gameSpeed = 6;
        private int avoidCount = 0;
        private bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true; // 깜빡임 방지
            this.KeyPreview = true;

            ground = pictureBoxPlayer.Top; // 플레이어가 점프 이후 착지할 y좌표 설정

            pictureBoxPlayer.Size = new Size(96, 96); // 플레이어 사이즈 설정
            pictureBoxPlayer.SizeMode =
                PictureBoxSizeMode.Zoom; // 플레이어 이미지 박스에 맞게끔 설정

            // 타이머 설정
            timerGame.Interval = 16;
            timerGame.Tick += TimerGame_Tick;

            ShowFrame(imageListRun, 0);

            timerGame.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // 키 입력
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (isGameOver) return;

            if (e.KeyCode == Keys.ShiftKey)
            {
                shiftHeld = true;

                if (state == PlayerState.Run)
                {
                    state = PlayerState.Crouch;
                    animationTick = 0;
                }
            }

            if (e.KeyCode == Keys.Space &&
                state == PlayerState.Run)
            {
                StartJump();
                e.SuppressKeyPress = true;
            }
        }

        // 키를 놓았을 때
        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                shiftHeld = false;

                if (state == PlayerState.Crouch)
                {
                    state = PlayerState.Run;
                    animationTick = 0;
                }
            }
        }

        // 점프 시작
        private void StartJump()
        {
            state = PlayerState.Jump;
            jumpTick = 0;

            // 중력과 최대 높이로 점프 시작 속도 계산
            jumpSpeed = (float)Math.Sqrt(
                2f * gravity * jumpHeight
            );
        }

        // 이미지 리스트의 프레임 출력
        private void ShowFrame(ImageList list, int index)
        {
            index = Math.Max(
                0,
                Math.Min(index, list.Images.Count - 1)
            );

            pictureBoxPlayer.Image = list.Images[index];
        }

        // 게임 타이머
        private void TimerGame_Tick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            switch (state)
            {
                case PlayerState.Run:
                    RunAnimation();
                    break;

                case PlayerState.Crouch:
                    CrouchAnimation();
                    break;

                case PlayerState.Jump:
                    JumpAnimation();
                    break;

                case PlayerState.Landing:
                    LandingAnimation();
                    break;
            }

            // 장애물 이동, 속도 증가, 충돌 판정
            UpdateObstacleAndSpeed();
            CheckCollision();
        }

        // 장애물 이동 및 속도 증가 (점수 출력 없이 회피할수록 점점 빨라짐)
        private void UpdateObstacleAndSpeed()
        {
            Control obstacle = this.Controls.Find("pbObstacle", true).FirstOrDefault()
                            ?? this.Controls.Find("pictureBoxObstacle", true).FirstOrDefault();

            if (obstacle == null) return;

            obstacle.Left -= gameSpeed;

            if (obstacle.Right < 0)
            {
                obstacle.Left = this.ClientSize.Width + 50;
                avoidCount++;

                if (gameSpeed < 20)
                {
                    gameSpeed = 6 + (avoidCount / 2);
                }
            }
        }

        // 충돌 판정 (.IntersectsWith)
        private void CheckCollision()
        {
            Control obstacle = this.Controls.Find("pbObstacle", true).FirstOrDefault()
                            ?? this.Controls.Find("pictureBoxObstacle", true).FirstOrDefault();

            if (obstacle == null || !obstacle.Visible) return;

            Rectangle playerBox = new Rectangle(
                pictureBoxPlayer.Left + 8,
                pictureBoxPlayer.Top + 8,
                pictureBoxPlayer.Width - 16,
                pictureBoxPlayer.Height - 16
            );

            Rectangle obstacleBox = new Rectangle(
                obstacle.Left + 5,
                obstacle.Top + 5,
                obstacle.Width - 10,
                obstacle.Height - 10
            );

            if (playerBox.IntersectsWith(obstacleBox))
            {
                isGameOver = true;
                timerGame.Stop();

                try
                {
                    pictureBoxPlayer.Image = Properties.Resources.player_collision;
                }
                catch
                {
                    // 리소스 이름이 다를 경우 기존 이미지 유지
                }

                MessageBox.Show("장애물에 부딪혔습니다! 게임 오버", "School Escape");
            }
        }

        // 달리기 애니메이션
        private void RunAnimation()
        {
            if (imageListRun.Images.Count == 0)
                return;

            int frame =
                (animationTick / 5) %
                imageListRun.Images.Count;

            ShowFrame(imageListRun, frame);

            animationTick++;
        }

        // 숙이기 애니메이션
        private void CrouchAnimation()
        {
            if (imageListCrouch.Images.Count == 0)
                return;

            int frame =
                (animationTick / 5) %
                imageListCrouch.Images.Count;

            ShowFrame(imageListCrouch, frame);

            animationTick++;
        }

        // 점프 애니메이션 및 위치 이동
        private void JumpAnimation()
        {
            jumpTick++;

            // 점프 준비 동작
            if (jumpTick <= 2)
            {
                ShowFrame(imageListJump, jumpTick - 1);
                return;
            }

            float t = jumpTick - 2;

            // 최고점 도달 시간
            float riseTime = jumpSpeed / gravity;

            // 전체 점프 시간
            float totalTime = riseTime * 2f;

            // 착지
            if (t >= totalTime)
            {
                pictureBoxPlayer.Top = ground;
                state = PlayerState.Landing;
                landingTick = 0;

                ShowFrame(imageListJump, 14);
                return;
            }

            // 점프 높이 계산
            float height =
                jumpSpeed * t - 0.5f * gravity * t * t;

            // 최대 높이 제한
            height = Math.Max(0f, Math.Min(jumpHeight, height));

            pictureBoxPlayer.Top =
                ground - (int)Math.Round(height);

            // 상승 애니메이션 (2~5번)
            if (t < riseTime - 1f)
            {
                int frame = 2 + Math.Min(
                    3, (int)(t / riseTime * 4f)
                );

                ShowFrame(imageListJump, frame);
            }
            // 최고점 애니메이션 (6~7번)
            else if (t <= riseTime + 1f)
            {
                ShowFrame(imageListJump, t <= riseTime ? 6 : 7);
            }
            // 하강 애니메이션 (8~13번)
            else
            {
                float progress = (t - riseTime) / riseTime;

                int frame = 8 + Math.Min(
                    5, (int)(progress * 6f)
                );

                ShowFrame(imageListJump, frame);
            }
        }

        // 착지 애니메이션 실행
        private void LandingAnimation()
        {
            landingTick++;

            if (landingTick == 1)
                ShowFrame(imageListJump, 14);
            else
                ShowFrame(imageListJump, 15);

            if (landingTick >= 2)
            {
                state = shiftHeld
                    ? PlayerState.Crouch
                    : PlayerState.Run