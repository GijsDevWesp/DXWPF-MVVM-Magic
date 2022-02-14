using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
