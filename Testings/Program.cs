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
            TestimNumriElementeve();
            Console.Read();
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
            //var temp = new DataTree();
            //temp.AddElement(new PNumber(15));
            //t2.AddElement(temp);

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
        }
    }
}
