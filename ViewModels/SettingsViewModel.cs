using System;
using System.Reactive;
using Avalonia.Media;
using EmdrProject.Enums;
using EmdrProject.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    [Reactive] public int Speed { get; set; } = 10;
    [Reactive] public IBrush? Color { get; set; } = Brushes.Chocolate;
    [Reactive] public ShapeType ShapeType { get; set; } = ShapeType.Rectangle;
    [Reactive] public int RepeatCount { get; set; } = 30;
}