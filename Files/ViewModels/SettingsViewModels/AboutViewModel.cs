﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Uwp.Extensions;
using System;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace Files.ViewModels.SettingsViewModels
{
    public class AboutViewModel : ObservableObject
    {
        public RelayCommand OpenLogLocationCommand => new RelayCommand(() => SettingsViewModel.OpenLogLocation());

        public RelayCommand<ItemClickEventArgs> ClickAboutFeedbackItemCommand =>
            new RelayCommand<ItemClickEventArgs>(ClickAboutFeedbackItem);

        public string Version
        {
            get
            {
                var version = Package.Current.Id.Version;
                return string.Format($"{"SettingsAboutVersionTitle".GetLocalized()} {version.Major}.{version.Minor}.{version.Build}.{version.Revision}");
            }
        }

        private async void ClickAboutFeedbackItem(ItemClickEventArgs e)
        {
            var clickedItem = (StackPanel)e.ClickedItem;
            switch (clickedItem.Tag)
            {
                case "Feedback":
                    SettingsViewModel.ReportIssueOnGitHub();
                    break;

                case "ReleaseNotes":
                    await Launcher.LaunchUriAsync(new Uri(@"https://github.com/files-community/files-uwp/releases"));
                    break;

                case "Contributors":
                    await Launcher.LaunchUriAsync(new Uri(@"https://github.com/files-community/files-uwp/graphs/contributors"));
                    break;

                case "SupportUs":
                    await Launcher.LaunchUriAsync(new Uri(@"https://paypal.me/yaichenbaum"));
                    break;

                default:
                    break;
            }
        }
    }
}