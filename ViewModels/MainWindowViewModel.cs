using ReactiveUI.Fody.Helpers;

namespace MoveObject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive] public MovingObjectViewModel MovingObject { get; set; } = new();
}