
using System;
using System.Reactive;
using EmdrProject.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive] public MovingObjectViewModel MovingObject { get; set; } = new();
    [Reactive] public SettingsViewModel Settings { get; set; } = new();

}