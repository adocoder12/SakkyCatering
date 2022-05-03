namespace SakkyCatering
{
    partial class taulutYhdessä
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.weekCombo = new SakkyCatering.Combo();
            this.vuosiCombo = new SakkyCatering.Combo();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(190)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.weekCombo);
            this.panel1.Controls.Add(this.vuosiCombo);
            this.panel1.Controls.Add(this.cartesianChart1);
            this.panel1.Location = new System.Drawing.Point(12, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 550);
            this.panel1.TabIndex = 2;
            // 
            // weekCombo
            // 
            this.weekCombo.AccessibleDescription = "";
            this.weekCombo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.weekCombo.BorderColor = System.Drawing.Color.Black;
            this.weekCombo.BorderSize = 2;
            this.weekCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.weekCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.weekCombo.ForeColor = System.Drawing.Color.DimGray;
            this.weekCombo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(190)))), ((int)(((byte)(0)))));
            this.weekCombo.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.weekCombo.ListTextColor = System.Drawing.Color.DimGray;
            this.weekCombo.Location = new System.Drawing.Point(715, 35);
            this.weekCombo.MinimumSize = new System.Drawing.Size(100, 30);
            this.weekCombo.Name = "weekCombo";
            this.weekCombo.Padding = new System.Windows.Forms.Padding(2);
            this.weekCombo.Size = new System.Drawing.Size(100, 30);
            this.weekCombo.TabIndex = 2;
            this.weekCombo.Texts = "Viikoa";
            // 
            // vuosiCombo
            // 
            this.vuosiCombo.AccessibleDescription = "";
            this.vuosiCombo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.vuosiCombo.BorderColor = System.Drawing.Color.Black;
            this.vuosiCombo.BorderSize = 2;
            this.vuosiCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.vuosiCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.vuosiCombo.ForeColor = System.Drawing.Color.DimGray;
            this.vuosiCombo.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(190)))), ((int)(((byte)(0)))));
            this.vuosiCombo.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.vuosiCombo.ListTextColor = System.Drawing.Color.DimGray;
            this.vuosiCombo.Location = new System.Drawing.Point(821, 34);
            this.vuosiCombo.MinimumSize = new System.Drawing.Size(100, 30);
            this.vuosiCombo.Name = "vuosiCombo";
            this.vuosiCombo.Padding = new System.Windows.Forms.Padding(2);
            this.vuosiCombo.Size = new System.Drawing.Size(111, 30);
            this.vuosiCombo.TabIndex = 2;
            this.vuosiCombo.Texts = "Vuosi";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(12)))));
            this.cartesianChart1.Location = new System.Drawing.Point(17, 71);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(917, 451);
            this.cartesianChart1.TabIndex = 1;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(712, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "VIIKOA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(818, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "VUOSI";
            // 
            // taulutYhdessä
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 757);
            this.Controls.Add(this.panel1);
            this.Name = "taulutYhdessä";
            this.Text = "taulutYhdessä";
            this.Load += new System.EventHandler(this.taulutYhdessä_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Combo weekCombo;
        private Combo vuosiCombo;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}