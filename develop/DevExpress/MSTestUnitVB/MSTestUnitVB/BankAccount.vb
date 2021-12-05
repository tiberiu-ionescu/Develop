Imports System

Public Class BankAccount
    Private ReadOnly m_customerName As String
    Private m_balance As Double
    Public Const DebitAmountExceedsBalanceMessage As String = "Debit amount exceeds balance"
    Public Const DebitAmountLessThanZeroMessage As String = "Debit amount is less than zero"

    Private Sub New()
    End Sub

    Public Sub New(customerName As String, balance As Double)
        m_customerName = customerName
        m_balance = balance
    End Sub

    Public Property CustomerName As String
        Get
            Return m_customerName
        End Get
        Set(value As String)
        End Set
    End Property

    Public Property Balance As Double
        Get
            Return m_balance
        End Get
        Set(value As Double)
        End Set
    End Property

    Public Sub Debit(amount As Double)
        If amount > m_balance Then
            Throw New System.ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage) 'ApplicationException
        End If

        If amount < 0 Then
            throw new System.ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage) 'ApplicationException
        End If

        m_balance -= amount 'intentionally incorrect code
    End Sub

    Public Sub Credit(amount As Double)
        If amount < 0 Then
            Throw New ArgumentOutOfRangeException("amount")
        End If

        m_balance += amount
    End Sub

    Public Shared Sub Main()
        Dim ba As New BankAccount("Mr. Bryan Walton", 11.99)

        ba.Credit(5.77)
        ba.Debit(11.22)
        Console.WriteLine("Current balance is ${0}", ba.Balance)
    End Sub
End Class