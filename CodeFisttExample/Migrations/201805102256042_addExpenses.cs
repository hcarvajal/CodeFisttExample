namespace CodeFisttExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addExpenses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Stocks", new[] { "CustomerId" });
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        CustomrId = c.String(maxLength: 250),
                        Name = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        ExpenseDate = c.DateTime(nullable: false),
                        ExpenseName = c.String(maxLength: 150),
                        Amout = c.Double(nullable: false),
                        Description = c.String(maxLength: 300),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            DropTable("dbo.Stocks");
            CreateStoredProcedure(
                "dbo.Projects_Insert",
                p => new
                    {
                        CustomrId = p.String(maxLength: 250),
                        Name = p.String(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Projects]([CustomrId], [Name], [CustomerId])
                      VALUES (@CustomrId, @Name, @CustomerId)
                      
                      DECLARE @ProjectId int
                      SELECT @ProjectId = [ProjectId]
                      FROM [dbo].[Projects]
                      WHERE @@ROWCOUNT > 0 AND [ProjectId] = scope_identity()
                      
                      SELECT t0.[ProjectId]
                      FROM [dbo].[Projects] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ProjectId] = @ProjectId"
            );
            
            CreateStoredProcedure(
                "dbo.Projects_Update",
                p => new
                    {
                        ProjectId = p.Int(),
                        CustomrId = p.String(maxLength: 250),
                        Name = p.String(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Projects]
                      SET [CustomrId] = @CustomrId, [Name] = @Name, [CustomerId] = @CustomerId
                      WHERE ([ProjectId] = @ProjectId)"
            );
            
            CreateStoredProcedure(
                "dbo.Projects_Delete",
                p => new
                    {
                        ProjectId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Projects]
                      WHERE ([ProjectId] = @ProjectId)"
            );
            
            CreateStoredProcedure(
                "dbo.Expenses_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 150),
                        ExpenseDate = p.DateTime(),
                        ExpenseName = p.String(maxLength: 150),
                        Amout = p.Double(),
                        Description = p.String(maxLength: 300),
                        ProjectId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Expenses]([Name], [ExpenseDate], [ExpenseName], [Amout], [Description], [ProjectId])
                      VALUES (@Name, @ExpenseDate, @ExpenseName, @Amout, @Description, @ProjectId)
                      
                      DECLARE @ExpenseId int
                      SELECT @ExpenseId = [ExpenseId]
                      FROM [dbo].[Expenses]
                      WHERE @@ROWCOUNT > 0 AND [ExpenseId] = scope_identity()
                      
                      SELECT t0.[ExpenseId]
                      FROM [dbo].[Expenses] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ExpenseId] = @ExpenseId"
            );
            
            CreateStoredProcedure(
                "dbo.Expenses_Update",
                p => new
                    {
                        ExpenseId = p.Int(),
                        Name = p.String(maxLength: 150),
                        ExpenseDate = p.DateTime(),
                        ExpenseName = p.String(maxLength: 150),
                        Amout = p.Double(),
                        Description = p.String(maxLength: 300),
                        ProjectId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Expenses]
                      SET [Name] = @Name, [ExpenseDate] = @ExpenseDate, [ExpenseName] = @ExpenseName, [Amout] = @Amout, [Description] = @Description, [ProjectId] = @ProjectId
                      WHERE ([ExpenseId] = @ExpenseId)"
            );
            
            CreateStoredProcedure(
                "dbo.Expenses_Delete",
                p => new
                    {
                        ExpenseId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Expenses]
                      WHERE ([ExpenseId] = @ExpenseId)"
            );
            
            DropStoredProcedure("dbo.Stocks_Insert");
            DropStoredProcedure("dbo.Stocks_Update");
            DropStoredProcedure("dbo.Stocks_Delete");
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Expenses_Delete");
            DropStoredProcedure("dbo.Expenses_Update");
            DropStoredProcedure("dbo.Expenses_Insert");
            DropStoredProcedure("dbo.Projects_Delete");
            DropStoredProcedure("dbo.Projects_Update");
            DropStoredProcedure("dbo.Projects_Insert");
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Stockname = c.String(maxLength: 250),
                        Content = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId);
            
            DropForeignKey("dbo.Expenses", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Expenses", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropTable("dbo.Expenses");
            DropTable("dbo.Projects");
            CreateIndex("dbo.Stocks", "CustomerId");
            AddForeignKey("dbo.Stocks", "CustomerId", "dbo.Customers", "CustomerId");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
