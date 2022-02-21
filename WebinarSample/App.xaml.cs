using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using System;
using System.Windows;
using Webinar.ViewModels;
using Webinar.Views;

namespace Webinar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            ApplicationThemeHelper.ApplicationThemeName = Theme.DefaultThemeName;
            var splashScreenViewModel = new DXSplashScreenViewModel() { 
                Title = "Control Center",
                IsIndeterminate = true,
                Subtitle = "Powered by DevExpress",
                Copyright = "© WESP automotive",
                Logo = new Uri("../../Images/wesp-logo-transparant.png", UriKind.Relative)
            };
            SplashScreenManager.Create(() => new CustomSplashScreen(), splashScreenViewModel).ShowOnStartup();
            InitViewInjection();
        }

        void InitViewInjection()
        {
            ViewInjectionManager.Default
                .Inject(Regions.Content,
                NavigationKey.Dashboard, 
                () => TrackListViewModel.Create(), 
                typeof(TrackListView));
            ViewInjectionManager.Default
                .Inject(Regions.Navigation,
                NavigationKey.Dashboard,
                () => NavigationItemViewModel.Create("Dashboard", NavigationKey.Dashboard),
                typeof(NavigationItemView));

            ViewInjectionManager.Default
                .Inject(Regions.Content,
                NavigationKey.Chart,
                () => ChartViewModel.Create(),
                typeof(ChartView));
            ViewInjectionManager.Default
                .Inject(Regions.Navigation,
                NavigationKey.Chart,
                () => NavigationItemViewModel.Create("Chart", NavigationKey.Chart),
                typeof(NavigationItemView));

            ViewInjectionManager.Default
                .Inject(Regions.Content,
                NavigationKey.Extra,
                () => ExtraViewModel.Create(),
                typeof(ExtraView));
            ViewInjectionManager.Default
                .Inject(Regions.Navigation,
                NavigationKey.Extra,
                () => NavigationItemViewModel.Create("Extra", NavigationKey.Extra),
                typeof(NavigationItemView));
        }
    }
}
