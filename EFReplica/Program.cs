using EFReplicaCore;
using EFReplicaCore.Models;
using EFReplicaCore.Models.Context;
using System;
using System.Collections.Generic;
using EFReplicaCore.Enums;
using EFReplicaCore.Interfaces;
using EFReplicaCore.Models.Builders;
using EFReplicaCore.Models.Helpers;

namespace EFReplica
{
    class Program
    {
        static void Main(string[] args)
        {
            // VPN conn
            string connection = "Server=mssql.fhict.local;Database=dbi409368;User Id=dbi409368;Password=Heclepra9;";
            // Local conn
            //string connection = "Data Source=DESKTOP-1JEGGKM\\SQLEXPRESS;Initial Catalog=DBGenCore;Integrated Security=true;";

            MSSQLDatabase database = new MSSQLDatabase(connection);
            database.AddRepository<User>();

            Repository<User> repo = database.GetRepository("UserRepository") as Repository<User>;

            MSSQLDatabase database2 = new MSSQLDatabase(connection);
            var x = database.GetRepository("UserRepository") as Repository<User>;

            // Insert test
            repo.Insert(new User()
            {
                Name = "TEST USER 2.0 AAAA",
                BirthDate = DateTime.Now,
                Email = "test@test.nu",
                Password = "OnveiligPassword"
            });

            // Update test
            //repo.Update(new User()
            //{
            //    Id = 1005,
            //    Email = "TEST@TEST.NL",
            //    Name = "TEEEST",
            //    BirthDate = DateTime.Now,
            //    Password = "lalalala"
            //});

            // Delete test
            // By Id
            //repo.Delete(new User()
            //{
            //    Id = 1002
            //});
            // By custom object
            //repo.Delete(new User()
            //{
            //    Name = "Kevin",
            //    Email = "kbeye1999@hotmail.com"
            //});

            // Select test
            //var selects = new List<string>()
            //{
            //    //"distinct Name"
            //};
            //var filters = new List<ColumnFilter>()
            //{

            //};
            //var order = new List<KeyValuePair<string, string>>()
            //{

            //};
            //List<User> items = repo.GetWithFilter(selects, filters, order);
            //foreach (User u in items)
            //{
            //    Console.WriteLine(u.Name);
            //}
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
