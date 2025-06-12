using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using StandardLicensingGenerator.UiSettings;

namespace CustomMessageBox.ViewModels
{
    public class UISettingsViewModel : INotifyPropertyChanged
    {
        private readonly WindowSettingsManager _settingsManager;
        private Window _window;

        private bool _rememberWindowPosition = true;
        public bool RememberWindowPosition
        {
            get => _rememberWindowPosition;
            set
            {
                if (_rememberWindowPosition != value)
                {
                    _rememberWindowPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        public UISettingsViewModel(Window window)
        {
            _window = window ?? throw new ArgumentNullException(nameof(window));
            _settingsManager = new WindowSettingsManager(window);

            // Hook up the window closing event to save settings
            _window.Closing += (sender, args) => SaveSettings();
        }

        public void SaveSettings()
        {
            if (RememberWindowPosition)
            {
                _settingsManager.Save();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
