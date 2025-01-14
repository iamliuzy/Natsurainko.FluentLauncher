﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Natsurainko.FluentLauncher.Components;
using Natsurainko.FluentLauncher.Models;
using System.Collections.Generic;
using System.Linq;

namespace Natsurainko.FluentLauncher.ViewModels.Pages.Activities;

public partial class Download : ObservableObject
{
    [ObservableProperty]
    private List<DownloadArrangement> downloadArrangements = GlobalActivitiesCache.DownloadArrangements;

    [ObservableProperty]
    private Visibility tipVisibility = GlobalActivitiesCache.DownloadArrangements.Any()
        ? Visibility.Collapsed
        : Visibility.Visible;
}
