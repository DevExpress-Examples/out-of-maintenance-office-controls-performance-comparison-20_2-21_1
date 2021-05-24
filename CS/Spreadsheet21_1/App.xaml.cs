using System.Windows;

namespace Spreadsheet21_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public int RunAttempt { get; private set; }
        public bool LoadDocument { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (e.Args.Length != 2) return;
            RunAttempt = int.Parse(e.Args[0]);
            LoadDocument = bool.Parse(e.Args[1]);
        }
    }
}