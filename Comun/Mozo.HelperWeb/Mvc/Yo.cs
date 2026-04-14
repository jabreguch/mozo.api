using Microsoft.AspNetCore.Mvc;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.Comun.Implement.Mvc;

public static class Yo
{
    public static int? CoEmpresa => GetCredential()?.CoEmpresa;
    public static int? CoPersona => GetCredential()?.CoPersona;
    public static string NoPersona => GetCredential()?.NoPersona;
    public static string NoApellidoP => GetCredential()?.NoApellidoP;
    public static string NoApellidoM => GetCredential()?.NoApellidoM;
    public static string NoArchivo => GetCredential()?.NoArchivo;
    public static string NoExtension => GetCredential()?.NoExtension;

    public static void AnadirMensaje(ProblemDetails i)
    {
        var ProblemDetailsList = new List<ProblemDetails>();
        if (ServiceProviderYo.Current.Session.TryGetValue(EnuCommon.GLOBAL_NAME_SESSION_MESSAGE, out var bytee))
            ProblemDetailsList = bytee.ByteToListObject<ProblemDetails>();
        ProblemDetailsList.Add(i);
        ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_MESSAGE, ProblemDetailsList);
    }

    public static string GetScriptLanguage()
    {
        return "MzGetLang('" + GetCredential().Permiso.CoLang + "');";
    }

    public static GlobalCredentialModel GetCredential()
    {
        return ServiceProviderYo.Current.Session.SesionGet<GlobalCredentialModel>(EnuCommon
            .GLOBAL_NAME_SESSION_CREDENTIAL);
    }

    public static void SetCredential(GlobalCredentialModel credential)
    {
        ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL, credential);
    }

    public static GlobalMenuModel GetMenu()
    {
        return ServiceProviderYo.Current.Session.SesionGet<GlobalMenuModel>(EnuCommon.GLOBAL_NAME_SESSION_MENU);
    }

    public static void SetMenu(GlobalMenuModel menu)
    {
        ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_MENU, menu);
    }

    //public static GlobalTokenModel GetToken() => ServiceProviderYo.Current.Session.SesionGet<GlobalTokenModel>(EnuCommon.GLOBAL_NAME_SESSION_TOKEN);
    //public static void SetToken(GlobalTokenModel token) => ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_TOKEN, token);

    public static bool FindPage(string NoArea, string NoControlador, string NoAccion)
    {
        NoArea = NoArea.Minuscula();
        NoControlador = NoControlador.Minuscula();
        NoAccion = NoAccion.Minuscula();

        var SessionMenu = GetMenu();

        var Fl = false;

        foreach (var ModuloUsuario in SessionMenu.ModuloUsuarioCol)
            foreach (var Menu in ModuloUsuario.MenuCol.AsNotNull())
            {
                var TmpPagina = Menu.PaginalCol.AsNotNull().Find(x =>
                    x.NoArea.Minuscula() == NoArea && x.NoControlador.Minuscula() == NoControlador &&
                    (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                if (TmpPagina != null)
                    Fl = true;
                else
                    foreach (var Pagina in Menu.PaginalCol)
                    {
                        var TmpPaginaFlotante = Pagina.PaginaFlotanteCol.AsNotNull().Find(x =>
                            x.NoArea.Minuscula() == NoArea && x.NoControlador.Minuscula() == NoControlador &&
                            (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                        if (TmpPaginaFlotante != null) Fl = true;
                        var TmpServicioWeb = Pagina.ServicioWebCol.AsNotNull().Find(x =>
                            x.NoControlador.Minuscula() == NoControlador &&
                            (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                        if (TmpServicioWeb != null) Fl = true;

                        var TmpSubPagina = Pagina.SubPaginaCol.AsNotNull().Find(x =>
                            x.NoArea.Minuscula() == NoArea && x.NoControlador.Minuscula() == NoControlador &&
                            (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                        if (TmpPagina != null)
                            Fl = true;
                        else
                            foreach (var SubPagina in Pagina.SubPaginaCol.AsNotNull())
                            {
                                var TmpSubPaginaFlotante = SubPagina.PaginaFlotanteCol.AsNotNull().Find(x =>
                                    x.NoArea.Minuscula() == NoArea && x.NoControlador.Minuscula() == NoControlador &&
                                    (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                                if (TmpSubPaginaFlotante != null) Fl = true;
                                var TmpSubServicioWeb = SubPagina.ServicioWebCol.AsNotNull().Find(x =>
                                    x.NoControlador.Minuscula() == NoControlador &&
                                    (x.NoAccion.Minuscula() == NoAccion || x.NoParametro == "*"));
                                if (TmpSubServicioWeb != null) Fl = true;
                            }
                    }
            }

        return Fl;
    }

    public static void SetPaginaActual(PaginaModel c, SubPaginaModel d)
    {
        if (c != null)
        {
            var SessionMenu = GetMenu();
            SessionMenu.PaginaSeleccionada = c;
            SessionMenu.SubPaginaSeleccionada = d;
            SetMenu(SessionMenu);
        }
    }

    public static class Empresa
    {
        public static string NoEmpresa => GetCredential()?.Empresa?.NoEmpresa;
        public static string NoEmpresaCorto => GetCredential()?.Empresa?.NoEmpresaCorto;
        public static string NoArchivo => GetCredential()?.Empresa?.NoArchivo;
        public static string NoExtension => GetCredential()?.Empresa?.NoExtension;
    }

    public static class Permiso
    {
        public static int? CoPermiso => GetCredential()?.Permiso?.CoPermiso;
        public static string NoUsuario => GetCredential()?.Permiso?.NoUsuario;

        public static string CoLang => GetCredential()?.Permiso?.CoLang;
        //public static string NoClave { get { return GetCredential().Permiso.NoClave; } }
    }

    public static class Ingreso
    {
        public static int? CoIngreso => GetCredential()?.Ingreso?.CoIngreso;
    }
}