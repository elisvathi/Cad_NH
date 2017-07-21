using CadTest3.GraphLogic;
using NH_VI.DataTypes.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testings
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestimNumriElementeve();
            TestEmptyTree();
            Console.Read();
        }


        static void TestEmptyTree()
        {
            var t = new DataTree();
            t.AddElement(new PNumber(4));
            t.AddElement(new PNumber(3));
            t.AddElement(new PNumber(4));
            var t2 = new DataTree();
            t2.AddElement(new PNumber(0));
            var t3 = new DataTree();
            t3.AddElement(new PNumber(34));
            t3.AddElement(new PNumber(33));
            t3.AddElement(new PNumber(23));
            t3.AddElement(new PNumber(83));
            t2.AddElement(t3);
            t.AddElement(t2);
            Console.WriteLine(t.DataDescription);
            var temp = t.GetEmptyTree();
            Console.WriteLine(temp.DataDescription);
            Console.WriteLine(temp.NumberChildEndings());
        }
        static void TestimNumriElementeve()
        {
            var t = new DataTree();
            t.AddElement(new PNumber(1));

            t.AddElement(new PNumber(2));

            t.AddElement(new PNumber(3));

            Console.WriteLine(t.DataDescription);

            var t2 = new DataTree();
            t2.AddElement(new PNumber(4));
            var temp = new DataTree();

            temp.AddElement(new PNumber(15));
            temp.AddElement(new PNumber(72));
            var temp2 = new PNumber(85);
            temp.AddElement(temp2);
            t2.AddElement(temp);

            t2.AddElement(new PNumber(5));
            t2.AddElement(new PNumber(6));
            t2.AddElement(new PNumber(7));
            t2.AddElement(new PNumber(8));
            t2.AddElement(new PNumber(9));
            Console.WriteLine(t2.DataDescription);
            Console.WriteLine("AfTER CONFORMING  \r\n" );
            DataTree.ConformTrees(t, t2, NH_VI.GraphLogic.Operators.DataGroupingModes.CrossReference, out DataTree f1, out DataTree f2);

            Console.WriteLine(f1.DataDescription);
            Console.WriteLine(f2.DataDescription);
            Console.WriteLine("");
            Console.WriteLine("Nr: "+ f2.NumberChildEndings());

            var a = f2.GetEmptyTree();
            Console.WriteLine(a.DataDescription);

        }
    }
}
