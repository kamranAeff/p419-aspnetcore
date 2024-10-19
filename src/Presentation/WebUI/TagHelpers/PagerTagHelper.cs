using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebUI.TagHelpers
{
    [HtmlTargetElement("pager")]
    public class PagerTagHelper(IUrlHelperFactory factory, IActionContextAccessor ctx) : TagHelper
    {
        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("asp-page")]
        public int Page { get; set; }

        [HtmlAttributeName("asp-size")]
        public int Size { get; set; }

        [HtmlAttributeName("asp-pages")]
        public int Pages { get; set; }

        [HtmlAttributeName("asp-has-previous")]
        public bool HasPrevious { get; set; }

        [HtmlAttributeName("asp-has-next")]
        public bool HasNext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = factory.GetUrlHelper(ctx.ActionContext);

            int start = this.Page <= 3 ? 1 : this.Page - 3;
            int end = start + 10;

            if (end > this.Pages) end = this.Pages;

            output.TagName = "ul";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", $"pagination{(string.IsNullOrWhiteSpace(this.Class) ? string.Empty : $" {this.Class}")}");

            var prevLink = urlHelper.Action(new UrlActionContext
            {
                Action = "Index",
                Values = new
                {
                    page = this.Page - 1,
                    size = this.Size
                }
            });

            var nextLink = urlHelper.Action(new UrlActionContext
            {
                Action = "Index",
                Values = new
                {
                    page = this.Page + 1,
                    size = this.Size
                }
            });

            output.Content.AppendHtml($"<li class='page-item {(this.HasPrevious ? "" : " disabled")}'><a class='page-link' href='{prevLink}'><i class='fa fa-long-arrow-left'></i></a></li>");
            for (int i = start; i <= end; i++)
            {
                if (i == this.Page)
                    output.Content.AppendHtml($"<li class='page-item disabled'><a class='page-link'>{@i}</a></li>");
                else
                {
                    var navLink = urlHelper.Action(new UrlActionContext
                    {
                        Action = "Index",
                        Values = new
                        {
                            page = i,
                            size = this.Size
                        }
                    });
                    output.Content.AppendHtml($"<li class='page-item'><a class='page-link' href='{navLink}'>{i}</a></li>");
                }
            }
            output.Content.AppendHtml($"<li class='page-item {(this.HasNext ? "" : " disabled")}'><a class='page-link' href='{nextLink}'><i class='fa fa-long-arrow-right'></i></a></li>");
        }
    }
}