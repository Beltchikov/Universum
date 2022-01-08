namespace UniversumUi
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSymbols = new System.Windows.Forms.TextBox();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSymbols
            // 
            this.txtSymbols.Location = new System.Drawing.Point(22, 48);
            this.txtSymbols.Multiline = true;
            this.txtSymbols.Name = "txtSymbols";
            this.txtSymbols.Size = new System.Drawing.Size(218, 713);
            this.txtSymbols.TabIndex = 0;
            // 
            // txtResults
            // 
            this.txtResults.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.RecentlyUsedList;
            this.txtResults.Location = new System.Drawing.Point(309, 48);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.Size = new System.Drawing.Size(816, 713);
            this.txtResults.TabIndex = 1;
            // 
            // btGo
            // 
            this.btGo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btGo.Location = new System.Drawing.Point(245, 345);
            this.btGo.Name = "btGo";
            this.btGo.Size = new System.Drawing.Size(60, 63);
            this.btGo.TabIndex = 2;
            this.btGo.Text = ">>";
            this.btGo.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 773);
            this.Controls.Add(this.btGo);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.txtSymbols);
            this.Name = "MainForm";
            this.Text = "Universum UI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSymbols;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btGo;
    }
}
