﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Natsurainko.FluentLauncher.Components;
using Natsurainko.FluentLauncher.Models;
using Natsurainko.FluentLauncher.Views.Pages;
using System.Collections.Generic;
using System.Linq;

namespace Natsurainko.FluentLauncher.ViewModels.Pages.Activities;

public partial class Launch : ObservableObject
{
    [ObservableProperty]
    private List<LaunchArrangement> launchArrangements = GlobalActivitiesCache.LaunchArrangements;

    [ObservableProperty]
    private Visibility tipVisibility = GlobalActivitiesCache.LaunchArrangements.Any()
        ? Visibility.Collapsed
        : Visibility.Visible;

    [RelayCommand]
    public void Home() => MainContainer.ContentFrame.Navigate(typeof(Views.Pages.Home));
}
