Imports System.Windows

Namespace RichEdit20_2
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Private privateRunAttempt As Integer
		Public Property RunAttempt() As Integer
			Get
				Return privateRunAttempt
			End Get
			Private Set(ByVal value As Integer)
				privateRunAttempt = value
			End Set
		End Property
		Private privateLoadDocument As Boolean
		Public Property LoadDocument() As Boolean
			Get
				Return privateLoadDocument
			End Get
			Private Set(ByVal value As Boolean)
				privateLoadDocument = value
			End Set
		End Property
		Protected Overrides Sub OnStartup(ByVal e As StartupEventArgs)
			MyBase.OnStartup(e)
			If e.Args.Length <> 2 Then
				Return
			End If
			RunAttempt = Integer.Parse(e.Args(0))
			LoadDocument = Boolean.Parse(e.Args(1))
		End Sub
	End Class
End Namespace