Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class IdToGuid
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Categories", "UName", Function(c) c.String(maxLength := 10))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.Categories", "UName", Function(c) c.String())
        End Sub
    End Class
End Namespace
