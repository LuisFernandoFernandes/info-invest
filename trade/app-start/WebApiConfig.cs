using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using trade.application.injector;
using trade.application.routes;
using trade.application.validations;

namespace trade
{
    public static class WebApiConfig
    {
        public static async void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());
            config.EnableCors(true);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            Json(config);
            InitNinject(config);

            config.Filters.Add(new HandlerErrorExceptionFilterAttribute());
        }

        private static void Json(HttpConfiguration config)
        {
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.Culture = new CultureInfo("pt-BR");
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm";
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        }

        public static void InitNinject(HttpConfiguration config)
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(LoadNinjectModules());
            config.DependencyResolver = new DependencyInjector(kernel);
        }

        public static IEnumerable<System.Reflection.Assembly> LoadNinjectModules() { return AppDomain.CurrentDomain.GetAssemblies(); }

    }
}
