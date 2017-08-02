using NH_UI.Modules;
using NH_VI.GraphLogic.Nodes.Geometry_Nodes;
using NH_VI.GraphLogic.Nodes.NumberNode;
using NH_VI.GraphLogic.Nodes.NumberNode.Comparsions;
using NH_VI.GraphLogic.Nodes.NumberNode.NumberSeries;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_UI.Factory
{

    public class NodeFactory
    {
        private ContextManager manager;
        public NodeFactory(ContextManager mg)
        {
            manager = mg;
        }

        public List<ButtonCategory> Categories => new List<ButtonCategory>() { MathCat, GeometryCat };
        public IEnumerable<Type> GetTypes
        {
            get
            {
                foreach (var cat in Categories)
                {
                    foreach (var sc in cat.SubCategories)
                    {
                        foreach (var t in sc.Types)
                        {
                            yield return t;
                        }
                    }
                }
            }
        }
        public Type[] GetAvailableTypes => GetTypes.ToArray();

        public ButtonCategory MathCat
        {
            get
            {
                var b = new ButtonCategory("Math")
                {
                    SubCategories = new List<SubCategory>() { MathOps, SeriesOps }
                };
                return b;
            }
        }
        public ButtonCategory GeometryCat
        {
            get
            {
                var b = new ButtonCategory("Geometry")
                {
                    SubCategories = new List<SubCategory>() { VectorOps }
                };
                return b;
            }
        }

        public SubCategory MathOps
        {
            get
            {
                var retval = new SubCategory("Operators", manager)
                {
                    Types = new List<Type>()
                {
                        typeof(NumberInputNode),
                    typeof(AddNode),
                    typeof(SubtractNode),
                    typeof(MultiplyNode),
                    typeof(DivideNode),
                    typeof(BiggerThanNode),
                    typeof(SmallerThanNode)
                }
                };
                return retval;
            }
        }
        public SubCategory VectorOps
        {
            get
            {
                var retval = new SubCategory("Vector", manager)
                {
                    Types = new List<Type>()
                {
                    typeof(ConstructVectorNode),
                    typeof(DeconstructVectorNode)
                }
                };
                return retval;
            }
        }
        public SubCategory SeriesOps
        {
            get
            {
                var retVal = new SubCategory("Series", manager)
                {
                    Types = new List<Type>()
                {
                    typeof(CreateSeriesNode)
                }
                };
                return retVal;
            }
        }
    }
}
