using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private void MethodMaster()
        {
            //branch1 change
        }
        private void MethodBranch4()
        {
            //Method body
        }
        private void MethodBranch5()
        {
            //master change
        }
        private void MethodBranch3() { }
        private void MethodBranch2() { }
        private void MethodBranch1()
        {
            //Method body
        }
        private void Method1()
        {
            //Method body branch 5
        }
        static void Main(string[] args)
        {
            //OuterClass obj = new OuterClass();
            //OuterClass.PublicNestedClass obj2 = new OuterClass.PublicNestedClass();
            ////new OuterClass.PrivateNestedClass();//produces an error
            //Interface1 nestedObject = OuterClass.privateNestedObject2;
            //nestedObject.InterfaceMethod();
            ////nestedObject.PublicInnerMethod();//produces an error

            //ClassInterface inter = new ClassInterface();
            ////(Interface2)inter.Method1();


            //int[] a = new int[] { 1, 2, 3 };
            //int[] b = new int[a.Length];
            //System.Array.Copy(a, b, a.Length);
            //b[2] = 4;
            //Console.WriteLine();

            int x = (1 + 2) * (3 + 5);
        }
    }
}
