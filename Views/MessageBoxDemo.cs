using System.Windows;

namespace CustomMessageBox.Views;

/// <summary>
/// Demo class showing how to use the MessageBoxStyleGenerator
/// </summary>
public static class MessageBoxDemo
{
    private static bool _usingStandardColors = false;

    /// <summary>
    /// Gets whether the message boxes are currently using standard system colors
    /// </summary>
    public static bool UsingStandardColors => _usingStandardColors;

    /// <summary>
    /// Sets up system dialog colors for all CustomMessageBox instances
    /// </summary>
    public static void SetupSystemDialogColors()
    {
        // Create a generator that uses system colors and set it as current
        var systemGenerator = MessageBoxStyleGenerator.CreateSystemColorsGenerator();
        MessageBoxStyleGenerator.SetCurrent(systemGenerator);
        _usingStandardColors = true;
    }

    /// <summary>
    /// Sets up custom colors for all CustomMessageBox instances
    /// </summary>
    /// <param name="primary">Primary color for title bar and buttons</param>
    public static void SetupCustomColors(System.Windows.Media.Color primary)
    {
        // Create a custom generator with specified colors
        var customGenerator = new MessageBoxStyleGenerator
        {
            TitleBackground = new System.Windows.Media.SolidColorBrush(primary),
            BorderBrush = new System.Windows.Media.SolidColorBrush(primary),
            ButtonBackground = new System.Windows.Media.SolidColorBrush(primary)
            // Other properties can be customized here as needed
        };

        MessageBoxStyleGenerator.SetCurrent(customGenerator);
    }

    /// <summary>
    /// Resets to default colors
    /// </summary>
    public static void ResetToDefaultColors()
    {
        MessageBoxStyleGenerator.ResetToDefaults();
        _usingStandardColors = false;
    }

    /// <summary>
    /// Toggles between standard system colors and normal custom colors
    /// </summary>
    /// <returns>True if now using standard colors, false if using normal colors</returns>
    public static bool ToggleStandardNormalColors()
    {
        if (_usingStandardColors)
        {
            // Switch to normal colors (default blue theme)
            ResetToDefaultColors();
            return false;
        }
        else
        {
            // Switch to standard system colors
            SetupSystemDialogColors();
            return true;
        }
    }
}
