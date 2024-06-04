using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MoveObject.ViewModels;
using MoveObject.Views;
using Splat;

namespace MoveObject;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow();
            desktop.MainWindow = mainWindow;

            var viewModel = new MainWindowViewModel();
            mainWindow.DataContext = viewModel;
        }

        base.OnFrameworkInitializationCompleted();
    }
}