using NH_UI.Modules;
using System;
using System.Collections.Generic;

namespace NH_UI.Factory
{
    public class SubCategory
    {
        public string Name;
        public List<Type> Types { get; set; } = new List<Type>();
        private ContextManager manager;
        public SubCategory(string name, ContextManager mg)
        {
            Name = name;
            manager = mg;
        }
        public List<NodeAdder> Buttons
        {
            get
            {
                var retVal = new List<NodeAdder>();
                foreach (var v in Types)
                {
                    retVal.Add(new NodeAdder(v, manager));
                }
                return retVal;
            }
        }
    }
}
