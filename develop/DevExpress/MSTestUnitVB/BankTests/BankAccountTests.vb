Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports MSTestUnitVB

Namespace BankTests
    <TestClass>
    Public Class BankAccountTests
        <TestMethod>
        Sub TestSub()

        End Sub

        <TestMethod>
        Public Sub Debit_WithValidAmount_UpdatesBalance()
            ' Arrange
            Dim beginningBalance As Double = 11.99
            Dim debitAmount As Double = 4.55
            Dim expected As Double = 7.44
            Dim account As New BankAccount("Mr. Bryan Walton", beginningBalance)

            ' Act
            account.Debit(debitAmount)

            ' Assert
            Dim actual As Double = account.Balance
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly")
        End Sub

        <TestMethod>
        Public Sub Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
            ' Arrange
            Dim beginningBalance As Double = 11.99
            Dim debitAmount As Double = -100.0
            Dim account As New BankAccount("Mr. Bryan Walton", beginningBalance)

            ' Act
            Try
                account.Debit(debitAmount)
            Catch e As System.ArgumentOutOfRangeException
                ' Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage)
                Return
            End Try

            Assert.Fail("The expected exception was not thrown.")
        End Sub

        <TestMethod>
        Public Sub Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
            ' Arrange
            Dim beginningBalance As Double = 11.99
            Dim debitAmount As Double = 20.0
            Dim account As New BankAccount("Mr. Bryan Walton", beginningBalance)

            ' Act
            Try
                account.Debit(debitAmount)
            Catch e As System.ArgumentOutOfRangeException
                ' Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage)
                Return
            End Try

            Assert.Fail("The expected exception was not thrown.")
        End Sub
    End Class
End Namespace