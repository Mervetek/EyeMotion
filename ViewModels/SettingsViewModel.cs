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
        
        
        this.WhenAnyValue(model => model.ShapeSize).Subscribe(size =>
        {
            Size = HandleSize(ShapeSize);
        });
    }

    [Reactive] public ShapeSize ShapeSize { get; set; } = ShapeSize.Medium;
    [Reactive] public int Size { get; set; } = 30;
    [Reactive] public string? IconName { get; set; }
    
   
    
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