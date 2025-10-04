namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Category", "SeoDescription", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Category", "SeoDescription", c => c.String(maxLength: 250));
        }
    }
}
