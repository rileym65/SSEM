using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManchesterBaby
{
    class Cpu
    {
        private int[] mem;
        private int ci;
        private int pi;
        private int acc;
        private Boolean skip;
        private Boolean stopped;
        private Boolean statAuto;
        private int statLines;

        public Cpu()
        {
            mem = new int[32];
            statAuto = true;
        }

        public void setStatLines(int n)
        {
            statLines = n;
        }

        public int getStatLines()
        {
            return statLines;
        }

        public void setStatAuto(Boolean b)
        {
            statAuto = b;
        }

        public void setStopped(Boolean s)
        {
            stopped = s;
        }

        public int[] getMem()
        {
            return mem;
        }

        public int getAcc()
        {
            return acc;
        }

        public int getCi()
        {
            return ci;
        }

        public int getPi()
        {
            return pi;
        }

        public Boolean getStopped()
        {
            return stopped;
        }

        public void reset()
        {
            ci = 0;
            acc = 0;
            skip = false;
            stopped = false;
        }

        public void step()
        {
            ci++;
            if (skip)
            {
                ci++;
                skip = false;
            }
            if (statAuto) pi = mem[ci & 0x1f] & statLines;
            else pi = statLines;
            switch ((pi >> 13) & 0x7)
            {
                case 0: ci = mem[pi & 0x1f];
                    break;
                case 1: ci += mem[pi & 0x1f];
                    break;
                case 2: acc = -mem[pi & 0x1f];
                    break;
                case 3: mem[pi & 0x1f] = acc;
                    break;
                case 4: acc -= mem[pi & 0x1f];
                    break;
                case 5: acc -= mem[pi & 0x1f];
                    break;
                case 6: if (acc < 0) skip = true;
                    break;
                case 7: stopped = true;
                    break;
            }
        }
    }
}
