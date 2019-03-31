using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Attributes
{
    public class Property: Attribute
    {
        public string PropertyName { get; set; }
        public Property(string propName = null)
        {
            PropertyName = propName;
        }
        public string GetValue()
        {
            return PropertyName;
        }
    }
}
