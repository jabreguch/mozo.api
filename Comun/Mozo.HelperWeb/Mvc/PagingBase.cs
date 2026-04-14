using Microsoft.AspNetCore.Html;
using Mozo.Comun.Helper.Global;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.Comun.Implement.Mvc;

public static class TypeButtonPagination
{
    public const int Standart = 0;
    public const int Current = 1;
    public const int ThreePoints = 2;
    public const int Next = 3;
    public const int Previous = 4;
}

public class PageModel
{
    public int? Index { get; set; }
    public int TypeButtonPagination { get; set; }
}

public class PagingBase<T> : IEnumerable<T>
{
    private string pagerId;
    public IEnumerable<T> items { get; set; }


    public List<PageModel> PageList { get; set; }
    public bool SinglePage { get; set; } = false;

    public int? PageCount { get; set; }
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? RowsCount { get; set; }

    public string PagerId
    {
        get
        {
            if (pagerId.EsNulo()) return "Tbl" + DateTime.Now.ToString("yyyyMMddHHmiss");

            if (pagerId.IndexOf("Tbl") > -1)
                return pagerId;
            return "Tbl" + pagerId;
        }

        set => pagerId = value;
    }

    public string IdContainer => "PgContainer" + PagerId;

    public string IdPageActive => "PgActive" + PagerId;

    public string IdSelectSize => "PgSize" + PagerId;


    public IEnumerator<T> GetEnumerator()
    {
        var count = items.Count();
        for (var i = 0; i < count; i++)
            yield return items.ElementAt(i); // [i];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public virtual HtmlString Pager()
    {
        RowsCount = items.Count();

        if (RowsCount.Equals(0))
        {
        }
        else
        {
            var Item = items.First();

            var type = Item.GetType();
            var prop = type.GetProperty("RowsCount");

            if (prop != null)
            {
                var Value = prop.GetValue(Item, null);
                if (Value != null)
                {
                    RowsCount = (int)Value;
                    PageCount = (int)Math.Ceiling((decimal)(RowsCount ?? default) / (PageSize ?? default));
                }
            }
        }


        var pageList = new List<PageModel>();

        // function printPageLinksFinalReally(num totalPages, num currentPage)
        if (PageCount > 1)
        {
            // return


            if (PageIndex + 1 > 1)
                //print "Prev"
                pageList.Add(new PageModel
                { Index = PageIndex - 1, TypeButtonPagination = TypeButtonPagination.Previous });

            //print "1"
            pageList.Add(new PageModel
            {
                Index = 0,
                TypeButtonPagination = PageIndex == 0 ? TypeButtonPagination.Current : TypeButtonPagination.Standart
            });


            if (PageIndex + 1 > 2)
            {
                //print "..."
                pageList.Add(new PageModel { Index = 1, TypeButtonPagination = TypeButtonPagination.ThreePoints });
                if (PageIndex + 1 == PageCount && PageCount > 3)
                    //print PageIndex -2
                    pageList.Add(new PageModel
                    { Index = PageIndex - 2, TypeButtonPagination = TypeButtonPagination.Standart });
                //print PageIndex -1
                pageList.Add(new PageModel
                { Index = PageIndex - 1, TypeButtonPagination = TypeButtonPagination.Standart });
            }


            if (PageIndex + 1 != 1 && PageIndex + 1 != PageCount)
                //print currentPage
                pageList.Add(new PageModel { Index = PageIndex, TypeButtonPagination = TypeButtonPagination.Current });

            if (PageIndex + 1 < PageCount - 1)
            {
                //print currentPage + 1
                pageList.Add(new PageModel
                { Index = PageIndex + 1, TypeButtonPagination = TypeButtonPagination.Standart });
                if (PageIndex + 1 == 1 && PageCount > 3)
                    // print currentPage + 2
                    pageList.Add(new PageModel
                    { Index = PageIndex + 2, TypeButtonPagination = TypeButtonPagination.Standart });
                //print "..."
                pageList.Add(new PageModel
                { Index = PageCount - 2, TypeButtonPagination = TypeButtonPagination.ThreePoints });
            }

            //print PageCount
            pageList.Add(new PageModel
            {
                Index = PageCount - 1,
                TypeButtonPagination = PageIndex == PageCount - 1
                    ? TypeButtonPagination.Current
                    : TypeButtonPagination.Standart
            });


            if (PageIndex + 1 < PageCount)
                //print "Next"
                pageList.Add(new PageModel { Index = PageIndex + 1, TypeButtonPagination = TypeButtonPagination.Next });
        }

        PageList = pageList;

        return new HtmlString("");
    }
}