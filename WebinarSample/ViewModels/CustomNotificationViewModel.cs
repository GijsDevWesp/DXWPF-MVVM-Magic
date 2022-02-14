﻿using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webinar.ViewModels
{
    [POCOViewModel]
    public class CustomNotificationViewModel
    {
        public virtual string Caption { get; set; }

        public virtual string Content { get; set; }
    }
}
