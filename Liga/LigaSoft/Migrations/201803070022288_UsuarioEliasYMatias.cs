namespace LigaSoft.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioEliasYMatias : DbMigration
    {
        public override void Up()
        {
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8ec7a8c9-7197-4617-ad1d-4ebc99ba9076', N'matias@edefi.com.ar', 0, N'APV4oys5KJm/pOSQpzr6Cs2ZCF4OalOYgEFkeFSXi94mG/x2LYwvLiBWLDh/vce5LA==', N'1ae28605-1c64-4d32-92f2-ff86b37cddec', NULL, 0, 0, NULL, 1, 0, N'matias@edefi.com.ar')");
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a6f37f2a-9dfe-4953-9427-91e0f03789e3', N'elias@edefi.com.ar', 0, N'ADNIqRXnOhR7F/Jobsragrnj+acpFE5BaQoBQYtvG2+Q+1ruvKwuAiQ5u2r3Hb3AuQ==', N'dc1eb715-ef67-41d7-8626-08238cc80c8b', NULL, 0, 0, NULL, 1, 0, N'elias@edefi.com.ar')");
	        Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'8ec7a8c9-7197-4617-ad1d-4ebc99ba9076\', N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\')\r\nINSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'a6f37f2a-9dfe-4953-9427-91e0f03789e3\', N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\')\r\n");
		}
        
        public override void Down()
        {
        }
    }
}
