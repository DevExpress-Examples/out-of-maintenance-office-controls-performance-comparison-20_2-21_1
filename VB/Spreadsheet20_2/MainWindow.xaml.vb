Imports System
Imports System.Diagnostics
Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Threading
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Spreadsheet

Namespace Spreadsheet20_2
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Public Sub New()
			InitializeComponent()
		End Sub
		Protected Overrides Sub OnContentRendered(ByVal e As EventArgs)
			MyBase.OnContentRendered(e)
			Dim app As App = TryCast(Application.Current, App)
			If app IsNot Nothing AndAlso app.RunAttempt <> 0 Then
				RunTest(app.LoadDocument, app.RunAttempt)
			End If
		End Sub
		Private Sub RunTest(ByVal loadDocument As Boolean, Optional ByVal attempt As Integer = 0)
			Dim output = $"{Application.ResourceAssembly.GetName().Name}_{(If(loadDocument, "WithDocument", "WithoutDocument"))}.txt"
			Dim coldLaunch As Long = DoTest(loadDocument)
			Dim warmLaunch As Long = DoTest(loadDocument)
			Using streamWriter As New StreamWriter(output, attempt <> 1)
				streamWriter.WriteLine($"{coldLaunch}" & vbTab & "{warmLaunch}")
			End Using
			If attempt <> 0 Then
				Close()
			End If
		End Sub
		Private Function DoTest(ByVal loadDocument As Boolean) As Long
			GC.Collect()
			GC.WaitForPendingFinalizers()
			Dim window As New Window()
			Dim sw As Stopwatch = Stopwatch.StartNew()
			SetupWindow(window, loadDocument)
			Dispatcher.Invoke(Sub() window.Close(), DispatcherPriority.ApplicationIdle)
			sw.Stop()
			window = Nothing
			Return sw.ElapsedMilliseconds
		End Function
		Private Sub SetupWindow(ByVal window As Window, ByVal loadDocument As Boolean)
			Dim richEditControl = New SpreadsheetControl() With {.CommandBarStyle = CommandBarStyle.Ribbon}
			If loadDocument Then
				richEditControl.DocumentSource = AssemblyHelper.GetResourceStream(System.Reflection.Assembly.GetExecutingAssembly(), "Data/Demo.xlsx", True)
			End If
			window.Content = richEditControl
			window.Show()
			Dim frame As New DispatcherFrame()
			Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, New DispatcherOperationCallback(AddressOf ExitFrame), frame)
			If Not frame.Dispatcher.HasShutdownFinished Then
				Dispatcher.PushFrame(frame)
			End If
		End Sub
		Private Shared Function ExitFrame(ByVal f As Object) As Object
			DirectCast(f, DispatcherFrame).Continue = False
			Return Nothing
		End Function
	End Class
End Namespace