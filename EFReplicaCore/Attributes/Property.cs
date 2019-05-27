using EFReplicaCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Attributes
{
    public class Property: Attribute
    {
        public string PropertyName { get; set; }
        public DataType DataType { get; set; }
        public Property(string propName = null, DataType type = DataType.Character)
        {
            PropertyName = propName;
            DataType = type;

        }
        public string GetValue()
        {
            return PropertyName;
        }
    }
}
