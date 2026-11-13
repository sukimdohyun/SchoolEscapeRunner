namespace SchoolEscapeRunnerGame
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageListJump = new System.Windows.Forms.ImageList(this.components);
            this.pictureBoxPlayer = new System.Windows.Forms.PictureBox();
            this.imageListCrouch = new System.Windows.Forms.ImageList(this.components);
            this.imageListRun = new System.Windows.Forms.ImageList(this.components);
            this.imageListOther = new System.Windows.Forms.ImageList(this.components);
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListJump
            // 
            this.imageListJump.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListJump.ImageStream")));
            this.imageListJump.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListJump.Images.SetKeyName(0, "player_jump.png");
            // 
            // pictureBoxPlayer
            // 
            this.pictureBoxPlayer.Location = new System.Drawing.Point(116, 545);
            this.pictureBoxPlayer.Name = "pictureBoxPlayer";
            this.pictureBoxPlayer.Size = new System.Drawing.Size(100, 50);
            this.pictureBoxPlayer.TabIndex = 0;
            this.pictureBoxPlayer.TabStop = false;
            this.pictureBoxPlayer.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // imageListCrouch
            // 
            this.imageListCrouch.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListCrouch.ImageStream")));
            this.imageListCrouch.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListCrouch.Images.SetKeyName(0, "player_crouch2.png");
            this.imageListCrouch.Images.SetKeyName(1, "player_crouch1.png");
            // 
            // imageListRun
            // 
            this.imageListRun.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRun.ImageStream")));
            this.imageListRun.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListRun.Images.SetKeyName(0, "player_run1.png");
            this.imageListRun.Images.SetKeyName(1, "Player_run2.png");
            // 
            // imageListOther
            // 
            this.imageListOther.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListOther.ImageSize = new System.Drawing.Size(96, 96);
            this.imageListOther.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timerGame
            // 
            this.timerGame.Interval = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1254, 681);
            this.Controls.Add(this.pictureBoxPlayer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageListJump;
        private System.Windows.Forms.PictureBox pictureBoxPlayer;
        private System.Windows.Forms.ImageList imageListCrouch;
        private System.Windows.Forms.ImageList imageListRun;
        private System.Windows.Forms.ImageList imageListOther;
        private System.Windows.Forms.Timer timerGame;
    }
}

