using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentApp.WebClient.Code
{
    public static class ExtensionMethods
    {
        public static IEnumerable<SelectListItem> ToSelectList(this List<Data.Models.ListItem> items, int? id = null, bool includeBlank = false){
            return items.Select(x => new SelectListItem {
                Text = x.Text,
                Value = x.Id.ToString(),
                Selected = (id.HasValue && int.Equals(x.Id, id.Value))
                });
            }
    }
}