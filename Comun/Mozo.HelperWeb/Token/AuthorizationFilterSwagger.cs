using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

using Swashbuckle.AspNetCore.SwaggerGen;


namespace Mozo.HelperWeb.Token;

//SwaggerDoc
public class AuthorizationFilterSwagger : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<AuthorizeAttribute>().Any())
            return;

        // ✅ Se usa OpenApiSecuritySchemeReference como clave del diccionario
        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecuritySchemeReference("Bearer"),
                new List<string>()
            }
        };

        operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
    }
}
