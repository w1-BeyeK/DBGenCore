using EFReplicaCore;
using EFReplicaCore.Models;
using EFReplicaCore.Models.Context;
using System;
using System.Collections.Generic;
using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Builders;

namespace EFReplica
{
    class Program
    {
        static Repository<User> repo;

        static void Main(string[] args)
        {
            DBCore db = new DBCore(
                new DBOptions()
                {
                    Name = "dbtest",
                    Type = DatabaseType.MSSQL
                });

            string connection = "Server=mssql.fhict.local;Database=dbi409368;User Id=dbi409368;Password=Heclepra9;";
            repo = new Repository<User>(new MSSQLContext<User>(connection));

            Console.WriteLine("Hello World!");
            //repo.Insert(new User()
            //{
            //    Id = 1,
            //    Name = "Kevin",
            //    Email = "kbeye1999@hotmail.com",
            //    Password = "Test123",
            //    BirthDate = DateTime.Now
            //});
            //repo.Insert(new User()
            //{
            //    Id = 2,
            //    Name = "Thomas",
            //    Email = "thommy@hotmail.com",
            //    Password = "Test123",
            //    BirthDate = DateTime.Now
            //});


            var filters = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("email", "kbeye1999@hotmail.com")
            };
            User item = repo.GetWithFilter(filters)[0];
            Console.WriteLine(item.Name);
            Console.ReadKey();
        }
    }
}
