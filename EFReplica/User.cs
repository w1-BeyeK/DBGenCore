using EFReplicaCore.Attributes;
using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplica
{
    public class User : Entity
    {
        [Property]
        public string Name { get; set; }
        [Property]
        public string Email { get; set; }
        [Property]
        public string Password { get; set; }

        [Property]
        public DateTime BirthDate { get; set; }
    }
}
