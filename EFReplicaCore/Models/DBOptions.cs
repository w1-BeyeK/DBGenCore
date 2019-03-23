using EFReplicaCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore.Models
{
    public class DBOptions
    {
        public DatabaseType Type { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
