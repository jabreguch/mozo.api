using Microsoft.AspNetCore.Html;
using System.Text;

namespace Mozo.Comun.Implement.Mvc;

public class Paging<T> : PagingBase<T>
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

        s.Append("<nav aria-label='Paginación de " + IdContainer + "' class='row pt-2'>");


        s.Append(
            "<div class='col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'>");
        s.Append("<div class='dataTables_length'>");
        s.Append("<label>");
        s.Append("<select name='" + IdSelectSize + "' id='" + IdSelectSize +
                 "' aria-controls='kt_customers_table' class='form-select form-select-sm form-select-solid'>");
        s.Append("<option " + (PageSize == 10 ? "selected=selected" : "") + " value='10'>10</option>");
        s.Append("<option " + (PageSize == 20 ? "selected=selected" : "") + " value='20'>20</option>");
        s.Append("<option " + (PageSize == 50 ? "selected=selected" : "") + " value='50'>50</option>");
        s.Append("<option " + (PageSize == 100 ? "selected=selected" : "") + " value='100'>100</option>");
        s.Append("</select>");
        s.Append("</label>");
        s.Append("</div>");
        s.Append("<div class='dataTables_info' role='status' aria-live='polite'>" + string.Concat("Se encontraron ",
            RowsCount, " registros. (Página ", PageIndex + 1, " de ", PageCount, ")") + "</div>");
        s.Append("</div>");

        s.Append(
            "<div class='col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'>");

        s.Append("<ul id='" + IdContainer + "' class='pagination'>");


        foreach (var Page in PageList)
            if (Page.TypeButtonPagination == TypeButtonPagination.Previous)
                s.Append(string.Concat(
                    "<li class='page-item previous'><a href='#' class='page-link' data-codigo ='{\"PageIndex\":",
                    Page.Index, "}' ><i class='previous'></i></a></li>"));
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
                    Page.Index, "}' ><i class='next'></i></a></li>"));

        s.Append("</ul>");

        s.Append("</div>");
        s.Append("</nav>");

        return new HtmlString(s.ToString());
    }
}