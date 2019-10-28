using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ManchesterBaby
{
    public partial class MainForm : Form
    {
        private Cpu baby;
        private int actionLine;
        private uint writeValue;
        private int[] display;
        private Boolean keyPressed;
        private String sourceCode;
        private Boolean lastStopped;

        public MainForm()
        {
            int[] mem;
            int i;
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            writeValue = 0;
            baby = new Cpu();
            display = new int[32];
            mem = baby.getMem();
            baby.setStatLines(0xe01f);
            store.Image = new Bitmap(320, 320);
            for (i = 0; i < 32; i++)
            {
                display[i] = mem[i];
                displayRow(i);
            }
            keyPressed = false;
            writeValue = 0;
            sourceCode = "";
            lastStopped = false;
            stopLight.BackColor = Color.Black;
        }

        private String toBinary(int n)
        {
            String ret;
            int i;
            ret = "";
            for (i = 0; i < 32; i++)
            {
                if ((n & 1) == 1) ret += "*"; else ret += ".";
                n >>= 1;
            }
            return ret;
        }

        private void updateDisplays()
        {
            int i,j;
            int c, p;
            int[] mem;
            if (dispS.Checked)
            {
                mem = baby.getMem();
                for (i = 0; i < 32; i++)
                    if (mem[i] != display[i])
                    {
                        display[i] = mem[i];
                        displayRow(i);
                    }
            }
            if (dispA.Checked)
            {
                j = baby.getAcc();
                for (i = 0; i < 32; i++)
                    if (display[i] != j )
                {
                    display[i] = j;
                    displayRow(i);
                }
            }
            if (dispC.Checked)
            {
                c = baby.getCi();
                p = baby.getPi();
                if (csOff.Checked) p = c;
                for (i = 0; i < 16; i++)
                {
                    if (display[i * 2] != c)
                    {
                        display[i * 2] = c;
                        displayRow(i * 2);
                    }
                    if (display[i * 2 + 1] != p)
                    {
                        display[i * 2 + 1] = p;
                        displayRow(i * 2 + 1);
                    }
                }
            }
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateDisplays();
        }

        private void setActionLine()
        {
            actionLine = baby.getStatLines() & 0x1f;
        }

        private void typewriter_MouseDown(int tag)
        {
            if (tag > 31) return;
            setActionLine();
            writeValue = (uint)(1 << tag);
            keyPressed = true;
        }

        private void mouseDown(object sender, MouseEventArgs e)
        {
            typewriter_MouseDown(Convert.ToInt32(((Button)sender).Tag));
        }

        private void mouseUp(object sender, MouseEventArgs e)
        {
            keyPressed = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i;
            int[] mem;
            int current;
            if (keyPressed)
            {
                mem = baby.getMem();
                current = mem[actionLine];
                if (writeSwitch.Checked) mem[actionLine] |= (int)writeValue;
                else mem[actionLine] &= (int)(~writeValue);
                if (mem[actionLine] != current) updateDisplays();
            }
            if (kccOn.Checked)
            {
                baby.reset();
            }

            if (csOn.Checked)
            {
                for (i = 0; i < 12; i++)
                {
                    if (baby.getStopped() == false)
                    {
                        if (eraseSwitch.Checked)
                        {
                            mem = baby.getMem();
                            mem[(baby.getCi() + 1) & 0x1f] = 0;
                        }
                        baby.step();
                        updateDisplays();
                        actionLine = baby.getCi() & 0x1f;
                    }
                }
            }
            if (baby.getStopped() != lastStopped)
            {
                lastStopped = baby.getStopped();
                stopLight.BackColor = (lastStopped) ? Color.Red : Color.Black;
            }
        }

        private void kspOn_MouseUp(object sender, MouseEventArgs e)
        {
            kspOff.Checked = true;
        }

        private void kspOn_MouseDown(object sender, MouseEventArgs e)
        {
            kspOn.Checked = true;
            baby.setStopped(false);
            baby.step();
            updateDisplays();
        }

        private void kccOn_MouseUp(object sender, MouseEventArgs e)
        {
            kccOff.Checked = true;
        }

        private void kccOn_MouseDown(object sender, MouseEventArgs e)
        {
            kccOn.Checked = true;
            baby.reset();
            updateDisplays();
        }

        private void klcOn_MouseDown(object sender, MouseEventArgs e)
        {
            int[] mem;
            int current;
            mem = baby.getMem();
            klcOn.Checked = true;
            setActionLine();
            current = mem[actionLine];
            mem[actionLine] = 0;
            if (current != mem[actionLine]) updateDisplays();
        }

        private void klcOn_MouseUp(object sender, MouseEventArgs e)
        {
            klcOff.Checked = true;
        }

        private void kscOn_MouseUp(object sender, MouseEventArgs e)
        {
            kscOff.Checked = true;
        }

        private void kscOn_MouseDown(object sender, MouseEventArgs e)
        {
            int i;
            int[] mem;
            kscOn.Checked = true;
            mem = baby.getMem();
            for (i=0; i<32; i++) mem[i] = 0;
            updateDisplays();
        }

        private void displayRow(int n)
        {
            Graphics gc;
            Pen pen;
            int x, y;
            int v;
            gc = Graphics.FromImage(store.Image);
            pen = new Pen(Color.Lime);
            y = n * 10;
            v = display[n];
            for (x = 0; x < 32; x++)
            {
                pen.Width = 3;
                pen.Color = Color.Black;
                gc.DrawLine(pen, x * 10 + 2, y + 5, x * 10 + 8, y + 5);
                if ((v & 1) == 1)
                {
                    pen.Width = 3;
                    pen.Color = Color.Lime;
                    gc.DrawLine(pen, x * 10 + 2, y + 5, x * 10 + 8, y + 5);
                }
                else
                {
                    pen.Width = 1;
                    pen.Color = Color.Green;
                    gc.DrawLine(pen, x * 10 + 5, y + 5, x * 10 + 6, y + 5);
                }
                v >>= 1;
            }
            pen.Dispose();
            gc.Dispose();
            store.Invalidate();
        }

        private void dispS_CheckedChanged(object sender, EventArgs e)
        {
            updateDisplays();
        }

        private void dispA_CheckedChanged(object sender, EventArgs e)
        {
            updateDisplays();
        }

        private void dispC_CheckedChanged(object sender, EventArgs e)
        {
            updateDisplays();
        }

        private void csOff_Click(object sender, EventArgs e)
        {
            setActionLine();
            updateDisplays();
        }

        private void csOn_CheckedChanged(object sender, EventArgs e)
        {
            updateDisplays();
            if (csOn.Checked) baby.setStopped(false);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int i;
            int[] mem;
            StreamWriter outFile;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                outFile = new StreamWriter(saveFileDialog1.FileName);
                mem = baby.getMem();
                for (i = 0; i < 32; i++)
                    outFile.WriteLine(mem[i].ToString());
                outFile.Close();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            int i;
            int[] mem;
            String buffer;
            StreamReader inFile;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                inFile = new StreamReader(openFileDialog1.FileName);
                mem = baby.getMem();
                for (i = 0; i < 32; i++)
                {
                    buffer = inFile.ReadLine();
                    mem[i] = Convert.ToInt32(buffer);
                }
                inFile.Close();
                updateDisplays();
            }
        }

        private void statAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (statAuto.Checked) baby.setStatAuto(true);
        }

        private void statMan_CheckedChanged(object sender, EventArgs e)
        {
            if (statMan.Checked) baby.setStatAuto(false);
        }

        private void lstat_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void stat_CheckedChanged(object sender, EventArgs e)
        {
            int n;
            n = 0;
            if (stat0On.Checked) n |= 1;
            if (stat1On.Checked) n |= 2;
            if (stat2On.Checked) n |= 4;
            if (stat3On.Checked) n |= 8;
            if (stat4On.Checked) n |= 16;
            if (stat13On.Checked) n |= (1 << 13);
            if (stat14On.Checked) n |= (1 << 14);
            if (stat15On.Checked) n |= (1 << 15);
            baby.setStatLines(n);
        }

        private void asmButton_Click(object sender, EventArgs e)
        {
            AssemblerForm dialog;
            dialog = new AssemblerForm();
            dialog.setMem(baby.getMem());
            dialog.setSource(sourceCode);
            dialog.ShowDialog();
            sourceCode = dialog.getSource();
            dialog.Dispose();
            updateDisplays();
        }

    }
}
