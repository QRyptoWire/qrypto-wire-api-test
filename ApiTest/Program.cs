using System;
using RestSharp;

namespace ApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var LOCAL = false;
			const string deviceId = "YrdJwOphEfbeDK3itnxSBa3Ldj0=";
			const string password = "PASSWORD";
			Console.WriteLine("Testing /Register");
			var client = !LOCAL ?
				new RestClient("http://qryptowire.azurewebsites.net")
				: new RestClient("http://localhost:49954");

			var request = new RestRequest("api/Register", Method.POST);
			request.AddParameter("DeviceId", deviceId);
			request.AddParameter("Password", password);

			// execute the request
			var response = client.Execute(request);
			Console.WriteLine(
				"Status: "
				+ response.ResponseStatus + " | "
				+ response.StatusCode + " | "
				+ response.ErrorMessage
				);
			Console.WriteLine(response.Content);

			Console.WriteLine("Testing /Login");
			request = new RestRequest("api/Login", Method.POST);
			request.AddParameter("DeviceId", deviceId);
			request.AddParameter("Password", password);
			// execute the request
			response = client.Execute(request);

			Console.WriteLine(
				"Status: " 
				+ response.ResponseStatus + " | "
				+ response.StatusCode + " | "
				+ response.ErrorMessage
				);
			Console.WriteLine(response.Content);
		}
	}
}
