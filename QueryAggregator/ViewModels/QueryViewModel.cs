using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace QueryAggregator.ViewModels
{
    public class QueryViewModel
    {
        [Display(Name = "Query")]
        public string Query { get; set; }
    }
}