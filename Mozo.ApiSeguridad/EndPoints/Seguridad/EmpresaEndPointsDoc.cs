using Microsoft.OpenApi.Models;

namespace Mozo.Api.Seguridad;

public static partial class EmpresaEndPoints
{

    private static TBuilder SaveDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {
            opt.Summary = "Grabar empresa";
            opt.Description = "Con este endpoint puede insertar o actualizar una empresa";

            return opt;
        });

    }


    private static TBuilder DeleteDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {
            opt.Summary = "Eliminar empresa";
            opt.Description = "Con este endpoint puede eliminar una empresa por id";

            return opt;
        });

    }

    private static TBuilder GetByIdDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {
            opt.Parameters.Add(new OpenApiParameter
            {
                Name = "CoEmpresa",
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema
                {
                    Type = "int"
                },
                Description = "Codigo de la empresa"
            });

            opt.Summary = "Obtener una empresa";
            opt.Description = "Con este endpoint puede obtener una empresa por su id";

            opt.RequestBody = new OpenApiRequestBody
            {
                Description = "Obtener una empresa"

            };
            return opt;
        });
    }
    //
    private static TBuilder GetAllActivoDoc<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        return builder.WithOpenApi(opt =>
        {

            opt.Summary = "Obtener todas las empresa activas";
            opt.Description = "Con este endpoint puede obtener Obtener todas las empresa activas";

            opt.RequestBody = new OpenApiRequestBody
            {
                Description = "Obtener una empresa"

            };
            return opt;
        });
    }


}



