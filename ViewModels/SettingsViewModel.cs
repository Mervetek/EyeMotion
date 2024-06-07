using System;
using Avalonia.Media;
using EmdrProject.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public SettingsViewModel()
    {

        this.WhenAnyValue(model => model.Color).Subscribe(brush =>
        {
            IconColor = HandleColor(Color);
        });
        
        this.WhenAnyValue(model => model.ShapeSize).Subscribe(size =>
        {
            Size = HandleSize(ShapeSize);
        });
    }

    [Reactive] public int Speed { get; set; } = 10;
    [Reactive] public ShapeColor Color { get; set; } = ShapeColor.Blue;
    [Reactive] public ShapeSize ShapeSize { get; set; } = ShapeSize.Medium;
    [Reactive] public int Size { get; set; } = 30;
    [Reactive] public string? IconName { get; set; }
    [Reactive] public IBrush? IconColor { get; set; }
    
    private static IBrush HandleColor(ShapeColor color)
    {
        return color switch
        {
            ShapeColor.Red => Brushes.Red,
            ShapeColor.Green => Brushes.Green,
            ShapeColor.Blue => Brushes.Blue,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
    
    private static int HandleSize(ShapeSize size)
    {
        return size switch
        {
            ShapeSize.Small => 30,
            ShapeSize.Medium => 50,
            ShapeSize.Large => 70,
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }
}