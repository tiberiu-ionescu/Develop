Imports System.Data.Entity
Imports System
Imports System.Collections.Generic

Public Class AppDbContext
    Inherits DbContext

    Public Property Categories As DbSet(Of Category)
End Class