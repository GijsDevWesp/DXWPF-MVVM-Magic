using DevExpress.Mvvm.DataAnnotations;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class CustomNotificationViewModel
    {
        public virtual string Caption { get; set; }
        public virtual string Content { get; set; }
        public virtual string Time { get; set; }
    }
}
