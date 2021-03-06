﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NH_VI.DataTypes.Abstract
{
    public delegate void ExternalDelegate(object val);
   public interface IExternal
    {
        
        IExternal Copy();
        void RequestChange(object val);
        event ExternalDelegate OnExternalChanged;
    }
}
