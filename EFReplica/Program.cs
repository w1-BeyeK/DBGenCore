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
        static Repository<User> repo;

        static void Main(string[] args)
        {
            DBCore db = new DBCore(
                new DBOptions()
                {
                    Name = "dbtest",
                    Type = DatabaseType.MSSQL
                });

            // VPN conn
            //string connection = "Server=mssql.fhict.local;Database=dbi409368;User Id=dbi409368;Password=Heclepra9;";
            // Local conn
            string connection = "Data Source=DESKTOP-1JEGGKM\\SQLEXPRESS;Initial Catalog=DBGenCore;Integrated Security=true;";
            repo = new Repository<User>(new MSSQLContext<User>(connection));

            repo.Insert(new User()
            {
                Name = "TEST USER",
                BirthDate = DateTime.Now,
                Email = "test@test.nu",
                Password = "OnveiligPassword"
            });

            //var filters = new List<ColumnFilter>()
            //{
            //    new ColumnFilter("Name", "br", FilterType.Like)
            //};
            //List<User> items = repo.GetWithFilter(filters: filters);
            //foreach(User u in items)
            //{
            //    Console.WriteLine(u.Name);
            //}
            Console.ReadKey();
        }
    }
}
