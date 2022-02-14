using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class ChartViewModel
    {
        public virtual string Title { get; set; }

        protected ChartViewModel()
        {
            Title = "Chart";

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(
                    this, () =>
                    {
                        ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Chart);
                    });
        }

        public static ChartViewModel Create() { return ViewModelSource.Create(() => new ChartViewModel()); }
    }
}