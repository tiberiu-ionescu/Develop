Imports System.Data.Entity.Validation
Imports System.Data.SqlClient

Public Class Form1
    Dim context As New AppDbContext

    Private Sub ButtonSave_Click(sender As Object, e As EventArgs) Handles ButtonSave.Click
        Try
            If TextBox1.Text <> "" Then
                Dim newCategory As New Category
                newCategory.UName = TextBox1.Text
                context.Categories.Add(newCategory)
                context.SaveChanges()
                DataGridView1.DataSource = context.Categories.ToList()
            End If
        Catch ex As Exception
            MessageBox.Show("Failed to load Grid data: " + ex.Message.ToString())
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            DataGridView1.DataSource = context.Categories.ToList()
            DataGridView1.Columns("UId").Visible = False
            DataGridView1.Columns("UName").HeaderText = "Name"
        Catch ex As Exception
            MessageBox.Show("Failed to load Grid data: " + ex.Message.ToString())
        End Try
    End Sub
End Class
