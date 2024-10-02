using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;

public class FormFileOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.RequestBody != null && context.ApiDescription.ParameterDescriptions.Any())
        {
            var properties = typeof(Create.Request).GetProperties();
            foreach (var property in properties)
            {
                var schema = new OpenApiSchema
                {
                    Type = GetOpenApiType(property.PropertyType)
                };

                if (property.PropertyType.IsEnum)
                {
                    var enumValues = Enum.GetValues(property.PropertyType)
                        .Cast<Enum>()
                        .Select(e => new OpenApiString(e.ToString()))
                        .Cast<IOpenApiAny>() // Converte para IOpenApiAny
                        .ToList();

                    schema.Enum = enumValues;
                    schema.Type = "string"; 
                }

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = property.Name,
                    In = ParameterLocation.Query, // Usar Query ou RequestBody conforme necessário
                    Required = true, // Ou false, conforme sua necessidade
                    Schema = schema
                });
            }
        }
    }

    private static string GetOpenApiType(Type type)
    {
        if (type.IsEnum)
            return "string";
        if (type == typeof(string))
            return "string";
        if (type == typeof(DateTime))
            return "string"; // Considere se DateTime deveria ser tratado de forma diferente
        if (type == typeof(int))
            return "integer";
        if (type == typeof(List<string>))
            return "array";

        return "object";
    }
}