using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class ChartViewModel
    {
        public ObservableCollection<DataSeries> Data { get; private set; }

        protected ChartViewModel()
        {
            Data = Data = new ObservableCollection<DataSeries> {
                new DataSeries
                {
                    Name = "Blauw",
                    Values = new ObservableCollection<DataPoint> {
                        new DataPoint(new DateTime(2016, 12, 31), 362.5),
                        new DataPoint(new DateTime(2017, 12, 31), 348.4),
                        new DataPoint(new DateTime(2018, 12, 31), 279.0),
                        new DataPoint(new DateTime(2019, 12, 31), 230.9),
                        new DataPoint(new DateTime(2020, 12, 31), 203.5),
                        new DataPoint(new DateTime(2021, 12, 31), 197.1)
                    }
                },
                new DataSeries
                {
                    Name = "Rood",
                    Values = new ObservableCollection<DataPoint> {
                        new DataPoint(new DateTime(2016, 12, 31), 277.0),
                        new DataPoint(new DateTime(2017, 12, 31), 328.5),
                        new DataPoint(new DateTime(2018, 12, 31), 297.0),
                        new DataPoint(new DateTime(2019, 12, 31), 255.3),
                        new DataPoint(new DateTime(2020, 12, 31), 173.5),
                        new DataPoint(new DateTime(2021, 12, 31), 131.8)
                    }
                } };

            ViewInjectionManager.Default
                .RegisterNavigatedEventHandler(
                    this, () => {
                    ViewInjectionManager.Default.Navigate(Regions.Navigation, NavigationKey.Chart);
                });
        }

        public static ChartViewModel Create() { return ViewModelSource.Create(() => new ChartViewModel()); }
    }
}