using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MovingObjectViewModel : ReactiveObject
{
    private double _xPosition;
    private double _yPosition;
    private SolidColorBrush? _color;
    private double _screenWidth = 800;
    private double _objectWidth = 10;
    
    [Reactive] public bool IsMovingStarted { get; set; }

    public MovingObjectViewModel()
    {
        XPosition = 0;
        Color = SolidColorBrush.Parse("#FF0000"); // kırmızı renk atanıyor.

        StartMovingCommand = ReactiveCommand.Create(() =>
        {
            IsMovingStarted = true;
        });
        
        StopMovingCommand = ReactiveCommand.Create(() =>
        {
            IsMovingStarted = false;
        });

        this.WhenAnyValue(model => model.IsMovingStarted).Subscribe(async isMovingStarted =>
        {
            if (!isMovingStarted) return;
            await MoveSquare();
        });

    }
    
    private async Task MoveSquare()
    {
        while (IsMovingStarted)
        {
            await Task.Delay(20); // Hareketin hızını ayarlamak için bekleme süresi

            // XPosition değerini artırarak kareyi hareket ettir
            XPosition += 1;

            // Eğer kare sağ kenara ulaştıysa, sol kenara dön
            if (XPosition >= 800) // Kare genişliği sabit olarak 800 varsayıldı
            {
                XPosition = 0;
            }
        }
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

    public ReactiveCommand<Unit, Unit> StartMovingCommand { get; set; }
    public ReactiveCommand<Unit, Unit> StopMovingCommand { get; set; }

}