using System;
using System.Data.Entity.Migrations;

public partial class CreateDb : DbMigration
{
    public override void Up()
    {
        CreateTable(
            "dbo.Proceders",
            c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 2147483647),
                    SqlCode = c.String(maxLength: 2147483647),
                })
            .PrimaryKey(t => t.Id);
        
    }
    
    public override void Down()
    {
        DropTable("dbo.Proceders");
    }
}
