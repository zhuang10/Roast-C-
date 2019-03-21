namespace RoastMeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        message = c.String(),
                        voteScore = c.Int(nullable: false),
                        isFlagged = c.Boolean(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        Comment_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.PictureId, cascadeDelete: false)
                .ForeignKey("dbo.Comments", t => t.Comment_id)
                .Index(t => t.ParticipantId)
                .Index(t => t.PictureId)
                .Index(t => t.Comment_id);
            
            CreateTable(
                "dbo.Participants",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        email = c.String(),
                        ApplicationUserId = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        caption = c.String(),
                        imagePath = c.String(),
                        time = c.DateTime(nullable: false),
                        isFlagged = c.Boolean(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .Index(t => t.ParticipantId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        isLike = c.Boolean(nullable: false),
                        ParticipantId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                        Picture_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: false)
                .ForeignKey("dbo.Participants", t => t.ParticipantId, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.Picture_id)
                .Index(t => t.ParticipantId)
                .Index(t => t.CommentId)
                .Index(t => t.Picture_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "Comment_id", "dbo.Comments");
            DropForeignKey("dbo.Votes", "Picture_id", "dbo.Pictures");
            DropForeignKey("dbo.Votes", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Votes", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Pictures", "ParticipantId", "dbo.Participants");
            DropForeignKey("dbo.Comments", "PictureId", "dbo.Pictures");
            DropForeignKey("dbo.Comments", "ParticipantId", "dbo.Participants");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Votes", new[] { "Picture_id" });
            DropIndex("dbo.Votes", new[] { "CommentId" });
            DropIndex("dbo.Votes", new[] { "ParticipantId" });
            DropIndex("dbo.Pictures", new[] { "ParticipantId" });
            DropIndex("dbo.Comments", new[] { "Comment_id" });
            DropIndex("dbo.Comments", new[] { "PictureId" });
            DropIndex("dbo.Comments", new[] { "ParticipantId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Votes");
            DropTable("dbo.Pictures");
            DropTable("dbo.Participants");
            DropTable("dbo.Comments");
        }
    }
}
