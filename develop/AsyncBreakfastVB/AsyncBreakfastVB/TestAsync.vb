Imports NUnit.Framework

<TestFixture>
Public Class TestAsync
    <TestCase>
    Public Shared Sub CheckCoffees(coffeeToPour As Integer)
        Try
            Assert.AreEqual(coffeeToPour, Coffee.coffeesPoured)
        Catch ex As AssertionException
            Console.Write(ex.Message)
        End Try
    End Sub

    <TestCase>
    Public Shared Sub CheckEggs(eggsToBoil As Integer)
        Try
            Assert.AreEqual(eggsToBoil, Egg.eggsBoiled)
        Catch ex As AssertionException
            Console.Write(ex.Message)
        End Try
    End Sub

    <TestCase>
    Public Shared Sub CheckBacon(baconToFry As Integer)
        Try
            Assert.AreEqual(baconToFry, Bacon.baconFried)
        Catch ex As AssertionException
            Console.Write(ex.Message)
        End Try
    End Sub

    <TestCase>
    Public Shared Sub CheckToasts(toastsToMake As Integer)
        Try
            Assert.AreEqual(toastsToMake, Toast.toastsMade)
        Catch ex As AssertionException
            Console.Write(ex.Message)
        End Try
    End Sub
End Class