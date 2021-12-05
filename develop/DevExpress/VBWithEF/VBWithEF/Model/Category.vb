Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Category

    Private Id As Int32
    <Key>
    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    Public Property UId As Int32
        Get
            Return Id
        End Get
        Set
            Id = Value
        End Set
    End Property

    Private Name As String
    <StringLength(10)>
    Public Property UName As String
        Get
            Return Name
        End Get
        Set
            Name = Value
        End Set
    End Property
End Class
