namespace AloneAIOR
{
    partial class AloneAIO
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AloneAIO));
            this.TalesPID = new System.Windows.Forms.Label();
            this.TalesState = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.Testing = new System.Windows.Forms.Button();
            this.AloneProcessException = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OtherSystemException = new System.Windows.Forms.Label();
            this.InputSystemException = new System.Windows.Forms.Label();
            this.ImageSystemException = new System.Windows.Forms.Label();
            this.Debug = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TalesPID
            // 
            this.TalesPID.AutoSize = true;
            this.TalesPID.Location = new System.Drawing.Point(20, 36);
            this.TalesPID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TalesPID.Name = "TalesPID";
            this.TalesPID.Size = new System.Drawing.Size(96, 24);
            this.TalesPID.TabIndex = 1;
            this.TalesPID.Text = "TalesPID";
            // 
            // TalesState
            // 
            this.TalesState.AutoSize = true;
            this.TalesState.Location = new System.Drawing.Point(9, 12);
            this.TalesState.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.TalesState.Name = "TalesState";
            this.TalesState.Size = new System.Drawing.Size(104, 24);
            this.TalesState.TabIndex = 2;
            this.TalesState.Text = "AloneAIO";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(436, 1);
            this.Exit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(108, 44);
            this.Exit.TabIndex = 3;
            this.Exit.Text = "Close";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Testing
            // 
            this.Testing.Location = new System.Drawing.Point(327, 2);
            this.Testing.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Testing.Name = "Testing";
            this.Testing.Size = new System.Drawing.Size(106, 44);
            this.Testing.TabIndex = 4;
            this.Testing.Text = "Test";
            this.Testing.UseVisualStyleBackColor = true;
            this.Testing.Click += new System.EventHandler(this.testing_Click);
            // 
            // AloneProcessException
            // 
            this.AloneProcessException.AutoSize = true;
            this.AloneProcessException.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.AloneProcessException.Location = new System.Drawing.Point(20, 82);
            this.AloneProcessException.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.AloneProcessException.Name = "AloneProcessException";
            this.AloneProcessException.Size = new System.Drawing.Size(224, 24);
            this.AloneProcessException.TabIndex = 3;
            this.AloneProcessException.Text = "AloneProcessException";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TalesPID);
            this.groupBox2.Controls.Add(this.OtherSystemException);
            this.groupBox2.Controls.Add(this.InputSystemException);
            this.groupBox2.Controls.Add(this.ImageSystemException);
            this.groupBox2.Controls.Add(this.AloneProcessException);
            this.groupBox2.Location = new System.Drawing.Point(4, 178);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(332, 284);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // OtherSystemException
            // 
            this.OtherSystemException.AutoSize = true;
            this.OtherSystemException.Location = new System.Drawing.Point(20, 244);
            this.OtherSystemException.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.OtherSystemException.Name = "OtherSystemException";
            this.OtherSystemException.Size = new System.Drawing.Size(218, 24);
            this.OtherSystemException.TabIndex = 6;
            this.OtherSystemException.Text = "OtherSystemException";
            // 
            // InputSystemException
            // 
            this.InputSystemException.AutoSize = true;
            this.InputSystemException.Location = new System.Drawing.Point(20, 186);
            this.InputSystemException.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.InputSystemException.Name = "InputSystemException";
            this.InputSystemException.Size = new System.Drawing.Size(214, 24);
            this.InputSystemException.TabIndex = 5;
            this.InputSystemException.Text = "InputSystemException";
            // 
            // ImageSystemException
            // 
            this.ImageSystemException.AutoSize = true;
            this.ImageSystemException.Location = new System.Drawing.Point(20, 132);
            this.ImageSystemException.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ImageSystemException.Name = "ImageSystemException";
            this.ImageSystemException.Size = new System.Drawing.Size(224, 24);
            this.ImageSystemException.TabIndex = 4;
            this.ImageSystemException.Text = "ImageSystemException";
            // 
            // Debug
            // 
            this.Debug.AutoSize = true;
            this.Debug.Location = new System.Drawing.Point(9, 58);
            this.Debug.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(69, 24);
            this.Debug.TabIndex = 6;
            this.Debug.Text = "Debug";
            // 
            // AloneAIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(550, 98);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.TalesState);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Testing);
            this.Controls.Add(this.Exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AloneAIO";
            this.Opacity = 0.8D;
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AloneAIO";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AloneAIO_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label TalesPID;
        private System.Windows.Forms.Button Testing;
        private System.Windows.Forms.Label AloneProcessException;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ImageSystemException;
        private System.Windows.Forms.Label InputSystemException;
        private System.Windows.Forms.Label OtherSystemException;
        public System.Windows.Forms.Label TalesState;
        private System.Windows.Forms.Button Exit;
        public System.Windows.Forms.Label Debug;
    }
}

