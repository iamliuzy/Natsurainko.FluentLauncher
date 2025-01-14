﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Natsurainko.FluentCore.Interface;
using Natsurainko.FluentLauncher.Components.Mvvm;
using Natsurainko.FluentLauncher.Views.Pages;
using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;

namespace Natsurainko.FluentLauncher.ViewModels.Dialogs;

public partial class ExceptionDialogViewModel : DialogViewModel
{
    [ObservableProperty]
    string errorMessage;

    public ExceptionDialogViewModel(string errorMessage = "")
    {
        this.errorMessage = errorMessage;
    }

    [RelayCommand]
    async void CopyAndLaunchGitHub()
    {
        // Copy error message
        var package = new DataPackage();
        package.SetText(errorMessage);
        Clipboard.SetContent(package);

        // Launch GitHub
        await Launcher.LaunchUriAsync(new Uri("https://github.com/Xcube-Studio/Natsurainko.FluentLauncher/issues/new/choose"));
    }
}