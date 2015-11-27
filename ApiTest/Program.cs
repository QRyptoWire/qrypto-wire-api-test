using System;
using RestSharp;

namespace ApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Testing /Register");
			var client = new RestClient("http://qryptowire.azurewebsites.net/");
			var request = new RestRequest("api/Register/asdasdad", Method.GET).AddParameter("deviceId", "sdasdasd");
			// execute the request
			var response = client.Execute(request);
			Console.WriteLine(response.Content);
			Console.WriteLine("Testing /Login");
			client = new RestClient("http://qryptowire.azurewebsites.net/");
			request = new RestRequest("api/Login/asdasdad", Method.GET).AddParameter("deviceId", "sdasdasd");
			// execute the request
			response = client.Execute(request);
			Console.WriteLine(response.Content);
		}
	}
}
