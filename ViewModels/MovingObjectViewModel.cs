using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using ReactiveUI;

namespace MoveObject.ViewModels;

public class MovingObjectViewModel : ReactiveObject
{
    private double _xPosition;
    private double _yPosition;
    private SolidColorBrush _color;
    private double _screenWidth = 800;
    private double _objectWidth = 10;

    public MovingObjectViewModel()
    {
        // Default değerler
        XPosition = 0;
        YPosition = 0;
        Color = SolidColorBrush.Parse("#FF0000"); // Siyah renk atanıyor.
        
        MoveCommand = ReactiveCommand.Create(MoveStep);

    }


    private async Task MoveStep()
    {
        double startX = 0;
        var duration = TimeSpan.FromSeconds(1);
        var startTime = DateTime.Now;
        
        while (DateTime.Now - startTime < duration)
        {
            double progress = (DateTime.Now - startTime).TotalMilliseconds / duration.TotalMilliseconds;
            XPosition = startX + (_screenWidth - startX) * progress;
            await Task.Delay(16); // ~60 FPS
        }
        // Nesnenin XPosition özelliğini adım büyüklüğü kadar artır
        XPosition = _screenWidth;
        
    }


    public double XPosition
    {
        get => _xPosition;
        set => this.RaiseAndSetIfChanged(ref _xPosition, value);
    }

    public double YPosition
    {
        get => _yPosition;
        set => this.RaiseAndSetIfChanged(ref _yPosition, value);
    }

    public SolidColorBrush Color
    {
        get => _color;
        set => this.RaiseAndSetIfChanged(ref _color, value);
    }

    public ReactiveCommand<Unit, Task> MoveCommand { get; set; }

}