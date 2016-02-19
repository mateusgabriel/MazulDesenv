namespace AgendaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Evento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Eventoes", "Horario", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Eventoes", "Horario");
        }
    }
}
