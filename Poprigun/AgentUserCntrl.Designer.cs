
namespace Poprigun
{
    partial class AgentUserCntrl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TypeAgentLbl = new System.Windows.Forms.Label();
            this.phoneNumLbl = new System.Windows.Forms.Label();
            this.P = new System.Windows.Forms.Label();
            this.priorityLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Poprigun.Properties.Resources.picture;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // TypeAgentLbl
            // 
            this.TypeAgentLbl.AutoSize = true;
            this.TypeAgentLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.TypeAgentLbl.Location = new System.Drawing.Point(169, 33);
            this.TypeAgentLbl.Name = "TypeAgentLbl";
            this.TypeAgentLbl.Size = new System.Drawing.Size(195, 18);
            this.TypeAgentLbl.TabIndex = 1;
            this.TypeAgentLbl.Text = "Тип | наименование агента";
            // 
            // phoneNumLbl
            // 
            this.phoneNumLbl.AutoSize = true;
            this.phoneNumLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.phoneNumLbl.Location = new System.Drawing.Point(169, 51);
            this.phoneNumLbl.Name = "phoneNumLbl";
            this.phoneNumLbl.Size = new System.Drawing.Size(129, 18);
            this.phoneNumLbl.TabIndex = 3;
            this.phoneNumLbl.Text = "Номер телефона";
            // 
            // P
            // 
            this.P.AutoSize = true;
            this.P.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.P.Location = new System.Drawing.Point(169, 69);
            this.P.Name = "P";
            this.P.Size = new System.Drawing.Size(90, 18);
            this.P.TabIndex = 4;
            this.P.Text = "Приоритет: ";
            // 
            // priorityLbl
            // 
            this.priorityLbl.AutoSize = true;
            this.priorityLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.priorityLbl.Location = new System.Drawing.Point(254, 69);
            this.priorityLbl.Name = "priorityLbl";
            this.priorityLbl.Size = new System.Drawing.Size(52, 18);
            this.priorityLbl.TabIndex = 6;
            this.priorityLbl.Text = "Приор";
            // 
            // AgentUserCntrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.priorityLbl);
            this.Controls.Add(this.P);
            this.Controls.Add(this.phoneNumLbl);
            this.Controls.Add(this.TypeAgentLbl);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AgentUserCntrl";
            this.Size = new System.Drawing.Size(693, 150);
            this.Load += new System.EventHandler(this.AgentUserCntrl_Load);
            this.Click += new System.EventHandler(this.AgentUserCntrl_Click);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AgentUserCntrl_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label TypeAgentLbl;
        private System.Windows.Forms.Label phoneNumLbl;
        private System.Windows.Forms.Label P;
        private System.Windows.Forms.Label priorityLbl;
    }
}
