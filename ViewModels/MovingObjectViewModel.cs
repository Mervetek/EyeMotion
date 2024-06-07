using System;
using System.Reactive;
using System.Threading.Tasks;
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



    // Primitive types
    private double _xPosition;
    private double _yPosition;


    public MovingObjectViewModel()
    {
        XPosition = 0;
        _yPosition = 220;

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
        
        this.WhenAnyValue(model => model.Speed).Subscribe(async speed =>
        {
           
              Console.WriteLine(speed);
        });
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
            if (CurrentCycle >= RepeatCount)
            {
                IsMovingStarted = false;
                XPosition /= 2;
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
}