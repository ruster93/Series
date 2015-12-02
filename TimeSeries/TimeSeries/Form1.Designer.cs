namespace TimeSeries
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Update = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.Zoom_minus = new System.Windows.Forms.Button();
            this.Zoom_plus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Update
            // 
            this.Update.Location = new System.Drawing.Point(591, 9);
            this.Update.Name = "Update";
            this.Update.Size = new System.Drawing.Size(107, 23);
            this.Update.TabIndex = 0;
            this.Update.Text = "Обновить данные";
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.IsShowPointValues = false;
            this.zedGraphControl1.Location = new System.Drawing.Point(21, 36);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.PointValueFormat = "G";
            this.zedGraphControl1.Size = new System.Drawing.Size(675, 305);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Zoom_minus
            // 
            this.Zoom_minus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Zoom_minus.Location = new System.Drawing.Point(521, 9);
            this.Zoom_minus.Name = "Zoom_minus";
            this.Zoom_minus.Size = new System.Drawing.Size(23, 23);
            this.Zoom_minus.TabIndex = 3;
            this.Zoom_minus.Text = "-";
            this.Zoom_minus.UseVisualStyleBackColor = true;
            this.Zoom_minus.Click += new System.EventHandler(this.Zoom_minus_Click);
            // 
            // Zoom_plus
            // 
            this.Zoom_plus.Location = new System.Drawing.Point(550, 9);
            this.Zoom_plus.Name = "Zoom_plus";
            this.Zoom_plus.Size = new System.Drawing.Size(23, 23);
            this.Zoom_plus.TabIndex = 4;
            this.Zoom_plus.Text = "+";
            this.Zoom_plus.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 353);
            this.Controls.Add(this.Zoom_plus);
            this.Controls.Add(this.Zoom_minus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.Update);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Update;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Zoom_minus;
        private System.Windows.Forms.Button Zoom_plus;
    }
}

