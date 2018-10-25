using System.Web.Http;
using CompanyX.PaymentGateway.WebApi.Formatters;

namespace CompanyX.PaymentGateway.WebApi
{
	public static class WebApiConfig
	{
		public static object HttpConfig { get; private set; }

		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			config.Formatters.Remove(config.Formatters.XmlFormatter);  //remove original

			As400XmlMediaTypeFormatter formatter = new As400XmlMediaTypeFormatter();
			formatter.UseXmlSerializer = true;

			config.Formatters.Add(formatter); //add modified

			// ...
		}
	}
}
