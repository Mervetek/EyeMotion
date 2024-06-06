using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MovingObjectViewModel : ReactiveObject
{
    // Reactive attributes
    [Reactive] private bool IsMovingStarted { get; set; }
    [Reactive] private bool IsLeft { get; set; } = true;
    [Reactive] private bool IsRight { get; set; }
    [Reactive] private int Amount { get; set; } = 20;
    

    // Primitive types
    private double _xPosition;
    private double _yPosition;
    private SolidColorBrush? _color;


    public MovingObjectViewModel()
    {
        XPosition = 0;
        Color = SolidColorBrush.Parse("#FF0000"); // kırmızı renk atanıyor.

        StartMovingCommand = ReactiveCommand.Create(() => { IsMovingStarted = true; });

        StopMovingCommand = ReactiveCommand.Create(() => { IsMovingStarted = false; });

        this.WhenAnyValue(model => model.IsMovingStarted).Subscribe(async isMovingStarted =>
        {
            if (!isMovingStarted) return;
            await MoveSquare();
        });

        this.WhenAnyValue(model => model.IsLeft).Subscribe(async isLeft =>
        {
            if (isLeft)
                IsRight = false;
        });

        this.WhenAnyValue(model => model.IsRight).Subscribe(async isRight =>
        {
            if (isRight)
                IsLeft = false;
        });
        
       
    }


    private async Task MoveSquare()
    {
        while (IsMovingStarted)
        {
            if (IsLeft)
            {
                await Task.Delay(20);
                XPosition += Amount;
                UpdateEdge(XPosition);
            }

            else
            {
                await Task.Delay(20);
                XPosition -= Amount;
                if (XPosition <= 0)
                {
                    IsLeft = true;
                }
            }
        }
    }

    private void UpdateEdge(double xPosition)
    {

        if (xPosition >= 750)
        {
            IsRight = true;
        }
        else
        {
            IsLeft = true;
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