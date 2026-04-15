
using Dapper;

using Microsoft.AspNetCore.Http;

using System.Collections.Concurrent;
using System.Data;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using System.Xml.Linq;

namespace Mozo.Helper.Global;

//public static  class Glo2
//{
//    public static IList<T> Clone2<T>(this IList<T> listToClone) where T : ICloneable
//    {
//        return (listToClone.Select(item => (T)item.Clone()).ToList());
//    }
//}
public static partial class Glo
{
    public static readonly IEnumerable<string> MimeTypesCompress = new[]
    {
        "image/svg+xml",
        "application/atom+xml",
        // General
        "text/plain",
        // Static files
        "text/css",
        "application/javascript",
        // MVC
        "text/html",
        "application/xml",
        "text/xml",
        "application/json",
        "text/json"
    };

    public static string ToParam(this object obj)
    {
        IEnumerable<string> properties = from p in obj.GetType().GetProperties()
                                         where p.GetValue(obj, null) != null
                                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)!.ToString());

        return string.Join("&", properties.ToArray());
    }

    //public static List<T> AsNotNull<T>(this List<T> original)
    //{
    //    if (original == null)
    //        return new List<T>();
    //    return original;
    //}

    //public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> original)
    //{
    //    return original ?? Enumerable.Empty<T>();
    //}

    public static string Serializa(this object? value)
    {
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = false,
            //IgnoreNullValues = true
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(value, options);
    }

    public static List<T>? DeserializaLista<T>(this string data)
    {
        if (data.EsNulo()) return new List<T>();
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            //IgnoreNullValues = true
        };
        return JsonSerializer.Deserialize<List<T>>(data, options);
    }

    public static T? Deserializa<T>(this string data)
    {
        JsonSerializerOptions options = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        //var options = new JsonSerializerOptions
        //{
        //  PropertyNameCaseInsensitive = true // 👈 esto hace que ignore mayúsculas/minúsculas
        //};


        return JsonSerializer.Deserialize<T>(data, options);
    }

    public static T? DeserializaByte<T>(this byte[] bytee)
    {
        if (bytee == null || bytee.Length == 0)
            return default!;

        JsonSerializerOptions options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };

        return JsonSerializer.Deserialize<T>(bytee, options);
    }

    public static async Task<T?> DeserializaStream<T>(this Stream stream)
    {
        if (stream == null || stream.CanRead == false)
            return default!;

        var options = new JsonSerializerOptions
        {
            AllowTrailingCommas = true
        };

        return await JsonSerializer.DeserializeAsync<T>(stream, options);
    }

    public static async Task<string?> StreamToStringAsync(Stream stream)
    {
        string? content = null;

        if (stream != null)
            using (var sr = new StreamReader(stream))
            {
                content = await sr.ReadToEndAsync();
            }

        return content;
    }

    public static string GetQueryString2(this object obj, string? prefix = null)
    {
        List<string> list = new List<string>();
        PropertyInfo[] propertyLst = obj.GetType().GetProperties();
        foreach (PropertyInfo p in propertyLst)
        {
            object? v = p.GetValue(obj, null);
            if (v != null && v.NoNulo())
            {
                string k = prefix != null ? prefix + "." + p.Name : p.Name;
                string pr = (v != null && v.GetType().Name != "String" && v.GetType().Name != "Int32") ?
                    GetQueryString2(v, k) :
                    Uri.EscapeDataString(k) + "=" + Uri.EscapeDataString(v.Text());
                list.Add(pr);
            }
        }
        return string.Join("&", list.ToArray());
    }

    //public static string GetQueryString3(this object obj, string? prefix = null)
    //{
    //    List<string> list = new List<string>();
    //    PropertyInfo[] propertyLst = obj.GetType().GetProperties();
    //    foreach (PropertyInfo p in propertyLst)
    //    {
    //        object? v = p.GetValue(obj, null);
    //        if (v != null && v.NoNulo())
    //        {
    //            string k = prefix != null ? prefix + "." + p.Name : p.Name;
    //            if (v != null && (v.GetType().Name == "String" || v.GetType().Name == "Int32"))
    //            {
    //                string pr = Uri.EscapeDataString(k) + "=" + Uri.EscapeDataString(v.Text());
    //                list.Add(pr);
    //            }
    //        }
    //    }
    //    return string.Join("&", list.ToArray());
    //}

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
    }


    public static T? ClaimsCredential<T>(this T model, HttpContext ctx)
    {

        List<string> values = new() { "NoUsuario", "CoEmpresa", "CoPersona", "CoPermiso", "CoIngreso" };

        if (ctx.User != null)
        {
            IEnumerable<Claim> claims = ctx.User.Claims;
            if (claims != null)
            {
                PropertyInfo? propertyCredential = model!.GetType().GetProperty("Credencial");
                object credencial = Activator.CreateInstance(propertyCredential!.PropertyType)!;
                propertyCredential.SetValue(model, credencial, null);
                foreach (Claim claim in claims)
                {
                    string? key = values.Find(x => x.Equals(claim.Type));
                    if (key != null)
                    {
                        PropertyInfo? property = credencial!.GetType().GetProperty(key);
                        if (property != null)
                        {
                            object? value = claim.Value;
                            var propertyType = property.PropertyType;
                            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
                            if (value != null) value = Convert.ChangeType(value, targetType!);
                            property.SetValue(credencial, value, null);

                        }
                    }
                }
            }
        }

        return model;
    }



    //public static T GetBasicAttr<T>(this T obj)
    //{
    //    var InfoList = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
    //        .Where(p => p.GetCustomAttributes(typeof(BasicAttrAttribute), false).Count() == 1);

    //    var NewObjec = Activator.CreateInstance<T>();
    //    //T NewObjec = new  default(T); // obj.GetType();

    //    var t = NewObjec.GetType();

    //    foreach (var Info in InfoList)
    //    {
    //        var Value = Info.GetValue(obj, null);

    //        var propertyType = Info.PropertyType;
    //        var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
    //        if (Value != null) Value = Convert.ChangeType(Value, targetType);

    //        var PropertySource = t.GetProperty(Info.Name);

    //        PropertySource.SetValue(NewObjec, Value, null);
    //    }

    //    //object ret = prop != null ? prop.GetValue(obj, null) : null;

    //    return NewObjec;
    //}



    //public static T SetValueDefault<T>(this T Target, object Source)
    //{
    //    var tTarget = Target.GetType();
    //    var tSource = Source.GetType();
    //    string[] PropertyCol =
    //    {
    //        "CoEmpresa", "CoUsuarioLogin", "CoEstReg", "FlSitReg", "FeCre", // Property to audity
    //        "PageSize", "PageIndex", "RowsCount", // Property to pagination
    //        "NoArchivo", "NoExtension","NoInputSearch" // Other
    //    };

    //    foreach (var NoProperty in PropertyCol)
    //    {
    //        var PropertyTarget = tTarget.GetProperty(NoProperty);
    //        var PropertySource = tSource.GetProperty(NoProperty);

    //        if (PropertyTarget != null && PropertySource != null)
    //        {
    //            var Value = PropertySource.GetValue(Source, null);

    //            var propertyType = PropertyTarget.PropertyType;
    //            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
    //            if (Value != null) Value = Convert.ChangeType(Value, targetType);
    //            PropertyTarget.SetValue(Target, Value, null);
    //        }
    //    }
    //    return Target;
    //}

    /*
    public static (DynamicParameters Parameters, string SqlArgs) Build<T>(T model, params string[] propertyNames)
    {
        DynamicParameters parameters = new();
        List<string> sqlArgs = new();
        PropertyInfo[]? props = typeof(T).GetProperties();

        foreach (string name in propertyNames)
        {
            PropertyInfo? prop = props.FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (prop != null)
            {
                object? value = prop.GetValue(model);

                if (prop.PropertyType == typeof(string))
                {
                    parameters.Add("@" + name, value, DbType.String);
                }
                else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                {
                    parameters.Add("@" + name, value, DbType.Int32);
                }
                else
                {
                    parameters.Add("@" + name, value);
                }

                sqlArgs.Add("@" + name);
            }
        }

        return (parameters, string.Join(",", sqlArgs));
    }
    */

    private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _propCache = new();

    private static Dictionary<string, PropertyInfo> GetProps(Type type)
        => _propCache.GetOrAdd(type, modelType =>
            modelType.GetProperties().ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase));

    public static (DynamicParameters Parameters, string SqlArgs) Build<T>(T model, int? coEmpresa, int? coPersona, params string[] propertyNames)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        if (propertyNames == null || propertyNames.Length == 0)
            throw new ArgumentException("Debe indicar al menos un parámetro.", nameof(propertyNames));

        DynamicParameters parameters = new();
        List<string> sqlArgs = new();
        Dictionary<string, PropertyInfo> props = GetProps(typeof(T));

        foreach (string name in propertyNames)
        {
            if (string.IsNullOrWhiteSpace(name))
                continue;

            if (name == "CoEmpresa" && coEmpresa != null)
            {
                AddParameter(parameters, name, coEmpresa, DbType.Int32);
                sqlArgs.Add("@" + name);
            }
            else if ((name == "CoUsuCre" || name == "CoUsuMod" || name == "CoUsuEli") && coPersona != null)
            {
                AddParameter(parameters, name, coPersona, DbType.Int32);
                sqlArgs.Add("@" + name);
            }
            else
            {
                if (!props.TryGetValue(name, out PropertyInfo? prop)) continue;

                object? value = prop.GetValue(model);
                DbType dbType = ToDbType(prop.PropertyType);
                AddParameter(parameters, name, value, dbType);
                sqlArgs.Add("@" + name);
            }
        }

        return (parameters, string.Join(",", sqlArgs));
    }

    public static void AddParameter(DynamicParameters parameters, string name, object? value, DbType dbType)
    {
        if (parameters == null)
            throw new ArgumentNullException(nameof(parameters));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del parámetro es obligatorio.", nameof(name));

        string parameterName = name.StartsWith("@", StringComparison.Ordinal) ? name : $"@{name}";
        parameters.Add(parameterName, value ?? DBNull.Value, dbType);
    }

    public static void ValidateUserContext(UserContext user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        user.Validate();
    }

    private static DbType ToDbType(Type propertyType)
    {
        Type type = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
        return type switch
        {
            Type t when t == typeof(string) => DbType.String,
            Type t when t == typeof(int) => DbType.Int32,
            Type t when t == typeof(decimal) => DbType.Decimal,
            Type t when t == typeof(bool) => DbType.Boolean,
            Type t when t == typeof(DateTime) => DbType.DateTime,
            Type t when t == typeof(long) => DbType.Int64,
            Type t when t == typeof(short) => DbType.Int16,
            Type t when t == typeof(double) => DbType.Double,
            Type t when t == typeof(float) => DbType.Single,
            Type t when t == typeof(Guid) => DbType.Guid,
            _ => DbType.Object
        };
    }
    /*
    public static (DynamicParameters Parameters, string SqlArgs) Build<T>(T model, params string[] propertyNames)
    {
        DynamicParameters parameters = new();
        List<string> sqlArgs = new();
        PropertyInfo[] props = typeof(T).GetProperties();

        foreach (string name in propertyNames)
        {
            PropertyInfo? prop = props.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (prop == null) continue;

            object? value = prop.GetValue(model);
            Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            DbType dbType = type switch
            {
                Type t when t == typeof(string) => DbType.String,
                Type t when t == typeof(int) => DbType.Int32,
                Type t when t == typeof(decimal) => DbType.Decimal,
                Type t when t == typeof(bool) => DbType.Boolean,
                Type t when t == typeof(DateTime) => DbType.DateTime,
                Type t when t == typeof(long) => DbType.Int64,
                _ => DbType.Object
            };

            parameters.Add("@" + name, value, dbType);
            sqlArgs.Add("@" + name);
        }

        return (parameters, string.Join(",", sqlArgs));
    }
  
    */
    public static T Mapper<T>(this object obj) //where T : new()
    {
        if (obj == null)
            return default!;
        //Assembly assem = typeof(T).Assembly;
        ////T instance = null System.DBNull.Value;
        ////T instance;

        //return obj.GetType().MakeByRefType();

        Type t = obj.GetType();


        //return Activator.CreateInstance<T>();


        //return (T)new object { };


        IEnumerable<PropertyInfo> InfoList = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //.Where(p => p.GetCustomAttributes(typeof(BasicAttrAttribute), false).Count() == 1);

        T NewObjec = Activator.CreateInstance<T>();
        //T NewObjec = new  default(T); // obj.GetType();

        Type t2 = NewObjec!.GetType();

        foreach (var Info in InfoList)
        {
            var Value = Info.GetValue(obj, null);

            var propertyType = Info.PropertyType;
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            if (Value != null) Value = Convert.ChangeType(Value, targetType);

            PropertyInfo PropertySource = t2.GetProperty(Info.Name)!;

            PropertySource!.SetValue(NewObjec, Value, null);
        }

        //object ret = prop != null ? prop.GetValue(obj, null) : null;

        return NewObjec;
    }


    //public static int? EstadoPagina<T>(this T obj)
    //{
    //    Type t = null;
    //    //int? CoEstadoPagina = null;
    //    if (obj == null) return null;

    //    t = obj.GetType();
    //    var property = t.GetProperty("CoEstadoPagina");
    //    if (property != null)
    //    {
    //        var valueOrigin = property.GetValue(obj, null);
    //        if (valueOrigin != null)
    //        {
    //            if (int.Parse(valueOrigin.ToString()) == EnuCommon.EstadoPagina.Nuevo)
    //                return EnuCommon.EstadoPagina.Nuevo;
    //            if (int.Parse(valueOrigin.ToString()) == EnuCommon.EstadoPagina.Edicion)
    //                return EnuCommon.EstadoPagina.Edicion;
    //        }
    //    }

    //    return null;
    //}

    //public static T StateModel<T>(this T obj)
    //{
    //    Type t = null;
    //    int? CoEstadoPagina = null;
    //    if (obj == null)
    //    {
    //        obj = Activator.CreateInstance<T>();
    //        t = obj.GetType();
    //        CoEstadoPagina = EnuCommon.EstadoPagina.Nuevo;
    //    }
    //    else
    //    {
    //        t = obj.GetType();
    //        CoEstadoPagina = EnuCommon.EstadoPagina.Edicion;
    //    }

    //    var PropertyTarget = t.GetProperty("CoEstadoPagina");
    //    if (PropertyTarget != null) PropertyTarget.SetValue(obj, CoEstadoPagina, null);
    //    return obj;
    //}

    public static T With<T>(this T item, Action<T> action)
    {
        action(item);
        return item;
    }

    //public static T AssignNotNullProperties<T>(this T origin, object assign)
    //{
    //    var tOrigin = origin.GetType();
    //    var tAssign = assign.GetType();
    //    IEnumerable<PropertyInfo> PropertyList = tAssign.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //    foreach (var Property in PropertyList)
    //    {
    //        var PropertyOrigin = tOrigin.GetProperty(Property.Name);

    //        if (PropertyOrigin != null && PropertyOrigin.PropertyType.IsClass == false)// .FullName == Property.PropertyType.FullName
    //        {
    //            var ValueOrigin = PropertyOrigin.GetValue(origin, null);
    //            if (ValueOrigin == null)
    //            {
    //                var ValueAssign = Property.GetValue(assign, null);
    //                var propertyAssignType = Property.PropertyType;
    //                var AssignType = IsNullableType(propertyAssignType)
    //                    ? Nullable.GetUnderlyingType(propertyAssignType)
    //                    : propertyAssignType;
    //                //if (!Property.GetType().IsClass)
    //                //{
    //                if (ValueAssign != null)
    //                {
    //                    ValueAssign = Convert.ChangeType(ValueAssign, AssignType);
    //                    PropertyOrigin.SetValue(origin, ValueAssign, null);
    //                }
    //                //}
    //            }
    //        }
    //    }

    //    return origin;
    //}

    //[AttributeUsage(AttributeTargets.Property)]
    //public class BasicAttrAttribute : Attribute
    //{
    //}

    //[AttributeUsage(AttributeTargets.Property)]
    //public class LangAttribute : Attribute
    //{
    //}




    //public class SwaaggerParameterAttribute : Attribute
    //{
    //    public SwaaggerParameterAttribute(string name, string value)
    //    {
    //        Name = name;
    //        Value = value;
    //    }

    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //}

    //public class ParameterFilter : IParameterFilter
    //{
    //    public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
    //    {
    //        IEnumerable<SwaaggerParameterAttribute>? parameterAttributes = null;

    //        if (context.PropertyInfo != null)
    //        {
    //            parameterAttributes = context.PropertyInfo.GetCustomAttributes<SwaaggerParameterAttribute>();

    //        }
    //        else if (context.ParameterInfo != null)
    //        {
    //            parameterAttributes = context.ParameterInfo.GetCustomAttributes<SwaaggerParameterAttribute>();
    //        }

    //        if (parameterAttributes != null)
    //        {
    //            Add(parameter, parameterAttributes);
    //        }


    //    }

    //    private void Add(OpenApiParameter parameter, IEnumerable<SwaaggerParameterAttribute> parameterAttributes)
    //    {

    //        foreach (SwaaggerParameterAttribute item in parameterAttributes)
    //        {
    //            OpenApiExample i = new OpenApiExample
    //            {
    //                Value = new OpenApiString(item.Value)
    //            };
    //            parameter.Examples.Add(item.Name, i);
    //        }
    //    }



    //}


}
