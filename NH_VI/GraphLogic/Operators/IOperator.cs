﻿using CadTest3.GraphLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.GraphLogic.Operators
{
    public interface IOperator
    {
        List<Type> InputTypes { get; }
        List<Type> OutputTypes { get; }
        bool TreeOperator { get; }
        List<IData> ProcessData(List<IData> dat);
    }
}
