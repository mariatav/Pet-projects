using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class OuterClass
    {
        //public static PrivateNestedClass privateNestedObject1 = new PrivateNestedClass();
        public static Interface1 privateNestedObject2 = new PrivateNestedClass();
        private static PrivateNestedClass privateNestedObject3 = new PrivateNestedClass();
        private void PrivateOuterMethod()
        {
            new PrivateNestedClass().PublicInnerMethod();
            //new PrivateNestedClass().PrivateInnerMethod();
        }
        public void PublicOuterMethod() { }
        private class PrivateNestedClass:Interface1
        {
            public void InterfaceMethod()
            {
                //throw new NotImplementedException();
            }

            public void PublicInnerMethod()
            {
                new OuterClass().PublicOuterMethod();
                new OuterClass().PrivateOuterMethod();
            }

            private void PrivateInnerMethod()
            {
            }
        }

        public class PublicNestedClass
        {

        }
    }
}
