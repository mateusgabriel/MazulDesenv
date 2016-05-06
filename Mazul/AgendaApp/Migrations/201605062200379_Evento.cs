namespace AgendaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Evento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventoes", "SemanaDoMes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventoes", "SemanaDoMes");
        }
    }
}
