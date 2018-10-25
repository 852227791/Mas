using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mas.Tool.Web
{
    //public class AspNetCoreMvcExtensions
    //{
    //         public static IServiceCollection ConfigureMvcServices(this IServiceCollection services)
    //    {
    //        services.AddMvc(option => { option.Filters.Add(new PermissionFilter()); })
    //            .AddJsonOptions(options =>
    //                {
    //                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    //                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    //                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
    //                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    //                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    //                }
    //            );
    //        return services;
    //    }
    //}
}
