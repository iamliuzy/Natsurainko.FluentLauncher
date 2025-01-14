using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Natsurainko.FluentLauncher.Components.FluentCore;

namespace Natsurainko.FluentLauncher.Views.Pages.Properties;

public sealed partial class Launch : Page
{
    public Launch()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.DataContext = new ViewModels.Pages.Properties.Launch(e.Parameter as GameCore);
    }
}
