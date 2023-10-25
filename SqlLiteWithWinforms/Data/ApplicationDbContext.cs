using SqlLiteWithWinforms.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;

namespace SqlLiteWithWinforms.Data
{
    public partial class ApplicationDbContext : DbContext
    {

        public DbSet<Proceder> Proceders { get; set; }


        public ApplicationDbContext() : base(new SQLiteConnection()
        {
            ConnectionString = new SQLiteConnectionStringBuilder() { DataSource = @"D:\MyWork\WinForms\SqlLiteWithWinforms\SqlLiteWithWinforms\bin\Debug\SQLiteWithEF.db", ForeignKeys = true }.ConnectionString
        }, true)
        {
            Database.ExecuteSqlCommand(@"CREATE TABLE IF NOT EXISTS [Proceders] (
                                            [Id] UNIQUEIDENTIFIER PRIMARY KEY,
                                            [Name] VARCHAR(255) NOT NULL,
                                            [SqlCode] VARCHAR(255) NOT NULL
                                        ); ");
        }



    }
}
