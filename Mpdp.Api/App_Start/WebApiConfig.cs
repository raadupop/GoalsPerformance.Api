﻿using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using Mpdp.Api.Infrastructure.Binders;
using Mpdp.Api.Infrastructure.MessageHandlers;
using Newtonsoft.Json;

namespace Mpdp.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.EnableCors();

      // Web API configuration and services
      config.MessageHandlers.Add(new MpdpAuthHandler());
      config.Services.Insert(typeof(ModelBinderProvider), 0, new SimpleModelBinderProvider(typeof(TimeSpan), new IsoTimeSpanModelBinder()));
      
      config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

      var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
      json.UseDataContractJsonSerializer = true;

      // Web API routes

      // config.MapHttpAttributeRoutes();
      config.EnableCors();

      config.Routes.MapHttpRoute(
        name: "ActionApi",
        routeTemplate: "{controller}/{action}/{id}",
        defaults: new { id = RouteParameter.Optional, extension = RouteParameter.Optional });

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "{controller}/{id}",
          defaults: new { id = RouteParameter.Optional });
    }
  }
}
