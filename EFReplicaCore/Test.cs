using EFReplicaCore.Attributes;
using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore
{
    public class Test : Entity
    {
        [Property]
        public string Name { get; set; }
    }
}
