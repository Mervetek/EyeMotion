using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace EmdrProject.Views;

public partial class MovingObjectView : UserControl
{
    public MovingObjectView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}