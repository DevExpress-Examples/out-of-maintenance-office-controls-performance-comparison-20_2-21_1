Imports System
Imports System.Diagnostics
Imports System.Threading

Namespace RibbonPerformanceComparison
	Friend Class Program
		Private Shared attemptCount As Integer = 10
		Private Shared versions() As String = { "21_1", "20_2" }
		Public Shared Sub Main(ByVal args() As String)
			Console.WriteLine("Products (1 - RichEdit, 2 - Spreadsheet, 3 - both, 0 - exit):")
			Dim num As Integer = -1
			Do
				num = Console.ReadKey().Key - ConsoleKey.D0
			Loop While num < 0 OrElse num > 3
			Console.WriteLine()
			Select Case num
				Case 0
					Return
				Case 1
					StartTestForProduct("RichEdit")
				Case 2
					StartTestForProduct("Spreadsheet")
				Case 3
					StartTestForProduct("RichEdit")
					StartTestForProduct("Spreadsheet")
			End Select
			Console.WriteLine("Press any key to exit")
			Console.ReadKey()
		End Sub
		Private Shared Sub StartTestForProduct(ByVal product As String)
			For Each version In versions
				Dim fileName As String = String.Format("{0}{1}.exe", product, version)
				For i As Integer = 0 To attemptCount - 1
					Dim processWithoutDocument = Process.Start(fileName, $"{i + 1} {False}")
					If processWithoutDocument Is Nothing Then
						Console.WriteLine("Process creating error")
						Return
					End If
					Do While Not processWithoutDocument.HasExited
						Thread.Sleep(1000)
					Loop
					Dim processWithDocument = Process.Start(fileName, $"{i + 1} {True}")
					If processWithDocument Is Nothing Then
						Console.WriteLine("Process creating error")
						Return
					End If
					Do While Not processWithDocument.HasExited
						Thread.Sleep(1000)
					Loop
				Next i
				Console.WriteLine($"Task {fileName} completed.")
			Next version
		End Sub
	End Class
End Namespace