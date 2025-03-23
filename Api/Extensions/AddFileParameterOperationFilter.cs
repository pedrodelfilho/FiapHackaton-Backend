using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class AddFileParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            var requestFormConsumes = context.ApiDescription.ActionDescriptor.EndpointMetadata
                .OfType<ConsumesAttribute>()
                .SelectMany(attr => attr.ContentTypes)
                .ToList();

            if (requestFormConsumes.Count > 0)
            {
                foreach (var parameter in operation.Parameters)
                {
                    var isFileParameter = parameter.Schema.Type == "file";
                    if (isFileParameter)
                    {
                        parameter.In = ParameterLocation.Header;
                        parameter.Description = "Upload file.";
                        parameter.Required = true;
                        parameter.Schema.Type = "string";
                        parameter.Schema.Format = "binary";
                    }
                }

                var requestFormConsumesSet = new HashSet<string>(requestFormConsumes);
                var formConsumes = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties =
                    {
                        ["files"] = new OpenApiSchema
                        {
                            Description = "Upload file.",
                            Type = "array",
                            Items = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        }
                    }
                    }
                };

                foreach (var response in operation.Responses.Values)
                {
                    var responseContent = response.Content;
                    var formConsumesList = new List<OpenApiMediaType>();

                    foreach (var consume in requestFormConsumesSet)
                    {
                        if (responseContent.ContainsKey(consume))
                        {
                            var content = responseContent[consume];
                            content.Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties =
                            {
                                ["files"] = new OpenApiSchema
                                {
                                    Description = "Upload file.",
                                    Type = "array",
                                    Items = new OpenApiSchema
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                }
                            }
                            };
                        }
                    }
                }
            }
        }
    }
}
