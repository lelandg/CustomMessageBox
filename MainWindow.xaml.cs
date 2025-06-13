using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using StandardLicensingGenerator.UiSettings;
using CustomMessageBox.Views;

namespace CustomMessageBox;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly WindowSettingsManager _settingsManager;

    public MainWindow()
    {
        InitializeComponent();
        _settingsManager = new WindowSettingsManager(this);
        Closing += (sender, args) => _settingsManager.Save();
        KeyDown += MainWindow_KeyDown;
        Title = $"CustomMessageBox Demo - v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
    }

    #region Button Type Demos

    private void BtnOK_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This is a message with an OK button.",
            "OK Button Demo",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult("OK Button", result);
    }

    private void BtnOKCancel_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has OK and Cancel buttons.\nClick either button to see the result.",
            "OK/Cancel Demo",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Information);

        DisplayResult("OK/Cancel Buttons", result);
    }

    private void BtnYesNo_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has Yes and No buttons.\nWould you like to proceed?",
            "Yes/No Demo",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        DisplayResult("Yes/No Buttons", result);
    }

    private void BtnYesNoCancel_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has Yes, No, and Cancel buttons.\nDo you want to save your changes?",
            "Yes/No/Cancel Demo",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question);

        DisplayResult("Yes/No/Cancel Buttons", result);
    }

    #endregion

    #region Icon Type Demos

    private void BtnInformation_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This is an information message.\nIt uses the Information icon.",
            "Information Icon",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult("Information Icon", result);
    }

    private void BtnWarning_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This is a warning message.\nIt uses the Warning icon.",
            "Warning Icon",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);

        DisplayResult("Warning Icon", result);
    }

    private void BtnError_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This is an error message.\nIt uses the Error icon.",
            "Error Icon",
            MessageBoxButton.OK,
            MessageBoxImage.Error);

        DisplayResult("Error Icon", result);
    }

    private void BtnQuestion_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This is a question message.\nIt uses the Question icon.",
            "Question Icon",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        DisplayResult("Question Icon", result);
    }

    private void BtnNoIcon_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This message box has no icon.\nIt uses MessageBoxImage.None.",
            "No Icon",
            MessageBoxButton.OK,
            MessageBoxImage.None);

        DisplayResult("No Icon", result);
    }

    #endregion

    #region Default Result Demos

    private void BtnDefaultYes_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has a default result of Yes.\nPressing Enter will select Yes.",
            "Default Yes",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question,
            MessageBoxResult.Yes);

        DisplayResult("Default Yes", result);
    }

    private void BtnDefaultNo_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has a default result of No.\nPressing Enter will select No.",
            "Default No",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question,
            MessageBoxResult.No);

        DisplayResult("Default No", result);
    }

    private void BtnDefaultCancel_Click(object sender, RoutedEventArgs e)
    {
        var result = Views.CustomMessageBox.Show(
            this,
            "This dialog has a default result of Cancel.\nPressing Enter will select Cancel.",
            "Default Cancel",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question,
            MessageBoxResult.Cancel);

        DisplayResult("Default Cancel", result);
    }

    #endregion

    private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Escape)
        {
            var result = Views.CustomMessageBox.Show(
                this,
                "Do you want to quit?",
                "Quit?",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question,
                MessageBoxResult.Yes);
            
            Close();
        }
    }

    #region Custom Image Demo

    private void BtnCustomImage_Click(object sender, RoutedEventArgs e)
    {
        // Create a simple custom image - in a real app, you would load this from resources
        var customImage = CreateCustomImage();

        var result = Views.CustomMessageBox.ShowWithImage(
            this,
            "This dialog uses a custom image instead of a standard icon.\n" +
            "You can use any ImageSource for the icon.",
            "Custom Image",
            MessageBoxButton.OKCancel,
            customImage);

        DisplayResult("Custom Image", result);
    }

    #endregion

    #region Style Generator Demo

    private void BtnSystemColors_Click(object sender, RoutedEventArgs e)
    {
        Views.MessageBoxDemo.SetupSystemDialogColors();

        var result = Views.CustomMessageBox.Show(
            this,
            "This message box is using system dialog colors.",
            "System Colors",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult("System Colors Applied", result);
    }

    private void BtnDefaultColors_Click(object sender, RoutedEventArgs e)
    {
        Views.MessageBoxDemo.ResetToDefaultColors();

        var result = Views.CustomMessageBox.Show(
            this,
            "Message box has been reset to default colors.",
            "Default Colors",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult("Default Colors Restored", result);
    }

    private void BtnToggleStandardNormal_Click(object sender, RoutedEventArgs e)
    {
        // Toggle between standard and normal colors
        bool usingStandard = Views.MessageBoxDemo.ToggleStandardNormalColors();

        // Update button content based on current state
        BtnToggleStandardNormal.Content = usingStandard ? 
            "Switch to App Colors" : 
            "Switch to Standard Colors";

        string colorType = usingStandard ? "Standard" : "Normal";

        var result = Views.CustomMessageBox.Show(
            this,
            $"Switched to {colorType} colors. All future message boxes will use these colors.",
            $"{colorType} Colors",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult($"{colorType} Colors Applied", result);
    }

    private void BtnCustomColors_Click(object sender, RoutedEventArgs e)
    {
        // Set a custom color scheme (dark green in this example)
        Views.MessageBoxDemo.SetupCustomColors(Colors.DarkGreen);

        var result = Views.CustomMessageBox.Show(
            this,
            "This message box is using custom color scheme.",
            "Custom Colors",
            MessageBoxButton.OK,
            MessageBoxImage.Information);

        DisplayResult("Custom Colors Applied", result);
    }

    private ImageSource CreateCustomImage()
    {
        var visual = new DrawingVisual();
        using (var context = visual.RenderOpen())
        {
            // Draw a gradient circle
            var radius = 16;
            var center = new Point(radius, radius);
            var gradientBrush = new RadialGradientBrush(
                Colors.LightGreen, Colors.DarkGreen);
            context.DrawEllipse(gradientBrush, new Pen(Brushes.DarkGreen, 1), center, radius, radius);

            // Draw a checkmark
            var checkPen = new Pen(Brushes.White, 3);
            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Point(8, 16), false, false);
                geometryContext.LineTo(new Point(14, 22), true, true);
                geometryContext.LineTo(new Point(24, 10), true, true);
            }
            geometry.Freeze();
            context.DrawGeometry(null, checkPen, geometry);
        }

        var bitmap = new RenderTargetBitmap(32, 32, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(visual);
        return bitmap;
    }

    #endregion

    private void DisplayResult(string dialogType, MessageBoxResult result)
    {
        txtResult.Text = $"Dialog: {dialogType}\nResult: {result}";
    }
}