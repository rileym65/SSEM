namespace ManchesterBaby
{
    partial class AssemblerForm
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
            this.sourceCode = new System.Windows.Forms.TextBox();
            this.assembleButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.disassembleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sourceCode
            // 
            this.sourceCode.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sourceCode.Location = new System.Drawing.Point(37, 16);
            this.sourceCode.Multiline = true;
            this.sourceCode.Name = "sourceCode";
            this.sourceCode.Size = new System.Drawing.Size(296, 456);
            this.sourceCode.TabIndex = 0;
            // 
            // assembleButton
            // 
            this.assembleButton.Location = new System.Drawing.Point(378, 16);
            this.assembleButton.Name = "assembleButton";
            this.assembleButton.Size = new System.Drawing.Size(75, 23);
            this.assembleButton.TabIndex = 1;
            this.assembleButton.Text = "Assemble";
            this.assembleButton.UseVisualStyleBackColor = true;
            this.assembleButton.Click += new System.EventHandler(this.assembleButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(6, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(25, 456);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0\r\n1\r\n2\r\n3\r\n4\r\n5\r\n6\r\n7\r\n8\r\n9\r\n10\r\n11\r\n12\r\n13\r\n14\r\n15\r\n16\r\n17\r\n18\r\n19\r\n20\r\n21\r\n22\r" +
    "\n23\r\n24\r\n25\r\n26\r\n27\r\n28\r\n29\r\n30\r\n31";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(378, 449);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // disassembleButton
            // 
            this.disassembleButton.Location = new System.Drawing.Point(378, 81);
            this.disassembleButton.Name = "disassembleButton";
            this.disassembleButton.Size = new System.Drawing.Size(75, 23);
            this.disassembleButton.TabIndex = 4;
            this.disassembleButton.Text = "Disassemble";
            this.disassembleButton.UseVisualStyleBackColor = true;
            this.disassembleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // AssemblerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(465, 484);
            this.Controls.Add(this.disassembleButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.assembleButton);
            this.Controls.Add(this.sourceCode);
            this.Name = "AssemblerForm";
            this.Text = "AssemblerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sourceCode;
        private System.Windows.Forms.Button assembleButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button disassembleButton;
    }
}