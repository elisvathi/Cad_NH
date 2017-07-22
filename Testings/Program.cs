using CadTest3.GraphLogic;
using NH_VI.DataTypes.Abstract;
using NH_VI.DataTypes.Numeric;
using NH_VI.GraphLogic.Nodes.NumberNode;
using NH_VI.GraphLogic.Operators;
using NH_VI.GraphLogic.Operators.NumberOperators;
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
            //TestEmptyTree();
            //CrossRefTree();
            //OperatorTest();
            //PathTest();
            GraphTest();
            Console.Read();
        }

        static void CrossRefTree()
        {
            var t = new DataTree();
            var t2 = new DataTree();
            var t3 = new DataTree();

            t.AddElement(new PNumber(1));
            t.AddElement(new PNumber(2));
            t.AddElement(new PNumber(3));
            t2.AddElement(new PNumber(2));
            t2.AddElement(new PNumber(4));
            t3.AddElement(new PNumber(12));
            t3.AddElement(new PNumber(24));

            DataTree.ConformTrees(new List<DataTree>() { t, t2, t3 }, out List<DataTree> output, DataGroupingModes.Primary);


          foreach(var v in output)
            {
                Print(v);
            }
        }
        static void OperatorTest()
        {
            var t = new DataTree();
            var t2 = new DataTree();
            var t3 = new DataTree();
            t.AddElement(new PNumber(200));
            t.AddElement(new PNumber(3));
            t2.AddElement(new PNumber(20));
            t2.AddElement(new PNumber(100));
            t2.AddElement(new PNumber(1000));
            t3.AddElement(new PNumber(14));
            t2.AddElement(t3);
            var op = new AddOperator();
            op.GroupingMode = DataGroupingModes.CrossReference;
            var te = op.ProcessData(new List<IData>() { t, t2 });
            foreach(var res in te)
            {
                Print(res);
            }
        }
        static void PathTest()
        {
            var t = new PNumber(15);
            var l = new DataTree();
            l.AddElement(new PNumber(4));
            l.AddElement(new PNumber(3));
            l.AddElement(new PNumber(1));
            var t2 = new DataTree();
            t2.AddElement(new PNumber(22));
            var t3 = new DataTree();
            t3.AddElement(new PNumber(14));
            t3.AddElement(t);
            t2.AddElement(t3);
            l.AddElement(t2);
            var d = new DataTree();
            d.AddElement(l.Copy());
            d.AddElement(l);
            d.AddElement(d.Copy());
            Print(d);
            PrintPath(t.Path);
            foreach(var temp in d.GetChildIterator())
            {
                PrintPath(temp.Path);
            }

        }
        static void PrintPath(int[] p)
        {
            Console.WriteLine();
            foreach(var v in p)
            {
                Console.Write(" " + v);
            }
        }
        static void Print(IData d)
        {
            Console.WriteLine(d.DataDescription);
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
            Console.WriteLine("AfTER CONFORMING  \r\n");
            DataTree.ConformTrees(t, t2, NH_VI.GraphLogic.Operators.DataGroupingModes.CrossReference, out DataTree f1, out DataTree f2);

            Console.WriteLine(f1.DataDescription);
            Console.WriteLine(f2.DataDescription);
            Console.WriteLine("");
            Console.WriteLine("Nr: " + f2.NumberChildEndings());

            var a = f2.GetEmptyTree();
            Console.WriteLine(a.DataDescription);

        }

        public static void GraphTest()
        {
            var n = new AddNode();
            var n2 = new NumberInputNode();
            var numer = 152.2;
            var exNum = new ExternalNumber(numer);
            var exData = new ExternalData(exNum);
            n2.ConnectToData(new List<ExternalData>() { exData });

            Print(n2.OutputSockets[0].Data);
        }
        static void Print(string s)
        {
            Console.WriteLine(s);
        }
        static void Print(double d)
        {
            Print(d + "");
        }
    }
}
