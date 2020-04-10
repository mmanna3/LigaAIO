namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregarUsarioSoloPuedeFichar : DbMigration
    {
        public override void Up()
        {
			Sql("INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N\'5e1e3471-3ab8-4f36-a4cc-bd414c313083\', N\'SoloPuedeFichar\')");
			Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N\'895c47a7-1ec4-4f21-a1f0-9ca65bcdc7d4\', N\'fichaje@edefi.com.ar\', 0, N\'ABSzw3TNw+Ob/osP7jMCUoBmGfL2r64mLvtwJyeOV9nhtQ/Nd+qgSzANw/6Qw7tOEg==\', N\'5e2bbb37-d1ec-4273-89ed-53f2b6ed3c88\', NULL, 0, 0, NULL, 1, 0, N\'fichaje@edefi.com.ar\')");
			Sql("INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N\'895c47a7-1ec4-4f21-a1f0-9ca65bcdc7d4\', N\'5e1e3471-3ab8-4f36-a4cc-bd414c313083\')");
        }
        
        public override void Down()
        {
        }
    }
}
