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
    // Color themes for the unique colors button
    private readonly Color[][] _colorThemes = new Color[][] {
        new[] { Colors.Purple, Colors.MediumPurple },
        new[] { Colors.DarkRed, Colors.Crimson },
        new[] { Colors.DarkGreen, Colors.ForestGreen },
        new[] { Colors.DarkOrange, Colors.Orange },
        new[] { Colors.Teal, Colors.CadetBlue }
    };

    private int _currentThemeIndex = 0;
    private readonly WindowSettingsManager _settingsManager;

    // Flag to track if custom style is applied
    private bool _customStyleApplied = false;

    // Custom style for global application
    private Views.CustomMessageBoxStyle _customGlobalStyle = new Views.CustomMessageBoxStyle
    {
        TitleBackground = new SolidColorBrush(Colors.Indigo),
        BorderBrush = new SolidColorBrush(Colors.Indigo),
        ButtonBackground = new SolidColorBrush(Colors.Indigo),
        ButtonHoverBackground = new SolidColorBrush(Colors.MediumSlateBlue),
        ButtonPressedBackground = new SolidColorBrush(Colors.DarkSlateBlue),
        WindowBackground = new SolidColorBrush(Color.FromRgb(245, 240, 255)),
        ButtonOutline = new SolidColorBrush(Colors.MediumPurple)
    };

    public MainWindow()
    {
        InitializeComponent();
        _settingsManager = new WindowSettingsManager(this);
        Closing += (sender, args) => _settingsManager.Save();
        KeyDown += MainWindow_KeyDown;
        var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        Title = version != null ? $"CustomMessageBox Demo - v{version.Major}.{version.Minor}.{version.Build}" : "CustomMessageBox Demo (no version info)";
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
            if (result == MessageBoxResult.Yes)
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

    private void BtnUniqueColors_Click(object sender, RoutedEventArgs e)
    {
        // Cycle to the next color theme
        _currentThemeIndex = (_currentThemeIndex + 1) % _colorThemes.Length;
        var theme = _colorThemes[_currentThemeIndex];

        // Create a custom style with the theme colors
        var customStyle = new Views.CustomMessageBoxStyle
        {
            TitleBackground = new SolidColorBrush(theme[0]),
            BorderBrush = new SolidColorBrush(theme[0]),
            ButtonBackground = new SolidColorBrush(theme[0]),
            ButtonHoverBackground = new SolidColorBrush(theme[1]),
            ButtonPressedBackground = new SolidColorBrush(theme[0])
        };

        // Show a message box with the custom style
        var result = Views.CustomMessageBox.ShowWithStyle(
            this,
            $"This message box is using a unique color theme ({theme[0].ToString()}).",
            "Unique Colors",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Information,
            customStyle);

        DisplayResult("Unique Colors", result);
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

    private void BtnStyleDemo_Click(object sender, RoutedEventArgs e)
    {
        // First dialog: Purple theme
        var purpleStyle = new Views.CustomMessageBoxStyle
        {
            TitleBackground = new SolidColorBrush(Colors.Purple),
            BorderBrush = new SolidColorBrush(Colors.Purple),
            ButtonBackground = new SolidColorBrush(Colors.Purple),
            ButtonHoverBackground = new SolidColorBrush(Colors.MediumPurple),
            ButtonOutline = new SolidColorBrush(Colors.Lavender)
        };

        var result1 = Views.CustomMessageBox.ShowWithStyle(
            this,
            "This is the first dialog with a purple theme.",
            "Purple Theme",
            MessageBoxButton.OK,
            MessageBoxImage.Information,
            purpleStyle);

        // Second dialog: Orange theme with custom window background
        var orangeStyle = new Views.CustomMessageBoxStyle
        {
            TitleBackground = new SolidColorBrush(Colors.DarkOrange),
            BorderBrush = new SolidColorBrush(Colors.DarkOrange),
            WindowBackground = new SolidColorBrush(Color.FromRgb(255, 250, 240)), // Light orange
            ButtonBackground = new SolidColorBrush(Colors.DarkOrange),
            ButtonHoverBackground = new SolidColorBrush(Colors.Orange),
            TitleForeground = new SolidColorBrush(Colors.Black),
            ButtonOutline = new SolidColorBrush(Colors.SaddleBrown)
        };

        var result2 = Views.CustomMessageBox.ShowWithStyle(
            this,
            "This is the second dialog with an orange theme and custom window background.",
            "Orange Theme",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Warning,
            orangeStyle);

        // Third dialog: Green theme with rounded image
        var greenStyle = new Views.CustomMessageBoxStyle
        {
            TitleBackground = new SolidColorBrush(Colors.DarkGreen),
            BorderBrush = new SolidColorBrush(Colors.DarkGreen),
            ButtonBackground = new SolidColorBrush(Colors.DarkGreen),
            ButtonHoverBackground = new SolidColorBrush(Colors.ForestGreen),
            ButtonForeground = new SolidColorBrush(Colors.LightYellow),
            ButtonOutline = new SolidColorBrush(Colors.LightGreen)
        };

        // Create a custom image
        var customImage = CreateCustomImage();

        var result3 = Views.CustomMessageBox.ShowWithImageAndStyle(
            this,
            "This is the third dialog with a green theme and custom image.",
            "Green Theme",
            MessageBoxButton.YesNoCancel,
            customImage,
            greenStyle);

        // Display combined results
        txtResult.Text = $"Style Demo Results:\n" +
                         $"Purple Theme: {result1}\n" +
                         $"Orange Theme: {result2}\n" +
                         $"Green Theme: {result3}";
    }

    private void DisplayResult(string dialogType, MessageBoxResult result)
    {
        txtResult.Text = $"Dialog: {dialogType}\nResult: {result}";
    }

    private void BtnToggleCustomStyle_Click(object sender, RoutedEventArgs e)
    {
        _customStyleApplied = !_customStyleApplied;

        if (_customStyleApplied)
        {
            // Apply custom style to all message boxes
            var customGenerator = new Views.MessageBoxStyleGenerator
            {
                TitleBackground = _customGlobalStyle.TitleBackground,
                BorderBrush = _customGlobalStyle.BorderBrush,
                ButtonBackground = _customGlobalStyle.ButtonBackground,
                ButtonHoverBackground = _customGlobalStyle.ButtonHoverBackground,
                ButtonPressedBackground = _customGlobalStyle.ButtonPressedBackground,
                WindowBackground = _customGlobalStyle.WindowBackground,
                ButtonOutline = _customGlobalStyle.ButtonOutline
            };

            Views.MessageBoxStyleGenerator.SetCurrent(customGenerator);
            btnToggleCustomStyle.Content = "Use Default Style";

            var result = Views.CustomMessageBox.Show(
                this,
                "Applied custom style to all message boxes. All dialogs will now use the custom style.",
                "Custom Style Applied",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DisplayResult("Custom Style Toggle", result);
        }
        else
        {
            // Reset to default style
            Views.MessageBoxStyleGenerator.ResetToDefaults();
            btnToggleCustomStyle.Content = "Toggle Custom Style";

            var result = Views.CustomMessageBox.Show(
                this,
                "Reset to default style. All dialogs will now use the default style.",
                "Default Style Applied",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            DisplayResult("Custom Style Toggle", result);
        }
    }
}