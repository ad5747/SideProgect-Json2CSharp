namespace Json2CSharp
{
    partial class Form1
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
            this.JsonText = new System.Windows.Forms.TextBox();
            this.ClassText = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JsonText
            // 
            this.JsonText.Location = new System.Drawing.Point(27, 30);
            this.JsonText.Multiline = true;
            this.JsonText.Name = "JsonText";
            this.JsonText.Size = new System.Drawing.Size(286, 408);
            this.JsonText.TabIndex = 0;
            // 
            // ClassText
            // 
            this.ClassText.Location = new System.Drawing.Point(451, 30);
            this.ClassText.Multiline = true;
            this.ClassText.Name = "ClassText";
            this.ClassText.Size = new System.Drawing.Size(277, 408);
            this.ClassText.TabIndex = 1;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(344, 172);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.ClassText);
            this.Controls.Add(this.JsonText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox JsonText;
        private System.Windows.Forms.TextBox ClassText;
        private System.Windows.Forms.Button btnConvert;
    }
}

