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

## Using Custom Icons in Another Project

To use the custom message box with custom icons in your own project, follow these detailed steps:

1. **Add the Required Files**:
   - Copy `CustomMessageBox.xaml` and `CustomMessageBox.xaml.cs` to your project
   - Copy `SystemIconExtensions.cs` to your project (for fallback system icons)
   - Update the namespaces in all files to match your project structure

2. **Set Up Resource Files**:
   - Create a `/Resources` folder in your project if one doesn't exist
   - Add the following PNG image files to your Resources folder:
     - `error.png` - For error message boxes
     - `warning.png` - For warning message boxes
     - `info.png` - For information message boxes
     - `question.png` - For question message boxes
   - Ensure each image is 32x32 pixels with transparent background for best results

3. **Set Resource Build Action**:
   - In Solution Explorer, select each PNG file
   - In the Properties panel, set the Build Action to "Resource"
   - Set "Copy to Output Directory" to "Do not copy"

4. **Avoid Duplicate Resource Entries**:
   - Ensure each image is included only once in your project file
   - Case sensitivity matters: use consistent casing for filenames
   - If you manually edit the .csproj file, check for duplicate Resource entries

5. **Reference the Images in XAML**:
   ```xml
   <Image Source="pack://application:,,,/Resources/error.png" />
   ```

6. **Reference the Images in Code**:
   ```csharp
   var customImage = new BitmapImage(new Uri("pack://application:,,,/Resources/error.png", UriKind.Absolute));
   ```

7. **Troubleshooting**:
   - If you get "An item with the same key has already been added" build errors, check for duplicate resource entries
   - If images don't display, verify paths and ensure the Build Action is set correctly

## Additional Documentation

For implementation details, please see the [Views Documentation](Views/ReadMe_CustomMessageBox.md).

