using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using EmdrProject.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MovingObjectViewModel : ReactiveObject
{
    [Reactive] public SettingsViewModel Settings { get; set; } = new();

    // Reactive attributes
    [Reactive] private bool IsMovingStarted { get; set; }
    [Reactive] private bool IsLeft { get; set; } = true;
    [Reactive] private bool IsRight { get; set; }
    [Reactive] public int CurrentCycle { get; set; } = 0;
    [Reactive] public int RepeatCount { get; set; } = 20;
    [Reactive] public int Speed { get; set; } = 10;
    
    [Reactive] public ShapeColor Color { get; set; } = ShapeColor.Blue;
    [Reactive] public IBrush? IconColor { get; set; } = Brushes.Chocolate;



    // Primitive types
    private double _xPosition;
    private double _yPosition;


    public MovingObjectViewModel()
    {
        XPosition = 0;
        _yPosition = 220;

        StartMovingCommand = ReactiveCommand.Create(() => { IsMovingStarted = true; });

        StopMovingCommand = ReactiveCommand.Create(() => { IsMovingStarted = false; });
        
        AdjustColorCommand = ReactiveCommand.Create<ShapeColor>(param =>
        {
            IconColor = HandleColor(param);
        });

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
    
    private static IBrush HandleColor(ShapeColor color)
    {
        switch (color)
        {
            case ShapeColor.Aqua:
                return Brushes.Aqua;
            case ShapeColor.Lime:
                return Brushes.Lime;
            case ShapeColor.Blue:
                return Brushes.Blue;
            case ShapeColor.Purple:
                return Brushes.Purple;
            case ShapeColor.DeepSkyBlue:
                return Brushes.DeepSkyBlue;
            case ShapeColor.Brown:
                return Brushes.Brown;
            case ShapeColor.MediumSlateBlue:
                return Brushes.MediumSlateBlue;
            case ShapeColor.Red:
                return Brushes.Red;
            case ShapeColor.OrangeRed:
                return Brushes.OrangeRed;
            default:
                throw new ArgumentOutOfRangeException(nameof(color), color, null);
        }
    }


    private async Task MoveSquare()
    {
        while (IsMovingStarted)
        {
            if (IsLeft)
            {
                await Task.Delay(20);
                XPosition += Speed;
                UpdateEdge(XPosition);
            }

            else
            {
                await Task.Delay(20);
                XPosition -= Speed;
                if (XPosition <= 0)
                {
                    IsLeft = true;
                }
            }
        }
    }

    private void UpdateEdge(double xPosition)
    {
        if (xPosition >= 1850)
        {
            IsRight = true;
            CurrentCycle++;
            if (CurrentCycle == RepeatCount)
            {
                IsMovingStarted = false;
                XPosition /= 2;
                CurrentCycle = 0;
            }
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

    public ReactiveCommand<Unit, Unit> StartMovingCommand { get; set; }
    public ReactiveCommand<Unit, Unit> StopMovingCommand { get; set; }
    public ReactiveCommand<ShapeColor, Unit> AdjustColorCommand { get; set; }
}