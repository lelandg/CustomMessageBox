using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CustomMessageBox.Views;
public partial class CustomMessageBox : Window
{
    public MessageBoxResult Result { get; private set; }
    private CustomMessageBox()
    {
        InitializeComponent();
        KeyDown += CustomMessageBox_KeyDown;
    }
    private void CustomMessageBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        // Skip if Alt is pressed - this is handled by WPF's built-in access key system
        if ((System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Alt) == System.Windows.Input.ModifierKeys.Alt)
        {
            return;
        }

        // Handle common keyboard shortcuts for dialog buttons
        switch (e.Key)
        {
            case System.Windows.Input.Key.Escape:
                if (CancelButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.Cancel;
                    DialogResult = false;
                    Close();
                }
                else if (NoButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.No;
                    DialogResult = false;
                    Close();
                }
                break;
            case System.Windows.Input.Key.Y:
                if (YesButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.Yes;
                    DialogResult = true;
                    Close();
                }
                break;
            case System.Windows.Input.Key.N:
                if (NoButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.No;
                    DialogResult = false;
                    Close();
                }
                break;
            case System.Windows.Input.Key.O:
                if (OkButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.OK;
                    DialogResult = true;
                    Close();
                }
                break;
            case System.Windows.Input.Key.C:
                if (CancelButton.Visibility == Visibility.Visible)
                {
                    Result = MessageBoxResult.Cancel;
                    DialogResult = false;
                    Close();
                }
                break;
        }
    }
    private void SetupButtons(MessageBoxButton buttons)
    {
        // Reset all buttons to not be default first
        OkButton.IsDefault = false;
        YesButton.IsDefault = false;
        NoButton.IsDefault = false;
        CancelButton.IsDefault = false;

        switch (buttons)
        {
            case MessageBoxButton.OK:
                OkButton.Visibility = Visibility.Visible;
                OkButton.IsDefault = true;
                break;
            case MessageBoxButton.OKCancel:
                OkButton.Visibility = Visibility.Visible;
                CancelButton.Visibility = Visibility.Visible;
                OkButton.IsDefault = true;
                break;
            case MessageBoxButton.YesNo:
                YesButton.Visibility = Visibility.Visible;
                NoButton.Visibility = Visibility.Visible;
                YesButton.IsDefault = true;
                break;
            case MessageBoxButton.YesNoCancel:
                YesButton.Visibility = Visibility.Visible;
                NoButton.Visibility = Visibility.Visible;
                CancelButton.Visibility = Visibility.Visible;
                YesButton.IsDefault = true;
                break;
        }
    }

    private void SetIcon(MessageBoxImage image)
    {
        string iconUri = "";

        switch (image)
        {
            case MessageBoxImage.Error:
                iconUri = "pack://application:,,,/CustomMessageBox;component/Resources/error.png";
                try {
                    // Try to load custom resource first
                    MessageIcon.Source = new BitmapImage(new Uri(iconUri));
                }
                catch {
                    // Fallback: create a simple error icon using WPF geometry
                    MessageIcon.Source = CreateFallbackIcon("❌", Brushes.Red);
                }
                break;
            case MessageBoxImage.Question:
                iconUri = "pack://application:,,,/CustomMessageBox;component/Resources/Question.png";
                try {
                    MessageIcon.Source = new BitmapImage(new Uri(iconUri));
                }
                catch {
                    MessageIcon.Source = CreateFallbackIcon("❓", Brushes.Blue);
                }
                break;
            case MessageBoxImage.Warning:
                iconUri = "pack://application:,,,/CustomMessageBox;component/Resources/warning.png";
                try {
                    MessageIcon.Source = new BitmapImage(new Uri(iconUri));
                }
                catch {
                    MessageIcon.Source = CreateFallbackIcon("⚠", Brushes.Orange);
                }
                break;
            case MessageBoxImage.Information:
                iconUri = "pack://application:,,,/CustomMessageBox;component/Resources/info.png";
                try {
                    MessageIcon.Source = new BitmapImage(new Uri(iconUri));
                }
                catch {
                    MessageIcon.Source = CreateFallbackIcon("ℹ", Brushes.Blue);
                }
                break;
            case MessageBoxImage.None:
                MessageIcon.Visibility = Visibility.Collapsed;
                break;
        }
    }

    private ImageSource CreateFallbackIcon(string text, Brush color)
    {
        var visual = new DrawingVisual();
        using (var context = visual.RenderOpen())
        {
            var formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI Symbol"),
                24,
                color,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);

            context.DrawText(formattedText, new Point(0, 0));
        }

        var bitmap = new RenderTargetBitmap(32, 32, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(visual);
        return bitmap;
    }
    
    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.Yes;
        DialogResult = true;
        Close();
    }
    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.No;
        DialogResult = false;
        Close();
    }
    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.OK;
        DialogResult = true;
        Close();
    }
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Result = MessageBoxResult.Cancel;
        DialogResult = false;
        Close();
    }
    /// <summary>
    /// Set a custom image instead of the standard system icon
    /// </summary>
    /// <param name="imageSource">The image source to use</param>
    public void SetCustomImage(ImageSource imageSource)
    {
        MessageIcon.Source = imageSource;
        MessageIcon.Visibility = Visibility.Visible;
    }
    public static MessageBoxResult Show(string messageText)
    {
        return Show(null, messageText, "", MessageBoxButton.OK, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(string messageText, string caption)
    {
        return Show(null, messageText, caption, MessageBoxButton.OK, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(string messageText, string caption, MessageBoxButton buttons)
    {
        return Show(null, messageText, caption, buttons, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(Window owner, string messageText)
    {
        return Show(owner, messageText, "", MessageBoxButton.OK, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(Window owner, string messageText, string caption)
    {
        return Show(owner, messageText, caption, MessageBoxButton.OK, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(Window owner, string messageText, string caption, MessageBoxButton buttons)
    {
        return Show(owner, messageText, caption, buttons, MessageBoxImage.None);
    }
    public static MessageBoxResult Show(string messageText, string caption, MessageBoxButton buttons, MessageBoxImage icon)
    {
        return Show(null, messageText, caption, buttons, icon);
    }
    public static MessageBoxResult Show(Window? owner, string messageText, string caption, MessageBoxButton buttons, MessageBoxImage icon)
    {
        return Show(owner, messageText, caption, buttons, icon, MessageBoxResult.None);
    }
    public static MessageBoxResult Show(Window? owner, string messageText, string caption, MessageBoxButton buttons, MessageBoxImage icon, MessageBoxResult defaultResult)
    {
        var msgBox = new CustomMessageBox
        {
            Title = string.IsNullOrEmpty(caption) ? "Message" : caption,
            Owner = owner,
            // Set default result based on parameter
            Result = defaultResult,
            MessageTitle =
            {
                // Set title and message text
                Text = string.IsNullOrEmpty(caption) ? "" : caption,
                Visibility = string.IsNullOrEmpty(caption) ? Visibility.Collapsed : Visibility.Visible
            },
            MessageText =
            {
                Text = messageText
            }
        };

        // Setup buttons and icon
        msgBox.SetupButtons(buttons);
        msgBox.SetIcon(icon);

        // Show dialog
        msgBox.ShowDialog();
        return msgBox.Result;
    }
    /// <summary>
    /// Display a message box with a custom image
    /// </summary>
    public static MessageBoxResult ShowWithImage(Window? owner, string messageText, string caption, 
        MessageBoxButton buttons, ImageSource customImage, MessageBoxResult defaultResult = MessageBoxResult.None)
    {
        var msgBox = new CustomMessageBox
        {
            Title = string.IsNullOrEmpty(caption) ? "Message" : caption,
            Owner = owner,
            // Set default result
            Result = defaultResult,
            MessageTitle =
            {
                // Set title and message text
                Text = string.IsNullOrEmpty(caption) ? "" : caption,
                Visibility = string.IsNullOrEmpty(caption) ? Visibility.Collapsed : Visibility.Visible
            },
            MessageText =
            {
                Text = messageText
            }
        };

        // Setup buttons and custom image
        msgBox.SetupButtons(buttons);
        msgBox.SetCustomImage(customImage);

        // Show dialog and make sure proper button is focused
        msgBox.ShowDialog();
        return msgBox.Result;
    }

}