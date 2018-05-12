namespace CodeFisttExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        LastName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Stockname = c.String(maxLength: 250),
                        Content = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateStoredProcedure(
                "dbo.Customers_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 150),
                        LastName = p.String(maxLength: 100),
                    },
                body:
                    @"INSERT [dbo].[Customers]([Name], [LastName])
                      VALUES (@Name, @LastName)
                      
                      DECLARE @CustomerId int
                      SELECT @CustomerId = [CustomerId]
                      FROM [dbo].[Customers]
                      WHERE @@ROWCOUNT > 0 AND [CustomerId] = scope_identity()
                      
                      SELECT t0.[CustomerId]
                      FROM [dbo].[Customers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[CustomerId] = @CustomerId"
            );
            
            CreateStoredProcedure(
                "dbo.Customers_Update",
                p => new
                    {
                        CustomerId = p.Int(),
                        Name = p.String(maxLength: 150),
                        LastName = p.String(maxLength: 100),
                    },
                body:
                    @"UPDATE [dbo].[Customers]
                      SET [Name] = @Name, [LastName] = @LastName
                      WHERE ([CustomerId] = @CustomerId)"
            );
            
            CreateStoredProcedure(
                "dbo.Customers_Delete",
                p => new
                    {
                        CustomerId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Customers]
                      WHERE ([CustomerId] = @CustomerId)"
            );
            
            CreateStoredProcedure(
                "dbo.Stocks_Insert",
                p => new
                    {
                        Stockname = p.String(maxLength: 250),
                        Content = p.String(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Stocks]([Stockname], [Content], [CustomerId])
                      VALUES (@Stockname, @Content, @CustomerId)
                      
                      DECLARE @StockId int
                      SELECT @StockId = [StockId]
                      FROM [dbo].[Stocks]
                      WHERE @@ROWCOUNT > 0 AND [StockId] = scope_identity()
                      
                      SELECT t0.[StockId]
                      FROM [dbo].[Stocks] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[StockId] = @StockId"
            );
            
            CreateStoredProcedure(
                "dbo.Stocks_Update",
                p => new
                    {
                        StockId = p.Int(),
                        Stockname = p.String(maxLength: 250),
                        Content = p.String(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Stocks]
                      SET [Stockname] = @Stockname, [Content] = @Content, [CustomerId] = @CustomerId
                      WHERE ([StockId] = @StockId)"
            );
            
            CreateStoredProcedure(
                "dbo.Stocks_Delete",
                p => new
                    {
                        StockId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Stocks]
                      WHERE ([StockId] = @StockId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Stocks_Delete");
            DropStoredProcedure("dbo.Stocks_Update");
            DropStoredProcedure("dbo.Stocks_Insert");
            DropStoredProcedure("dbo.Customers_Delete");
            DropStoredProcedure("dbo.Customers_Update");
            DropStoredProcedure("dbo.Customers_Insert");
            DropForeignKey("dbo.Stocks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Stocks", new[] { "CustomerId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Customers");
        }
    }
}
