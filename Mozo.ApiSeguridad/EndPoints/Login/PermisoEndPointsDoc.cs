namespace Mozo.Api.Login;

public static partial class PermisoEndPoints
{

    private static TBuilder GetByIdDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {
            //opt.Parameters.Add(
            //        new()
            //        {
            //            Name = "CoEmpresa",
            //            In = ParameterLocation.Query,
            //            Schema = new() { Type = "int" },
            //            Description = "Codigo de la empresa"
            //        });

            //opt.Parameters.Add(
            //      new()
            //      {
            //          Name = "CoPermiso",
            //          In = ParameterLocation.Query,
            //          Schema = new() { Type = "int" },
            //          Description = "Codigo del permiso"
            //      });


            opt.Summary = "Obtener permiso por id";
            opt.Description = "Con este endpoint puede permiso por id";


            return opt;
        });

    }

    private static TBuilder DocGetByUser<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {

            //opt.Parameters.Add(
            //        new() {
            //            Name = "CoEmpresa",
            //            In = ParameterLocation.Query,                           
            //            Schema =new() { Type ="int" },
            //            Description = "Codigo de la empresa"
            //        });

            //opt.Parameters.Add(
            //      new()
            //      {
            //          Name = "CoModulo",
            //          In = ParameterLocation.Query,
            //          Schema = new() { Type = "int" },
            //          Description = "Codigo del módulo"
            //      });

            //opt.Parameters.Add(
            //      new()
            //      {
            //          Name = "NoUsuario",
            //          In = ParameterLocation.Query,
            //          Schema = new OpenApiSchema { Type = "string" },
            //          Description = "Nombre del usuario"
            //      });

            //opt.Parameters.Add(
            //        new()
            //        {
            //            Name = "NuDocumento",
            //            In = ParameterLocation.Query,
            //            Schema = new OpenApiSchema { Type = "string" },
            //            Description = "Número de documento"
            //        });

            //opt.Parameters.Add(
            //     new()
            //     {
            //         Name = "NoClave",
            //         In = ParameterLocation.Query,
            //         Schema = new OpenApiSchema { Type = "string" },
            //         Description = "Clave de la cuenta"
            //     });



            opt.Summary = "Obtener usuario";
            opt.Description = "Con este endpoint obtiene el usuario";

            return opt;
        });

    }

    private static TBuilder UpdateLanguageDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {
            opt.Summary = "Actualizar el lenguaje ";
            opt.Description = "Con este endpoint puede actualizar el lenguaje";

            return opt;
        });

    }


}



