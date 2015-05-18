namespace DocSpin2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationsRevamp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.DocumentVersions", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "Repository_Id", "dbo.Repositories");
            DropForeignKey("dbo.DocumentVersions", "Document_Id", "dbo.Documents");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Document_Id" });
            DropIndex("dbo.Documents", new[] { "Repository_Id" });
            DropIndex("dbo.DocumentVersions", new[] { "Author_Id" });
            DropIndex("dbo.DocumentVersions", new[] { "Document_Id" });
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "Document_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Documents", "Repository_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Repositories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentVersions", "Filename", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentVersions", "OriginalFilename", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentVersions", "Hash", c => c.String(nullable: false));
            AlterColumn("dbo.DocumentVersions", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.DocumentVersions", "Document_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Author_Id");
            CreateIndex("dbo.Comments", "Document_Id");
            CreateIndex("dbo.Documents", "Repository_Id");
            CreateIndex("dbo.DocumentVersions", "Author_Id");
            CreateIndex("dbo.DocumentVersions", "Document_Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DocumentVersions", "Author_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Documents", "Repository_Id", "dbo.Repositories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DocumentVersions", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "Active");
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AddColumn("dbo.AspNetUsers", "Active", c => c.String());
            DropForeignKey("dbo.DocumentVersions", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.Documents", "Repository_Id", "dbo.Repositories");
            DropForeignKey("dbo.DocumentVersions", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.DocumentVersions", new[] { "Document_Id" });
            DropIndex("dbo.DocumentVersions", new[] { "Author_Id" });
            DropIndex("dbo.Documents", new[] { "Repository_Id" });
            DropIndex("dbo.Comments", new[] { "Document_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            AlterColumn("dbo.DocumentVersions", "Document_Id", c => c.Int());
            AlterColumn("dbo.DocumentVersions", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.DocumentVersions", "Hash", c => c.String());
            AlterColumn("dbo.DocumentVersions", "OriginalFilename", c => c.String());
            AlterColumn("dbo.DocumentVersions", "Filename", c => c.String());
            AlterColumn("dbo.Repositories", "Name", c => c.String());
            AlterColumn("dbo.Documents", "Repository_Id", c => c.Int());
            AlterColumn("dbo.Documents", "Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AlterColumn("dbo.Comments", "Document_Id", c => c.Int());
            AlterColumn("dbo.Comments", "Author_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Content", c => c.String());
            CreateIndex("dbo.DocumentVersions", "Document_Id");
            CreateIndex("dbo.DocumentVersions", "Author_Id");
            CreateIndex("dbo.Documents", "Repository_Id");
            CreateIndex("dbo.Comments", "Document_Id");
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.DocumentVersions", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.Documents", "Repository_Id", "dbo.Repositories", "Id");
            AddForeignKey("dbo.DocumentVersions", "Author_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "Document_Id", "dbo.Documents", "Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
