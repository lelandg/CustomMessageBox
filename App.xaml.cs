using System.Configuration;
using System.Data;
using System.Windows;

namespace CustomMessageBox;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Set application-wide exception handling if needed
        // Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
    }

    private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        // Log the exception
        System.Diagnostics.Debug.WriteLine($"Unhandled exception: {e.Exception}");

        // Show error to the user
        Views.CustomMessageBox.Show(
            $"An unexpected error occurred:\n{e.Exception.Message}",
            "Application Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        // Mark as handled to prevent app from crashing
        e.Handled = true;
    }
}
