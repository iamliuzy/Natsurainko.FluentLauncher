using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Navigation;
using Natsurainko.FluentLauncher.Components.FluentCore;

namespace Natsurainko.FluentLauncher.Views.Pages.Properties;

public sealed partial class Mod : Page
{
    public Mod()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        this.DataContext = new ViewModels.Pages.Properties.Mod(e.Parameter as GameCore);
    }

    private void Border_PointerEntered(object sender, PointerRoutedEventArgs e)
        => (((Border)sender).FindName("DeleteButton") as Button).Visibility = Visibility.Visible;

    private void Border_PointerExited(object sender, PointerRoutedEventArgs e)
        => (((Border)sender).FindName("DeleteButton") as Button).Visibility = Visibility.Collapsed;
}
