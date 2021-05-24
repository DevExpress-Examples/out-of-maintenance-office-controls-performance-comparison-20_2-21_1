using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.RichEdit;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace RichEdit21_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            var app = Application.Current as App;
            if (app != null && app.RunAttempt != 0)
                RunTest(app.LoadDocument, app.RunAttempt);
        }
        void RunTest(bool loadDocument, int attempt = 0)
        {
            var output = $"{Application.ResourceAssembly.GetName().Name}_{(loadDocument ? "WithDocument" : "WithoutDocument")}.txt";
            long coldLaunch = DoTest(loadDocument);
            long warmLaunch = DoTest(loadDocument);
            using (StreamWriter streamWriter = new StreamWriter(output, attempt != 1))
                streamWriter.WriteLine($"{coldLaunch}\t{warmLaunch}");
            if (attempt != 0)
                Close();
        }
        long DoTest(bool loadDocument)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Window window = new Window();
            Stopwatch sw = Stopwatch.StartNew();
            SetupWindow(window, loadDocument);
            Dispatcher.Invoke(() => window.Close(), DispatcherPriority.ApplicationIdle);
            sw.Stop();
            window = null;
            return sw.ElapsedMilliseconds;
        }
        void SetupWindow(Window window, bool loadDocument)
        {
            var richEditControl = new RichEditControl() { CommandBarStyle = CommandBarStyle.Ribbon };
            if (loadDocument)
                richEditControl.DocumentSource =
                    AssemblyHelper.GetResourceStream(Assembly.GetExecutingAssembly(), "Data/Demo.docx", true);
            window.Content = richEditControl;
            window.Show();
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), frame);
            if (!frame.Dispatcher.HasShutdownFinished)
                Dispatcher.PushFrame(frame);
        }
        static object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }
    }
}