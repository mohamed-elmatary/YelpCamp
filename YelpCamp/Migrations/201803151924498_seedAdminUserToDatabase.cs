namespace YelpCamp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedAdminUserToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'430cd680-e6ce-4879-9eb6-a869d910f992', N'ali@gmail.com', 0, N'AOTo7XxY6VoHWj9io65hQ4K2OFmATaIZBlvpq/Z2GHuNZn3JV+KesiduVvdH5mvXsw==', N'e466d869-28b5-434a-aa03-3ffe49ce5305', NULL, 0, 0, NULL, 1, 0, N'ali')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c2fc96c6-fb80-4541-a56b-ba535a75fc4f', N'admin@gmail.com', 0, N'AE7HoDZEVv2x7TaJm1Rd6bjAtjde41noadXWRsE8fiBYxsJ288O/u95y2og4jIsPrA==', N'3573b8dd-fc0f-4c0c-aecc-2efbb0f53d92', NULL, 0, 0, NULL, 1, 0, N'admin')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd377690d-6ba9-452a-b225-040bb34c7bcd', N'maged@gmail.com', 0, N'AAP1oKWxKVA/qTlmgb56WKIQegUSmcqxNBatROqaW3ly3OgEKkrx4zAH3lv3jEfA0Q==', N'25db9135-45b3-4bac-9ead-c827b697e2ba', NULL, 0, 0, NULL, 1, 0, N'maged@gmail.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'11ee65d1-b338-4911-8bb5-96f4840b0def', N'Admin')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c2fc96c6-fb80-4541-a56b-ba535a75fc4f', N'11ee65d1-b338-4911-8bb5-96f4840b0def')
                ");

        }

        public override void Down()
        {
        }
    }
}
