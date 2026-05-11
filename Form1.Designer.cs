namespace ex_2
{
    partial class Form1
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
            cmbLanguages = new ComboBox();
            btnTranslate = new Button();
            lblResult = new RichTextBox();
            txtInput = new RichTextBox();
            SuspendLayout();
            // 
            // cmbLanguages
            // 
            cmbLanguages.FormattingEnabled = true;
            cmbLanguages.Location = new Point(12, 160);
            cmbLanguages.Name = "cmbLanguages";
            cmbLanguages.Size = new Size(223, 28);
            cmbLanguages.TabIndex = 1;
            // 
            // btnTranslate
            // 
            btnTranslate.BackColor = SystemColors.ActiveCaption;
            btnTranslate.Location = new Point(241, 159);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(150, 29);
            btnTranslate.TabIndex = 2;
            btnTranslate.Text = "translate";
            btnTranslate.UseVisualStyleBackColor = false;
            // 
            // lblResult
            // 
            lblResult.Location = new Point(12, 194);
            lblResult.Name = "lblResult";
            lblResult.Size = new Size(776, 244);
            lblResult.TabIndex = 3;
            lblResult.Text = "";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(12, 25);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(776, 120);
            txtInput.TabIndex = 4;
            txtInput.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtInput);
            Controls.Add(lblResult);
            Controls.Add(btnTranslate);
            Controls.Add(cmbLanguages);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cmbLanguages;
        private Button btnTranslate;
        private RichTextBox lblResult;
        private RichTextBox txtInput;
    }
}
