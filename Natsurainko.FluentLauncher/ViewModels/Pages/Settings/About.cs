﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Windows.ApplicationModel;
using Windows.System;

namespace Natsurainko.FluentLauncher.ViewModels.Pages.Settings;

public partial class About : ObservableObject
{
#if MICROSOFT_WINDOWSAPPSDK_SELFCONTAINED
    [ObservableProperty]
    private string version = "2.1.5.0";
#else
    [ObservableProperty]
    private string version = string.Format("{0}.{1}.{2}.{3}",
            Package.Current.Id.Version.Major,
            Package.Current.Id.Version.Minor,
            Package.Current.Id.Version.Build,
            Package.Current.Id.Version.Revision);
#endif

#if DEBUG
    [ObservableProperty]
    private string edition = "Debug Edition";
#else
    [ObservableProperty]
    private string edition = "Release Edition";
#endif
}

public partial class About
{
    [RelayCommand]
    public async void CheckUpdate()
        => await Launcher.LaunchUriAsync(new Uri("ms-windows-store://pdp/?productid=9P4NQQXQ942P"));

    [RelayCommand]
    public async void OpenGit()
        => await Launcher.LaunchUriAsync(new Uri("https://github.com/Xcube-Studio/Fluent-Launcher"));

    [RelayCommand]
    public async void OpenAuthor()
        => await Launcher.LaunchUriAsync(new Uri("https://github.com/natsurainko"));
}