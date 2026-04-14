using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Model.Control;
using Mozo.Model.Maestro;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mozo.Comun.Implement.Mvc;

public static class Html2
{
    public static HtmlString Codigo(this object CustomAttributes)
    {
        return new HtmlString(CustomAttributes.Serializa());
    }

    public static HtmlString StateRow(this int? coEstReg, string txTooltip = null)
    {
        ButtonModel Btn = new(EnuCommon.CoTipoBoton.Check);
        if (txTooltip.NoNulo())
        {
            Btn.TxTooltip = txTooltip;
            Btn.TxTooltipCustom = txTooltip;
        }
        Btn.CoButtonSize = EnuCommon.ButtonSize.OnlyIcon;
        return new HtmlString(coEstReg == 1 ? Btn.TxIconoDefault("pe-2") : Btn.TxIconoCustom("pe-2"));
    }

    public static HtmlString HTMLSelectTipoGeneral(this IEnumerable<TipoGeneralModel> list, string id,
        object customAtributes, int? CoValueSelect, bool FlSigla = false, string textNull = null)
    {
        var sb = new StringBuilder("<select ");
        sb.Append("id=\"" + id + "\" ");
        sb.Append("name=\"" + id + "\" ");
        sb.Append("class=\"form-select\" ");

        var attributos = HtmlHelper.AnonymousObjectToHtmlAttributes(customAtributes);


        foreach (var property in attributos) sb.Append(property.Key + "=\"" + property.Value + "\" ");

        sb.Append(">");

        if (textNull != null) sb.Append("<option value=\"\" >" + textNull + "</option>");

        if (CoValueSelect == null || CoValueSelect == -1)
        {
            var TipoDefault = list.ToList().Find(x => x.FlDefault == 1);
            if (TipoDefault != null) CoValueSelect = TipoDefault.CoTipo;
        }

        foreach (var item in list)
            if (FlSigla == false)
                sb.Append("<option " + (item.CoTipo == CoValueSelect ? "selected=\"selected\"" : "") + " title=\"" +
                     item.TxDescripcion + "\" value=\"" + item.CoTipo + "\" >" + item.NoTipo + "</option>");
            else
                sb.Append("<option " + (item.CoTipo == CoValueSelect ? "selected=\"selected\"" : "") + " title=\"" +
                          item.NoTipo + "\" value=\"" + item.CoTipo + "\" >" + item.NoSigla + "</option>");

        sb.Append("</select>");
        return new HtmlString(sb.ToString());
    }

    public static HtmlString HTMLSelectTipo(this IEnumerable<TipoModel> list, string id, object customAtributes,
        int? CoValueSelect = null, bool FlSigla = false, bool FlValue = false, string textNull = null,
        bool flGroup = false)
    {
        var sb = new StringBuilder("<select ");
        sb.Append("id=\"" + id + "\" ");
        sb.Append("name=\"" + id + "\" ");
        sb.Append("class=\"form-select\" ");
        //           <optgroup label="Swedish Cars">
        //  <option value="volvo">Volvo</option>
        //  <option value="saab">Saab</option>
        //</optgroup>
        var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(customAtributes);

        foreach (var property in attrs) sb.Append(property.Key + "=\"" + property.Value + "\" ");

        sb.Append(">");
        //return new SelectList(list, "CoSubTipo", "NoSubTipo", CoTipo, "NoTipo");
        if (CoValueSelect == null)
        {
            var TipoDefault = list.ToList().Find(x => x.FlDefault == 1);
            if (TipoDefault != null) CoValueSelect = TipoDefault.CoTipo;
        }

        if (textNull != null) sb.Append("<option value=\"\" >" + textNull + "</option>");

        if (flGroup == false)
        {
            foreach (var item in list)
                if (FlSigla == false)
                    sb.Append("<option " + (item.CoTipo == CoValueSelect ? "selected=\"selected\"" : "") + " value=\"" +
                              item.CoTipo + "\" >" + item.NoTipo + (FlValue ? " => " + item.NoValor : "") +
                              "</option>");
                else
                    sb.Append("<option " + (item.CoTipo == CoValueSelect ? "selected=\"selected\"" : "") + " title=\"" +
                              item.NoTipo + "\" value=\"" + item.CoTipo + "\" >" + item.NoSigla +
                              (FlValue ? " => " + item.NoValor : "") + "</option>");
        }
        else
        {
            var GroupLst = list.GroupBy(x => new { x.CoTipo, x.NoTipo })
                .Select(y => new TipoModel { CoTipo = y.Key.CoTipo, NoTipo = y.Key.NoTipo });
            foreach (var itemGroup in GroupLst)
            {
                sb.Append("<optgroup label=\"" + itemGroup.NoTipo + "\">");
                foreach (var item in list.Where(x => x.CoTipo == itemGroup.CoTipo))
                    if (FlSigla == false)
                        sb.Append("<option " + (item.CoSubTipo == CoValueSelect ? "selected=\"selected\"" : "") +
                                  " value=\"" + item.CoSubTipo + "\" >" + item.NoSubTipo +
                                  (FlValue ? " => " + item.NoValor : "") + "</option>");
                    else
                        sb.Append("<option " + (item.CoSubTipo == CoValueSelect ? "selected=\"selected\"" : "") +
                                  " title=\"" + item.CoSubTipo + "\" value=\"" + item.NoSubTipo + "\" >" +
                                  item.NoSigla + (FlValue ? " => " + item.NoValor : "") + "</option>");
                sb.Append("</optgroup>");
            }
        }

        sb.Append("</select>");
        return new HtmlString(sb.ToString());
    }


