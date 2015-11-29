using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using RestSharp;

namespace ApiTest
{
	class Program
	{
		private static void Main(string[] args)
		{

			RestRequest request;
			IRestResponse response;
			RestClient client;

			string deviceId;
			string password;
			string sessionId = "";


			Console.WriteLine("Run on local (\"l\" for local, ENTER for Azure)?");
			var tmp = Console.ReadLine();
			client = tmp == "l" ? 
				new RestClient("http://localhost:49954") 
				: new RestClient("http://qryptowire.azurewebsites.net");


			Console.WriteLine("DeviceId (hit ENTER for: \"asd\"):");
			deviceId = Console.ReadLine();
			if (deviceId == "")
			{
				deviceId = "asd";
			}
			Console.WriteLine("Password (hiy ENTER for: \"PASSWORD\"):");
			password = Console.ReadLine();
			if (password == "")
			{
				password = "PASSWORD";
			}

			Console.WriteLine("For each test input \"n\" to cancel that test or ENTER to proceed.");
			Console.WriteLine();
			Console.WriteLine("Test /Register");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest("api/Register", Method.POST);
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

			Console.WriteLine();
			Console.WriteLine("Testing /Login");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
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
				var a = new char[1];
				a[0] = '\"';
				sessionId = response.Content.Trim(a);
				Console.WriteLine(sessionId);
			}


			Console.WriteLine();
			Console.WriteLine("Testing /SendMessage");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/SendMessage/{sessionId}", Method.POST);
				var msg = new Message()
				{
					Body = "ARAGoRn",
					DateSent = DateTime.Now,
					ReceiverId = 171,
					SenderId = 171,
					SessionKey = "aaa",
					Signature = "bbb",
					Time = DateTime.Now
				};
				request.AddJsonBody(msg);
				response = client.Execute(request);


				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}


			Console.WriteLine();
			Console.WriteLine("Testing /FetchMessages");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/FetchMessages/{sessionId}", Method.GET);
				response = client.Execute(request);

				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}

			Console.WriteLine();
			Console.WriteLine("Testing /AddContact");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/AddContact/{sessionId}", Method.POST);
				var con = new Contact()
				{
					ReceiverId = 171,
					SenderId = 171,
					Name = "Papa Smurf",
					PublicKey = "keypub"
				};
				request.AddJsonBody(con);
				response = client.Execute(request);

				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}


			Console.WriteLine();
			Console.WriteLine("Testing /FetchContacts");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/FetchContacts/{sessionId}", Method.GET);
				response = client.Execute(request);

				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}
		
			Console.WriteLine();
			Console.WriteLine("Testing /GetUserId");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/GetUserId/{sessionId}", Method.GET);
				response = client.Execute(request);

				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}
		
			Console.WriteLine();
			Console.WriteLine("Testing /RegisterPushToken");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/RegisterPushToken/{sessionId}", Method.POST);
				request.AddJsonBody("ONOMATOPEJA");
				response = client.Execute(request);

				Console.WriteLine(
					"Status: "
					+ response.ResponseStatus + " | "
					+ response.StatusCode + " | "
					+ response.ErrorMessage
					);
				Console.WriteLine(response.Content);
			}

			Console.WriteLine();
			Console.WriteLine("Testing /UnRegisterPushToken");
			if (Console.ReadLine() != "n")
			{
				Console.WriteLine("Starting test...");
				request = new RestRequest($"api/UnRegisterPushToken/{sessionId}", Method.GET);
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
}
