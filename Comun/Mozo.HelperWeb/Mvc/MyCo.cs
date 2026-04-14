using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Model.Seguridad;

namespace Mozo.Comun.Implement.Mvc;

public static class MyCo
{
    public static EmpresaModel Empresa => GetCredential();
    public static int? CoEmpresa => GetCredential().CoEmpresa;
    public static string NoEmpresaCorto => GetCredential().NoEmpresaCorto;
    public static string NoEmpresa => GetCredential().NoEmpresa;



    //NoWhatsapp
    //<li><a href = "@MyCo.NoUrlTwitter" target="_blank"><i class="fa fa-twitter"></i></a></li>
    //                          <li><a href = "@MyCo.NoUrlFacebook" target="_blank"><i class="fa fa-facebook "></i></a></li>
    //                          <li><a href = "@MyCo.NoUrlInstagram" target="_blank"><i class="fa fa-instagram "></i></a></li>
    //                          <li><a href = "@MyCo.NoUrlLinkedLn" target="_blank"><i class="fa fa-linkedin"></i></a></li>
    //                          <li><a href = "@MyCo.NoUrlYoutube" target="_blank"><i class="fa fa-youtube-play"></i></a></li>


    //public static int? CoPermiso { get { return GetCredential().CoPermiso; } }
    //public static int? CoPersona { get { return GetCredential().CoPersona; } }
    //public static string NoUsuario { get { return GetCredential().NoUsuario; } }
    //public static string NoClave { get { return GetCredential().NoClave; } }
    //public static string NoPersona { get { return GetCredential().NoPersona; } }
    //public static string NoApellidoP { get { return GetCredential().NoApellidoP; } }
    //public static string NoApellidoM { get { return GetCredential().NoApellidoM; } }
    //public static string NoArchivo { get { return GetCredential().NoArchivo; } }
    //public static string NoExtension { get { return GetCredential().NoExtension; } }

    //public static string NoEmpresa { get { return GetCredential().NoEmpresa; } }
    //public static string NoEmpresaCorto { get { return GetCredential().NoEmpresaCorto; } }
    ////public static string NoCelularEmpresa { get { return GetCredential().NoCelularEmpresa; } }
    ////public static string NoTelefonoEmpresa { get { return GetCredential().NoTelefonoEmpresa; } }
    ////public static string NoCorreoEmpresa { get { return GetCredential().NoCorreoEmpresa; } }
    //public static string NoArchivoEmpresa { get { return GetCredential().NoArchivoEmpresa; } }
    //public static string NoExtensionEmpresa { get { return GetCredential().NoExtensionEmpresa; } }

    public static void SetCredential(EmpresaModel c)
    {
        ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL, c);
        //string TxToken = UtilityJwt.GenerateToken(c);
        //SetToken(TxToken);
    }

    public static EmpresaModel GetCredential()
    {
        return ServiceProviderYo.Current.Session.SesionGet<EmpresaModel>(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL);
        //return Current.Session.SesionGet<GlobalSessionModel>(EnuCommon.GLOBAL_NAME_SESSION_CREDENTIAL); 
    }

    public static GlobalMenuModel GetMenu()
    {
        return ServiceProviderYo.Current.Session.SesionGet<GlobalMenuModel>(EnuCommon.GLOBAL_NAME_SESSION_MENU);
    }

    public static void SetMenu(GlobalMenuModel menu)
    {
        ServiceProviderYo.Current.Session.SesionSet(EnuCommon.GLOBAL_NAME_SESSION_MENU, menu);
    }

}