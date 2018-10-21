using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hus.MvcWebUI.Entity
{
    public enum EnumOrderState
    {
        [Display(Name = "Waiting for approval")]
        Waiting,
        [Display(Name = "Completed")]
        Completed
    }
}