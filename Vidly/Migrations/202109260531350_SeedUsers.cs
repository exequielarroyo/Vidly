namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'06d79067-ee33-4779-8887-9dec922d642d', N'user@vidly.com', 0, N'AN0k2+xEvJTQArEyS0XnsU25g7PJA9pWnMBTAQ4h+nXir4jFrcvLQ5v9IeXimaB7vQ==', N'b571d88b-ed98-41ae-8992-21d95d7f85a5', NULL, 0, 0, NULL, 1, 0, N'user@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'1cf07af2-dfe5-4557-b9b5-1e3086dc9f0e', N'admin@vidly.com', 0, N'APz2E89o0tKI0wyc9/wYUgBGWqIBupoDYmN3HaqquPN8CslWrYdBQb20vHmIqkNvyA==', N'd29cbcb8-2291-44f7-9046-ffed8a5a2f9e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2f3329b0-9d04-4af6-93b2-64f98f982204', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1cf07af2-dfe5-4557-b9b5-1e3086dc9f0e', N'2f3329b0-9d04-4af6-93b2-64f98f982204')
");
        }

        public override void Down()
        {
        }
    }
}
