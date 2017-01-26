using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Outer
    {
        public int publicOuterVariable;
        private int privateOuterVariable;

        private void Method1()
        {
            int temp1=new Inner().publicInnerVariable;
            int temp2 = new Inner().privateInnerVariable;
        }

        class Inner
        {
            public int publicInnerVariable;
            private int privateInnerVariable;

            private void Method2()
            {
                int temp1 = new Outer().publicOuterVariable;
                int temp2 = new Outer().privateOuterVariable;
            }
        }
    }
}
