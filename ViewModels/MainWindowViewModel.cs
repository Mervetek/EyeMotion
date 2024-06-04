
using System;
using System.Reactive;
using EmdrProject.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        OpenSettingsWindow = ReactiveCommand.Create(() =>
        {
            // TODO 
            Settings = new SettingsViewModel();
            var settingsView = new SettingsView()
            {
                DataContext = Settings
            };
            settingsView.Show();
        });
    }

    [Reactive] public MovingObjectViewModel MovingObject { get; set; } = new();
    
    [Reactive] public SettingsViewModel Settings { get; set; }
    public ReactiveCommand<Unit, Unit> OpenSettingsWindow { get; set; }
}