using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Webinar
{
    public enum NavigationKey
    {
        Dashboard,
        Chart,
        Extra
    }

    public static class Regions
    {
        public static string Content { get { return "ContentRegion"; } }

        public static string Navigation { get { return "NavigationRegion"; } }
    }

    public class DataSeries
    {
        public string Name { get; set; }

        public ObservableCollection<DataPoint> Values { get; set; }
    }

    public class DataPoint
    {
        public DateTime Argument { get; set; }

        public double Value { get; set; }

        public DataPoint(DateTime argument, double value)
        {
            Argument = argument;
            Value = value;
        }
    }
}
