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
        this.WhenAnyValue(model => model.ShapeType).Subscribe(type =>
        {
            IconName = HandleShapeType(type);
        });

        this.WhenAnyValue(model => model.Color).Subscribe(brush =>
        {
            IconColor = HandleColor(Color);
        });
    }

    [Reactive] public int Speed { get; set; } = 10;
    [Reactive] public ShapeColor Color { get; set; } = ShapeColor.Blue;
    [Reactive] public ShapeType ShapeType { get; set; } = ShapeType.Circle;
    [Reactive] public int RepeatCount { get; set; } = 30;
    [Reactive] public string? IconName { get; set; }
    [Reactive] public IBrush? IconColor { get; set; }


    private static string HandleShapeType(ShapeType type)
    {
        return type switch
        {
            ShapeType.Triangle => "avares://EmdrProject/Assets/triangle.png",
            ShapeType.Rectangle => "avares://EmdrProject/Assets/rectangle.png",
            ShapeType.Circle => "avares://EmdrProject/Assets/circle.png",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

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
}