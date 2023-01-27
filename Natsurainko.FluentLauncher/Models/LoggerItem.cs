﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Natsurainko.FluentCore.Model.Launch;
using Natsurainko.FluentLauncher.Components.Logger;

namespace Natsurainko.FluentLauncher.Models;

public partial class LoggerItem : ObservableObject
{
    public LoggerItem(GameProcessOutput gameProcessOutput)
    {
        var richTextBlock = new RichTextBlock();
        LoggerLanguage.Formatter.FormatRichTextBlock(gameProcessOutput.FullData, new LoggerLanguage(), richTextBlock);
        RichTextBlock = richTextBlock;

        ErrorVisibility = (gameProcessOutput.Level == GameProcessOutputLevel.Error || gameProcessOutput.Level == GameProcessOutputLevel.Fatal)
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    [ObservableProperty]
    private RichTextBlock richTextBlock;

    [ObservableProperty]
    private Visibility errorVisibility;
}