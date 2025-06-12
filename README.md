# CustomMessageBox Demo

This WPF application demonstrates a custom message box implementation with enhanced styling and features compared to the standard WPF MessageBox.

## Features

- Modern UI with rounded corners and custom styling
- Support for all standard MessageBox button combinations (OK, OK/Cancel, Yes/No, Yes/No/Cancel)
- Support for all standard MessageBox icons (Information, Warning, Error, Question)
- Ability to set default button focus (Yes, No, Cancel)
- Support for custom images instead of standard icons
- Window position and size persistence using the UISettings system
- Keyboard shortcuts for all buttons (Y, N, O, C, Escape)

## Button Types

- **OK**: A simple message box with only an OK button
- **OK/Cancel**: A message box with OK and Cancel buttons
- **Yes/No**: A message box with Yes and No buttons
- **Yes/No/Cancel**: A message box with Yes, No, and Cancel buttons

## Icon Types

- **Information**: An information icon (ℹ)
- **Warning**: A warning icon (⚠)
- **Error**: An error icon (❌)
- **Question**: A question icon (❓)
- **None**: No icon
- **Custom Image**: Any custom ImageSource can be used instead of standard icons

## Usage

```csharp
// Basic usage
var result = CustomMessageBox.Show("This is a message");

// With title
var result = CustomMessageBox.Show("This is a message", "Message Title");

// With buttons and icon
var result = CustomMessageBox.Show(
    "Would you like to save your changes?",
    "Save Changes",
    MessageBoxButton.YesNoCancel,
    MessageBoxImage.Question);

// With default result
var result = CustomMessageBox.Show(
    "Would you like to save your changes?",
    "Save Changes",
    MessageBoxButton.YesNoCancel,
    MessageBoxImage.Question,
    MessageBoxResult.Yes);

// With custom image
var customImage = new BitmapImage(new Uri("pack://application:,,,/Resources/CustomIcon.png"));
var result = CustomMessageBox.ShowWithImage(
    "This is a custom image message",
    "Custom Image",
    MessageBoxButton.OKCancel,
    customImage);
```

## Window Settings

The application uses the UISettings system to persist window position and size between sessions.

## Additional Documentation

For implementation details, please see the [Views Documentation](Views/ReadMe_CustomMessageBox.md).

