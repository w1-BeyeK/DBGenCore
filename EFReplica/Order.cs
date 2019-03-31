using EFReplicaCore.Attributes;
using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplica
{
    public class Order : Entity
    {
        [Property]
        [AutoIncrement]
        [PrimaryKey]
        public long Id { get; set; }

        [Property]
        public string Name { get; set; }
        [Property]
        public decimal TotalPrice { get; set; }
    }
}
