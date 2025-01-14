﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Natsurainko.FluentCore.Model.Install;
using Natsurainko.FluentCore.Model.Install.Vanilla;
using Natsurainko.FluentLauncher.Components.FluentCore;
using Natsurainko.FluentLauncher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MinecraftVanlliaInstaller = Natsurainko.FluentCore.Module.Installer.MinecraftVanlliaInstaller;

namespace Natsurainko.FluentLauncher.ViewModels.Pages.Installations;

public partial class Core : ObservableObject
{
    public Core()
    {
        LoadCores();
    }

    private static CoreManifest CoreManifest;

    private static readonly Regex NameRegex = new("^[^/\\\\:\\*\\?\\<\\>\\|\"]{1,255}$");

    private IEnumerable<string> CoreNames;

    public ContentDialog ContentDialog { get; set; }

    [ObservableProperty]
    private ObservableCollection<CoreManifestItem> cores;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(InstallCommand))]
    private CoreManifestItem selectedCoreManifestItem;

    [ObservableProperty]
    private ObservableCollection<ModLoader> loaders;

    [ObservableProperty]
    private ModLoader selectedModLoader;

    [ObservableProperty]
    private string filter = "Release";

    [ObservableProperty]
    private Visibility buildVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private bool nameTipOpen;

    [ObservableProperty]
    private bool enableCoreIndependent;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(InstallCommand))]
    private string coreName;

    public ObservableCollection<CoreManifestItem> GetFilteredCores()
    {
        IEnumerable<CoreManifestItem> filtered = default;

        if (Filter.Equals("Release"))
            filtered = CoreManifest.Cores.Where(x => x.Type.Equals("release"));
        else if (Filter.Equals("Snapshot"))
            filtered = CoreManifest.Cores.Where(x => x.Type.Equals("snapshot"));
        else if (Filter.Equals("Old"))
            filtered = CoreManifest.Cores.Where(x => x.Type.Contains("old_"));

        return new ObservableCollection<CoreManifestItem>(filtered);
    }

    public void LoadCores()
    {
        Task.Run(async () =>
        {
            if (CoreManifest == null)
                CoreManifest = await MinecraftVanlliaInstaller.GetCoreManifestAsync();

            var filtered = GetFilteredCores();

            App.MainWindow.DispatcherQueue.TryEnqueue(() =>
            {
                Cores = filtered;
                SelectedCoreManifestItem = filtered[0];
            });

            CoreNames = new GameCoreLocator(App.Configuration.CurrentGameFolder).GetGameCores().Select(x => x.Id);
        });
    }

    public void LoadLoaders()
    {
        Task.Run(() =>
        {
            App.MainWindow.DispatcherQueue.TryEnqueue(() => Loaders = new());

            var loaderTypes = new ModLoaderType[]
            {
                ModLoaderType.Forge,
                ModLoaderType.Fabric,
                ModLoaderType.OptiFine,
                ModLoaderType.Quilt
            };

            foreach (var loaderType in loaderTypes)
            {
                var modloader = new ModLoader(loaderType, SelectedCoreManifestItem?.Id);

                App.MainWindow.DispatcherQueue.TryEnqueue(() => Loaders.Add(modloader));
            }
        });
    }

    public bool EnableInstall()
        => SelectedCoreManifestItem != null && !NameTipOpen;

    [RelayCommand]
    public void RemoveLoader() => SelectedModLoader = null;

    [RelayCommand(CanExecute = nameof(EnableInstall))]
    public void Install()
    {
        if (SelectedModLoader?.SelectedBuild != null)
            InstallArrangement.StartNew(SelectedModLoader.SelectedBuild, CoreName, enableCoreIndependent);
        else if (SelectedCoreManifestItem != null)
            InstallArrangement.StartNew(SelectedCoreManifestItem, CoreName, enableCoreIndependent);

        ContentDialog.Hide();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(Filter))
            LoadCores();

        if (e.PropertyName == nameof(SelectedCoreManifestItem))
            LoadLoaders();

        if (e.PropertyName == nameof(SelectedModLoader))
            BuildVisibility = SelectedModLoader?.SelectedBuild == null
                ? Visibility.Collapsed
                : Visibility.Visible;

        if ((e.PropertyName == nameof(SelectedCoreManifestItem) || e.PropertyName == nameof(SelectedModLoader))
            && SelectedCoreManifestItem != null)
        {
            if (SelectedModLoader?.SelectedBuild == null)
                CoreName = SelectedCoreManifestItem.Id;
            else CoreName = $"{SelectedCoreManifestItem.Id}-" +
                    $"{SelectedModLoader?.SelectedBuild.ModLoaderType}-" +
                    $"{SelectedModLoader?.SelectedBuild.BuildVersion}";
        }

        if (e.PropertyName == nameof(CoreName))
            NameTipOpen = (CoreNames?.Contains(CoreName)).GetValueOrDefault(false) || !NameRegex.Matches(CoreName).Any();
    }
}
