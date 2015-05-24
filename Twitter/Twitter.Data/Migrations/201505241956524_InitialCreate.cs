namespace Twitter.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 200),
                        DatePublished = c.DateTime(nullable: false),
                        IsReply = c.Boolean(nullable: false),
                        PublisherId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PublisherId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.UserRetweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        TweetId = c.Int(nullable: false),
                        DateRetweeted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TweetId);
            
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
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(maxLength: 200),
                        DatePublished = c.DateTime(nullable: false),
                        RecipientId = c.String(maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RecipientId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.RecipientId)
                .Index(t => t.SenderId);
            
            CreateTable(
                "dbo.TweetFavorties",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TweetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TweetId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TweetId);
            
            CreateTable(
                "dbo.UserFollowing",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FollowingId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FollowingId })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowingId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "RecipientId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFollowing", "FollowingId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFollowing", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TweetFavorties", "TweetId", "dbo.Tweets");
            DropForeignKey("dbo.TweetFavorties", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRetweets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserRetweets", "TweetId", "dbo.Tweets");
            DropForeignKey("dbo.Tweets", "PublisherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.UserFollowing", new[] { "FollowingId" });
            DropIndex("dbo.UserFollowing", new[] { "FollowerId" });
            DropIndex("dbo.TweetFavorties", new[] { "TweetId" });
            DropIndex("dbo.TweetFavorties", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "RecipientId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.UserRetweets", new[] { "TweetId" });
            DropIndex("dbo.UserRetweets", new[] { "UserId" });
            DropIndex("dbo.Tweets", new[] { "PublisherId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.UserFollowing");
            DropTable("dbo.TweetFavorties");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.UserRetweets");
            DropTable("dbo.Tweets");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
