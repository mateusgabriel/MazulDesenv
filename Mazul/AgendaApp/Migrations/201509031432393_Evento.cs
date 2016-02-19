namespace AgendaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Evento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventoes", "DiaDaSemana", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventoes", "DiaDaSemana");
        }
    }
}
