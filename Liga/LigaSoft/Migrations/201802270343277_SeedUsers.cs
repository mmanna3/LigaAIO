namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
		public override void Up()
		{
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N\'43eb2e63-0457-446b-a81c-45e92e6d778c\', N\'pablo@edefi.com.ar\', 0, N\'AG+VLHKCb/B/YF0XsTdhSCs58bi2tx8ZBx4f3zEJnj8alfckbfhuoBYFWV2CT1DL1g==\', N\'5dd8999c-e524-461b-94d1-bd5b84edd4ed\', NULL, 0, 0, NULL, 1, 0, N\'pablo@edefi.com.ar\')\r\nINSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N\'e6c0d3f5-80cb-499d-a963-5011082b1872\', N\'lucas@edefi.com.ar\', 0, N\'AD+rCgcrPAWoXY5PPB89vTqrHjdku/lyUVHAOIPp6hD+WsC8TgDm8wnnIQ641SzaGQ==\', N\'7c0a58b0-a68c-4574-afe9-a377cf93525e\', NULL, 0, 0, NULL, 1, 0, N\'lucas@edefi.com.ar\')\r\nINSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N\'fcb29fde-796b-4d51-8448-da74f0d29e33\', N\'ezequiel@edefi.com.ar\', 0, N\'AI8DnHbC7P481/3jQCFglYOhmIIH4Zvk9EXR/t2hTgFP22sk35d8y1tM+YQi4gPMiA==\', N\'7e8f000d-5278-45e4-b689-74147ed07a74\', NULL, 0, 0, NULL, 1, 0, N\'ezequiel@edefi.com.ar\')\r\n");
			Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\', N\'Administrador\')\r\n");
			Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'43eb2e63-0457-446b-a81c-45e92e6d778c\', N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\')\r\nINSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'e6c0d3f5-80cb-499d-a963-5011082b1872\', N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\')\r\nINSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'fcb29fde-796b-4d51-8448-da74f0d29e33\', N\'06f1567f-6c39-4a37-b7f2-3d090dcf0baa\')\r\n");
		}

		public override void Down()
        {
        }
    }
}
