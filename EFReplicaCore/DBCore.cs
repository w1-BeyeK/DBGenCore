using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using EFReplicaCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFReplicaCore
{
    public class DBCore
    {
        private readonly DBOptions options;

        public IDatabase Database { get; set; }

        public DBCore(DBOptions options)
        {
            this.options = options;

            switch(options.Type)
            {
                // Use MSSQL db
                case DatabaseType.MSSQL:
                    break;
                // Use MySQL db
                case DatabaseType.MySQL:
                    break;
                // Use memory db
                case DatabaseType.None:
                default:
                    break;
            }
        }
    }
}
