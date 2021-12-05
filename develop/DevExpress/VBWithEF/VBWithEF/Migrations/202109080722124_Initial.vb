Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class Initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Categories",
                Function(c) New With
                    {
                        .UId = c.Int(nullable := False, identity := True),
                        .UName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.UId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.Categories")
        End Sub
    End Class
End Namespace
