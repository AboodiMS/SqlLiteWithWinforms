using SqlLiteWithWinforms.Data;
using System.Data.Entity.Migrations;

internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
        SetSqlGenerator("System.Data.SQLite", new SQLiteMigrationSqlGenerator());
    }
}