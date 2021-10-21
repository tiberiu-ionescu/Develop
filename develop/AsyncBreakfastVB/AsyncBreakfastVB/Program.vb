Imports NUnit.Framework

Namespace AsyncBreakfastVB
    Module Program
        Public Const coffeeToPour As Integer = 1
        Public Const eggsToBoil As Integer = 2
        Public Const baconToFry As Integer = 3
        Public Const toastToMake As Integer = 2

        Dim coffees As List(Of Coffee) = New List(Of Coffee)()
        Dim eggs As List(Of Egg) = New List(Of Egg)()
        Dim bacons As List(Of Bacon) = New List(Of Bacon)()
        Dim toasts As List(Of Toast) = New List(Of Toast)()

        Sub Main(args As String())
            Dim cup As Coffee = PourCoffee(coffeeToPour)
            Console.WriteLine("coffee is ready")

            RunTask().Wait()

            Dim oj As Juice = PourOJ()
            Console.WriteLine("oj is ready")
            Console.WriteLine("Breakfast is ready!")
            'Add your code here
        End Sub

        Public Async Function RunTask() As Task(Of Boolean)
            Dim eggsTask = BoilEggsAsync(eggsToBoil)
            Dim baconTask = FryBaconAsync(BaconToFry)
            Dim toastTask = MakeToastWithButterAndJamAsync(toastToMake)

            Dim breakfastTasks = New List(Of Task) From {eggsTask, baconTask, toastTask}

            While breakfastTasks.Count > 0
                Dim finishedTask As Task = Await Task.WhenAny(breakfastTasks)

                If finishedTask Is eggsTask Then
                    Console.WriteLine("eggs are ready")
                ElseIf finishedTask Is baconTask Then
                    Console.WriteLine("bacon is ready")
                ElseIf finishedTask Is toastTask Then
                    Console.WriteLine("toast is ready")
                End If
                breakfastTasks.Remove(finishedTask)
            End While
        End Function

        Private Function PourOJ() As Juice
            Console.WriteLine("Pouring orange juice")
            Return New Juice()
        End Function

        Private Sub ApplyJam(toast As Toast)
            Console.WriteLine("Putting jam on the toast")
        End Sub

        Private Sub ApplyButter(toast As Toast)
            Console.WriteLine("Putting butter on the toast")
        End Sub

        Private Async Function MakeToastWithButterAndJamAsync(number As Integer) As Task(Of Toast)
            Dim Toast = Await ToastBreadAsync(number)
            ApplyButter(Toast)
            ApplyJam(Toast)

            Return Toast
        End Function

        Private Async Function ToastBreadAsync(slices As Integer) As Task(Of Toast)
            For index = 1 To slices
                Dim t As New Toast
                toasts.Add(t)
                Console.WriteLine("Putting a slice of bread in the toaster")
                Console.WriteLine("Start toasting...")
            Next

            Await Task.Delay(3000)
            Console.WriteLine("Remove toast from toaster")

            Return toasts(0)
        End Function

        Private Async Function FryBaconAsync(slices As Integer) As Task(Of Bacon)
            Console.WriteLine($"putting {slices} slices of bacon in the pan")
            Console.WriteLine("cooking first side of bacon...")
            Await Task.Delay(3000)

            For index = 1 To slices
                Dim b As New Bacon
                bacons.Add(b)
                Console.WriteLine("flipping a slice of bacon")
            Next

            Console.WriteLine("cooking the second side of bacon...")
            Await Task.Delay(3000)
            Console.WriteLine("Put bacon on plate")

            Return bacons(0)
        End Function

        Private Async Function BoilEggsAsync(howMany As Integer) As Task(Of Egg)
            Console.WriteLine("Warming the egg pan...")
            Await Task.Delay(3000)

            For index = 1 To howMany
                Dim e As New Egg
                eggs.Add(e)
                Console.WriteLine("cracking one egg")
            Next

            Console.WriteLine("cooking the eggs ...")
            Await Task.Delay(3000)
            Console.WriteLine("Put eggs on plate")

            Return eggs(0)
        End Function

        Private Function PourCoffee(howMany As Integer) As Coffee
            For index = 1 To howMany
                Dim c As New Coffee
                coffees.Add(c)
                Console.WriteLine("Pouring coffee")
            Next

            Return coffees(0)
        End Function
    End Module
End Namespace
