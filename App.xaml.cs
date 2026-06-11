using System.Windows;
using System.Windows.Threading;

namespace LearnXaml;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DispatcherUnhandledException += (s, args) =>
        {
            MessageBox.Show(
                $"未处理的异常:\n{args.Exception.Message}\n\n{args.Exception.StackTrace}",
                "程序异常",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            args.Handled = true;
        };

        AppDomain.CurrentDomain.UnhandledException += (s, args) =>
        {
            if (args.ExceptionObject is Exception ex)
            {
                MessageBox.Show(
                    $"致命异常:\n{ex.Message}\n\n{ex.StackTrace}",
                    "程序崩溃",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        };
    }
}

