
using ReactiveUI.Fody.Helpers;

namespace EmdrProject.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive] public MovingObjectViewModel MovingObject { get; set; } = new();
}