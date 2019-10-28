using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ManchesterBaby
{
    public partial class AssemblerForm : Form
    {
        int[] mem;

        public AssemblerForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
        }

        public void setMem(int[] m)
        {
            mem = m;
        }

        public void setSource(String s)
        {
            sourceCode.Text = s;
            sourceCode.SelectionStart = sourceCode.Text.Length;
        }

        public String getSource()
        {
            return sourceCode.Text;
        }

        private void assembleButton_Click(object sender, EventArgs e)
        {
            int i;
            String line;
            for (i = 0; i < 32; i++) mem[i] = 0;
            for (i = 0; i < sourceCode.Lines.Count(); i++)
            {
                line = (sourceCode.Lines[i]).Trim().ToUpper();
                if (line.StartsWith("NUM"))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4));
                }
                else if (line.StartsWith("JMP"))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4)) + (0 << 13);
                }
                else if (line.StartsWith("JRP"))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4)) + (1 << 13);
                }
                else if (line.StartsWith("LDN"))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4)) + (2 << 13);
                }
                else if (line.StartsWith("STO "))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4)) + (3 << 13);
                }
                else if (line.StartsWith("SUB"))
                {
                    mem[i] = Convert.ToInt32(line.Substring(4)) + (4 << 13);
                }
                else if (line.StartsWith("CMP"))
                {
                    mem[i] = (6 << 13);
                }
                else if (line.StartsWith("STP"))
                {
                    mem[i] = (7 << 13);
                }
                else if (line.StartsWith("STOP"))
                {
                    mem[i] = (7 << 13);
                }
                else MessageBox.Show("Invalid instruction: " + line);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            sourceCode.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            sourceCode.Text = "";
            for (i = 0; i < 32; i++)
            {
                if (i != 0) sourceCode.AppendText("\r\n");
                if ((mem[i] & 0xffff1fe0) != 0)
                {
                    sourceCode.AppendText("NUM " + mem[i].ToString());
                }
                else
                {
                    switch ((mem[i] >> 13) & 0x7)
                    {
                        case 0: sourceCode.AppendText("JMP " + (mem[i] & 0x1f).ToString()); break;
                        case 1: sourceCode.AppendText("JRP " + (mem[i] & 0x1f).ToString()); break;
                        case 2: sourceCode.AppendText("LDN " + (mem[i] & 0x1f).ToString()); break;
                        case 3: sourceCode.AppendText("STO " + (mem[i] & 0x1f).ToString()); break;
                        case 4: sourceCode.AppendText("SUB " + (mem[i] & 0x1f).ToString()); break;
                        case 5: sourceCode.AppendText("SUB " + (mem[i] & 0x1f).ToString()); break;
                        case 6: sourceCode.AppendText("CMP"); break;
                        case 7: sourceCode.AppendText("STP"); break;
                    }
                }
            }
        }

    }
}
