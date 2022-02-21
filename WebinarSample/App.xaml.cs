using DevExpress.Mvvm;
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
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
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
