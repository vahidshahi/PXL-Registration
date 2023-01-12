
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using Vahid_C_Web_Proj.Data;

namespace Vahid_C_Web_Proj.TagHelpers
{
    [HtmlTargetElement("a",Attributes = "background-color")]
   
    public class RUTagHelpers : TagHelper
    {
        private readonly AppDbContext _context;
        public string BackgroundColor { get; set; } = "danger";
        public RUTagHelpers(AppDbContext context)
        {
            _context = context;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{BackgroundColor} btn-block link-light border-info m-1 active");
           

        }
        
        
    }
}

