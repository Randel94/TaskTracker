using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskTracker.Middlewares.Swagger
{
    public class CustomSwaggerOperationAttribute : IOperationFilter
    {
        private static readonly Dictionary<string, string> DEFAULT_RESPONSE_CODE = new() { { "200", "Успешно" } };


        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Get Authorize attribute
            var attributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (attributes.Any())
            {
                var attr = attributes.ToList()[0];

                // Add what should be show inside the security section
                IList<string> securityInfos = new List<string>();
                securityInfos.Add($"{nameof(AuthorizeAttribute.Policy)}:{attr.Policy}");
                securityInfos.Add($"{nameof(AuthorizeAttribute.Roles)}:{attr.Roles}");
                securityInfos.Add($"{nameof(AuthorizeAttribute.AuthenticationSchemes)}:{attr.AuthenticationSchemes}");
                foreach (var apiResponse in DEFAULT_RESPONSE_CODE)
                {
                    if (operation.Responses.ContainsKey(apiResponse.Key))
                    {
                        operation.Responses[apiResponse.Key].Description = apiResponse.Value;
                    }
                }

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new()
                    {
                        [
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header
                            }
                        ] = new[] { "Market.Api" }
                    }
                };
            }
            else
            {
                operation.Security.Clear();
            }
        }

        private static Dictionary<string, OpenApiMediaType> GetDefaultContent()
        {
            return new Dictionary<string, OpenApiMediaType> { ["application/json"] = new OpenApiMediaType() };
        }
    }
}