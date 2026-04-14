using Microsoft.AspNetCore.Mvc;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Http;
using Mozo.Model;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mozo.Comun.Implement.Mvc;

public class BaseController : Controller
{
    private readonly IHttpClientSeguridad _httpClientSeguridad;
    private readonly IHttpClientMaestro _httpClientMaestro;
    private readonly ConfigurationModel _configuration;
    public BaseController() { }


    public BaseController(
        IHttpClientSeguridad httpClientSeguridad
        , IHttpClientMaestro httpClientMaestro
        , ConfigurationModel configuration)
    {
        _httpClientMaestro = httpClientMaestro;
        _httpClientSeguridad = httpClientSeguridad;
        _configuration = configuration;
    }
    public BaseController(IHttpClientMaestro httpClientMaestro)
    {
        _httpClientMaestro = httpClientMaestro;
    }

    public BaseController(ConfigurationModel configuration)
    {
        _configuration = configuration;
    }

    public void MsgExito(string msg = "Se guardaron los cambios.")
    {
        ObjectResult2(200, msg);
    }

    public void MsgInformacion(string msg)
    {
        ObjectResult2(100, msg);
    }

    public void MsgPeligro(string msg)
    {
        ObjectResult2(500, msg);
    }

    public void MsgAdvertencia(string msg)
    {
        ObjectResult2(400, msg);
    }

    //protected ObjectResult BadRequest2(string msg = "Incorrect information") => ObjectResult2(400, msg);
    //protected ObjectResult NotFound2(string msg = "Record not found") => ObjectResult2(404, msg);
    private void ObjectResult2(int codeStatusHttp, string msg)
    {
        var http = Glo.StatusHttp(codeStatusHttp);

        var problem = new ProblemDetails();
        problem.Status = int.Parse(http[0]);
        problem.Title = http[1];
        problem.Instance = Request.Path;
        problem.Type = http[2];
        problem.Detail = msg;

        //ProblemDetails problem= new ProblemDetails(msg, Request.Path, int.Parse(http[0]), http[1], http[2]);
        Yo.AnadirMensaje(problem);
        //Yo.AnadirMensaje(new MensajeModel() { NoMensaje = _msg, CoTipoMensaje = EnuCommon.CoTipoMensaje.Exito });
    }

    //public static string MsgExito(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = EnuCommon.CoTipoMensaje.Exito }.Serializa();
    //}

    //public static string MsgInformacion(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = EnuCommon.CoTipoMensaje.Informacion }.Serializa();
    //}
    //public static string MsgAdvertencia(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = EnuCommon.CoTipoMensaje.Advertencia }.Serializa();
    //}

    //public static string MsgPeligro(string msg = null)
    //{
    //    return new MensajeModel() { NoMensaje = msg, CoTipoMensaje = EnuCommon.CoTipoMensaje.Peligro }.Serializa();
    //}

    public async Task<(EmpresaModel, EmpresaModel, ModuloModel)> GetEmpresaAndModulo(EmpresaModel EmpresaCliente = null)
    {
        EmpresaModel empresaPrincipal = await _httpClientSeguridad.One<EmpresaModel>("Cuenta/EmpresaApi/GetById", new EmpresaModel() { CoEmpresa = _configuration.AppSettings.CoEmpresaPrincipal }, Security.Anonymous);
        //        @CoEmpresa INT,
        //@CoOrigen Int,
        // @CoClasificacion Int,
        //   @CoPersona INT
        //AS
        //--@CoOrigenTelCor = 1: Empresa, 2:Persona




        empresaPrincipal.TelCorLst = (await _httpClientMaestro.All<IEnumerable<TelCorModel>>("TelCorApi/GetAllActivo", new TelCorModel()
        {
            CoEmpresa = _configuration.AppSettings.CoEmpresaPrincipal,
            CoOrigen = 1
        }, Security.Anonymous)).ToList();

        empresaPrincipal.SocialLst = (await _httpClientMaestro.All<IEnumerable<SocialModel>>("Maestro/SocialApi/GetAllActivo", new SocialModel()
        {
            CoEmpresa = _configuration.AppSettings.CoEmpresaPrincipal,
            CoOrigen = 1
        }, Security.Anonymous)).ToList();

        if (EmpresaCliente != null)
        {
            EmpresaCliente = await _httpClientSeguridad.One<EmpresaModel>("Cuenta/EmpresaApi/GetById", EmpresaCliente, Security.Anonymous);
        }
        ModuloModel Modulo = await _httpClientSeguridad.One<ModuloModel>("Cuenta/ModuloApi/GetById", new ModuloModel() { CoModulo = _configuration.CoModulo }, Security.Anonymous);

        return new(empresaPrincipal, EmpresaCliente, Modulo);
    }

    public async Task<UploadModel> FileUpload(UploadModel file)
    {
        var Tipo = await _httpClientMaestro.One<TipoGeneralModel>("TipoGeneralApi/GetById", new TipoGeneralModel
        {
            CoModulo = file.TipoArchivo.CoModulo,
            CoTipo = file.TipoArchivo.CoTipoFormato,
            CoGrupo = file.TipoArchivo.CoGrupo
        }, Security.Anonymous);

        if (Tipo != null) file.TxAccept = Tipo.NoTipo.Mayuscula();

        file.FlMultiple = file.TipoArchivo.FlMultiple;
        file.FlPreview = file.TipoArchivo.FlPreview;
        file.TxTitle = file.TipoArchivo.TxTitulo;

        file.CoSendTo = file.TipoArchivo.CoSendTo;
        return file;
    }
}