    // public static HtmlString MenuRow(this List<ButtonModel> buttonCol, string nameSpace, bool rightPosition = true)
    // {
    //     //btn btn-sm btn-icon btn-color-primary btn-active-light-primary show menu-dropdown
    //     //{(rightPosition ? "position-absolute top-0 end-0 mt-1 me-1" : "")}
    //     string DivDropDownr = @"<button class='btn btn-sm btn-icon btn-color-primary btn-active-light-primary' data-kt-menu-trigger='click' data-kt-menu-placement='bottom-end' data-kt-menu-flip='top-end'>
    //                                 <span class='svg-icon svg-icon-2'><svg xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' width='24px' height='24px' viewBox='0 0 24 24' version='1.1'><g stroke='none' stroke-width='1' fill='none' fill-rule='evenodd'><rect x='5' y='5' width='5' height='5' rx='1' fill='#000000'></rect><rect x='14' y='5' width='5' height='5' rx='1' fill='#000000' opacity='0.3'></rect><rect x='5' y='14' width='5' height='5' rx='1' fill='#000000' opacity='0.3'></rect><rect x='14' y='14' width='5' height='5' rx='1' fill='#000000' opacity='0.3'></rect></g></svg></span>
    //                             </button>
    //                             <div class='menu menu-sub menu-sub-dropdown menu-column menu-rounded menu-gray-600 menu-state-bg-light-primary fw-bold fs-7 w-200px py-4' data-kt-menu='true'>XX0XX</div>";

    //     //string LiItemSeparator = @"<div class='separator my-2'></div>";

    //     //string LiItemTitleGroup = "<div class='menu-item px-3'><div class='menu-content text-muted pb-2 px-3 fs-7 text-uppercase'>Payments</div></div>";

    //     StringBuilder sb = new StringBuilder("");

    //     foreach (ButtonModel Item in buttonCol)
    //     {
    //         // Leyenda 
    //         // {0} = TxUrl
    //         // {1} = TxTarget
    //         // {2} = TxDataCodigo
    //         // {3} = TxIconoColor
    //         // {4} = TxIcono
    //         // {5} = TxValue
    //         // {6} = TxTooltip
    //         // {7} = TxCommand


    //         if (Item.CoAppearance == EnuCommon.CoTipoBoton.TitleHeader)
    //         {
    //             sb.Append($@"<div class='menu-item px-3'><div class='menu-content text-muted pb-2 px-3 fs-7 text-uppercase'>{Item.TxValue}</div></div>");
    //         }
    //         else if (Item.CoAppearance == EnuCommon.CoTipoBoton.Separator)
    //         {
    //             sb.Append("<div class='separator my-2'></div>");
    //         }
    //         else
    //         {
    //             Item.CoButtonSize = EnuCommon.ButtonSize.OnlyIcon;
    //             string TxIcono = "", TxTarget = "", TxValue = "", TxTooltip = "", TxCommand = "", TxUrl = "#";
    //             if (Item.CoAppearance == EnuCommon.CoTipoBoton.Check)
    //             {

    //                 int CoEstReg = EstadoChek(Item.CustomAtributes, out object customAttibutesNew);
    //                 Item.CustomAtributes = customAttibutesNew;
    //                 TxValue = CoEstReg == 0 ? Item.TxValueCustom : Item.TxValue;
    //                 TxTooltip = CoEstReg == 0 ? Item.TxIconoCustom() : Item.TxTooltip;
    //                 TxIcono = CoEstReg == 0 ? Item.TxIconoCustom() : Item.TxIconoDefault();
    //             }
    //             else
    //             {

    //                 TxValue = Item.TxValue;
    //                 TxTooltip = Item.TxTooltip;
    //                 TxIcono = Item.TxIconoDefault();
    //                 TxUrl = Item.TxUrl;
    //             };

    //             if (Item.CoAppearance == EnuCommon.CoTipoBoton.Link)
    //             {
    //                 if (Item.CoLinkTarget == EnuCommon.LinkTarget.Blank)
    //                 {
    //                     TxTarget = "target='_blank'";
    //                 }
    //                 if (Item.CoLinkTarget == EnuCommon.LinkTarget.Self)
    //                 {
    //                     TxTarget = "target='_self'";
    //                 }
    //             }

    //             Item.TxDataCodigo = Item.CustomAtributes != null ? Newtonsoft.Json.JsonConvert.SerializeObject(Item.CustomAtributes) : "";

