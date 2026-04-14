namespace Mozo.ApiSeguridad.Helper
{
    public static class EndpointExtensions
    {
        //public static RouteHandlerBuilder WithNamespaceTag(this RouteHandlerBuilder builder, Type type)
        //    => builder.WithTags(type.Namespace ?? "Default");

        /// <summary>
        /// Para clases estáticas: recibe el <see cref="Type"/> explícito.
        /// </summary>
        public static RouteGroupBuilder MapWithAutoTag(
            this IEndpointRouteBuilder app,
            string prefix,
            Type endpointType,
            Action<RouteGroupBuilder> mapAction)
        {
            if (endpointType == null) throw new ArgumentNullException(nameof(endpointType));

            string ns = endpointType.Namespace;
            // Quitar prefijos del namespace
            if (ns.StartsWith("Mozo.Api."))
                ns = ns["Mozo.Api.".Length..];

            string tag = $"{ns} - {endpointType.Name.Replace("EndPoints", "")}";
            RouteGroupBuilder g = app.MapGroup(prefix).WithTags(tag);
            mapAction(g);
            return g;
        }

        //public static RouteHandlerBuilder MapPost2<TModel>(
        //         this IEndpointRouteBuilder app,
        //         string pattern,
        //         Func<TModel, Task<IResult>> handler) where TModel : IBase
        //{
        //    return app.MapPost(pattern, async (TModel model, UserClaims userContext) =>
        //    {
        //        model.AssignUserContext(userContext); 
        //        return await handler(model);
        //    }).RequireAuthorization();
        //}








        public static string FromNamespaceAndClass(Type t)
        {
            string ns = t.Namespace ?? "General";

            // Quitar prefijos del namespace
            if (ns.StartsWith("Mozo.ApiSeguridad."))
                ns = ns["Mozo.ApiSeguridad.".Length..];
            else if (ns.StartsWith("Mozo.ApiMaestro."))
                ns = ns["Mozo.ApiMaestro.".Length..];
            else if (ns.StartsWith("Mozo.ApiLogin."))
                ns = ns["Mozo.ApiLogin.".Length..];

            //

            // Tomar también el nombre de la clase
            string className = t.Name;//.Replace("Endpoints", "");

            return $"{ns.Replace(".", " / ")} - {className}";
        }
    }

}
