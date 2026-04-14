using Microsoft.AspNetCore.Html;
using System.Text;

namespace Mozo.Comun.Implement.Mvc.Paging;

public class PagingServiceIT<T> : PagingBase<T>
{
    public override HtmlString Pager()
    {
        base.Pager();

        var s = new StringBuilder("");


        if (RowsCount == 0)
        {
            s.Append("<div class='row pt-2'>");
            s.Append("<div class='col-xl-12 fs-6 fw-bold text-gray-700'>");
            s.Append("No se encontraron registros");
            s.Append("</div>");
            s.Append("</div>");
            return new HtmlString(s.ToString());
        }

        if (SinglePage || PageCount == 1)
        {
            s.Append("<div class='row pt-2'>");
            s.Append("<div class='col-xl-12 fs-6 fw-bold text-gray-700'>");
            s.Append(string.Concat("Se encontraron ", RowsCount, " registros"));
            s.Append("</div>");
            s.Append("</div>");
            return new HtmlString(s.ToString());
        }
        //<ul class="custom-pagination-style-1 pagination pagination-rounded pagination-md justify-content-center">
        //                  <li class="page-item"><a class="page-link" href="#"><i class="fas fa-angle-left"></i></a></li>
        //                  <li class="page-item active"><a class="page-link" href="#">1</a></li>
        //                  <li class="page-item"><a class="page-link" href="#">2</a></li>
        //                  <li class="page-item"><a class="page-link" href="#">3</a></li>
        //                  <li class="page-item"><a class="page-link" href="#"><i class="fas fa-angle-right"></i></a></li>
        //              </ul>


        s.Append("<ul id='" + IdContainer +
                 "' class='custom-pagination-style-1 pagination pagination-rounded pagination-md justify-content-center'>");


        foreach (var Page in PageList)
            if (Page.TypeButtonPagination == TypeButtonPagination.Previous)
                s.Append(string.Concat(
                    "<li class='page-item previous'><a href='#' class='page-link' data-codigo ='{\"PageIndex\":",
                    Page.Index, "}' ><i class='fas fa-angle-left'></i></a></li>"));
            else if (Page.TypeButtonPagination == TypeButtonPagination.Standart)
                s.Append(string.Concat(
                    "<li class='page-item'><a class='page-link' href='#' data-codigo='{\"PageIndex\":", Page.Index,
                    "}' >" + (Page.Index + 1) + "</a></li>"));
            else if (Page.TypeButtonPagination == TypeButtonPagination.Current)
                s.Append(string.Concat(
                    "<li class='page-item active' aria-current='page' ><a class='page-link' id='" + IdPageActive +
                    "' data-activepage='{\"PageIndex\":", Page.Index,
                    "}' href='#' >" + (Page.Index + 1) + "</a></li>"));
            else if (Page.TypeButtonPagination == TypeButtonPagination.ThreePoints)
                s.Append(string.Concat("<li class='page-item' ><a class='page-link' data-codigo='{\"PageIndex\":",
                    Page.Index, "}' href='#' >...</a></li>"));
            else if (Page.TypeButtonPagination == TypeButtonPagination.Next)
                s.Append(string.Concat(
                    "<li class='page-item next'><a class='page-link' href='#' data-codigo ='{\"PageIndex\":",
                    Page.Index, "}' ><i class='fas fa-angle-right'></i></a></li>"));

        s.Append("</ul>");

        //s.Append("</div>");
        //s.Append("</nav>");

        return new HtmlString(s.ToString());
    }
}