    //             if (Item.TxCommand != null)
    //             {
    //                 if (Item.TxNameSpace == null)
    //                 {
    //                     TxCommand = Item.TxCommand + "-" + nameSpace;
    //                 }
    //                 else
    //                 {
    //                     TxCommand = Item.TxCommand + "-" + Item.TxNameSpace;
    //                 }
    //             }


    //             if (Item.TxUrl != null)
    //             {


    //             }
    //             else
    //             {


    //             }


    //             sb.Append($@"<div class='menu-item px-3'>
    //    <a href='{TxUrl}' {TxTarget} data-codigo='{Item.TxDataCodigo}' data-toggle='{TxCommand}' class='menu-link flex-stack px-3'>                                    
    //                             {TxValue}
    //                             {TxIcono}
    //                         </a>
    //</div>");
    //         }
    //     }

    //     // XX0XX
    //     return new HtmlString(DivDropDownr.Replace("XX0XX", sb.ToString()));

    // }
    // private static int EstadoChek(object CustomAtributes, out object CustomAtributesNew)
    // {
    //     int CoEstReg = -1;
    //     dynamic jsonObj = null;
    //     if (CustomAtributes != null)
    //     {
    //         string output = Newtonsoft.Json.JsonConvert.SerializeObject(CustomAtributes);
    //         jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(output);
    //         if (jsonObj["CoEstReg"] != null)
    //         {
    //             CoEstReg = (int)jsonObj["CoEstReg"];
    //         }
    //         else { CoEstReg = 0; }
    //         // CoEstReg= CoEstReg == 1 ? 0 : 1;
    //         jsonObj["CoEstReg"] = CoEstReg == 1 ? 0 : 1;

    //     }
    //     CustomAtributesNew = jsonObj;
    //     //ButtonModel Boton = new ButtonModel(CoEstReg == 1 ? EnuCommon.CoTipoBoton.Inactivo : EnuCommon.CoTipoBoton.Activo);// { CoTipoBoton = CoEstReg == 1 ? EnuCommon.CoTipoBoton.Inactivo : EnuCommon.CoTipoBoton.Activo };
    //     //Boton.CustomAtributes = CustomAtributes;
    //     //return Boton;
    //     return CoEstReg == 1 ? 0 : 1;

    // }
    // public static TagBuilder button(this ButtonModel c)
    // {
    //     TagBuilder tag = new TagBuilder("button");
    //     if (c.CoTypeButton == EnuCommon.TypeButton.Button)
    //     {
    //         tag.Attributes.Add("type", "button");
    //     }
    //     else if (c.CoTypeButton == EnuCommon.TypeButton.Submit)
    //     {
    //         tag.Attributes.Add("type", "submit");
    //     }
    //     else if (c.CoTypeButton == EnuCommon.TypeButton.Reset)
    //     {
    //         tag.Attributes.Add("type", "reset");
    //     }


    //     if (c.CoAppearance == EnuCommon.CoTipoBoton.Check)
    //     {
    //         //string Value = c.TxValue, Tooltip = c.TxTooltip;
    //         int CoEstReg = EstadoChek(c.CustomAtributes, out object customAttributeNew);
    //         c.CustomAtributes = customAttributeNew;
    //         //c.TxValue = Value;
    //         //c.TxTooltip = Tooltip;

    //         tag.Attributes.Add("data-toggle", c.TxCommand);
    //         tag.Attributes.Add("title", CoEstReg == 0 ? c.TxTooltip : c.TxTooltipCustom);    // c.CoTipoBoton == EnuCommon.CoTipoBoton.Activo ? "¿Desactivar?" : "¿Activar?");

    //         tag.InnerHtml.AppendHtmlLine(CoEstReg == 0 ? c.TxIconoDefault() : c.TxIconoCustom());

    //     }
    //     else
    //     {
    //         if (c.TxCommand != null)
    //         {
    //             tag.Attributes.Add("data-toggle", c.TxCommand);
    //         }
    //         tag.Attributes.Add("class", c.TxCss);
    //         tag.Attributes.Add("title", c.TxTooltip);

    //         if (c.FlTieneIcono == true)
    //         {
    //             tag.InnerHtml.AppendHtmlLine(c.TxIconoDefault());
    //         }
    //     }


    //     if (c.CoButtonSize == EnuCommon.ButtonSize.BtnStandart || c.CoButtonSize == EnuCommon.ButtonSize.BtnSmall || c.CoButtonSize == EnuCommon.ButtonSize.BtnLg)
    //     {
    //         tag.InnerHtml.AppendHtml(" " + c.TxValue);
    //     }

    //     if (c.HtmlAttributes != null)
    //     {
    //         IDictionary<string, object> attributos = HtmlHelper.AnonymousObjectToHtmlAttributes(c.HtmlAttributes);
    //         tag.MergeAttributes(attributos);
    //     }

    //     if (c.CustomAtributes != null)
    //     {
    //         string output = Newtonsoft.Json.JsonConvert.SerializeObject(c.CustomAtributes);
    //         tag.Attributes.Add("data-codigo", output);
    //     }

    //     return tag;
    // }
}