using EFReplicaCore.Attributes;
using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaTest.EntityTest
{
    public class OrderProduct : Entity
    {
        [Property]
        [PrimaryKey]
        public long OrderId { get; set; }

        [Property]
        [PrimaryKey]
        public long ProductId { get; set; }

        [Property]
        public int Amount { get; set; }
        [Property]
        public decimal PricePerUnit { get; set; }

        public decimal Total
        {
            get
            {
                return PricePerUnit * Amount;
            }
        }
    }
}
