using Boson.ClickUp.Net.Model;

using Microsoft.AspNetCore.WebUtilities;

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Web;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boson.ClickUp.Net
{
	public class ClickUpApi
	{
		private readonly HttpClient client;
		private readonly string OAuthToken = string.Empty;
		private string Token = string.Empty;
		private string Endpoint = "https://api.clickup.com/api/v2";
		private readonly string ApiV2Endpoint = "https://api.clickup.com/api/v2";
		private readonly string ApiMockServer = "https://a00fb6e0-339c-4201-972f-503b9932d17a.remockly.com";
		private readonly ApiKeys ApiKeys = new();

		public ClickUpApi()
		{
			var handler = new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
			};
			client = new HttpClient(handler);

			#region [ Load settings ]

			//var settingsPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(ClickUpApi)).Location);
			//var settingsFile = "appsettings.json";
			//var settingsData = File.ReadAllText(Path.Combine(settingsPath, settingsFile));
			//var settings = JsonSerializer.Deserialize<Settings>(settingsData);
			//var settings = new Settings();
			//settings.PersonalToken = "pk_57147343_2AJ8N0LLIOPU8FYDFFK2MYPQ1OD5L43L";
			//Token = settings.PersonalToken;

			#endregion [ Load settings ]
		}

		public ClickUpApi UsingMockServer()
		{
			Endpoint = ApiMockServer;
			return this;
		}

		public ClickUpApi UsingV2Api()
		{
			Endpoint = ApiV2Endpoint;
			return this;
		}

		#region [ Authorization ]

		//TODO: GetAccessToken()
		//public async Task GetAccessToken()
		//{
		//	var request = await client.PostAsync($"https://api.clickup.com/api/v2/oauth/token?client_id={ClientID}&client_secret={ClientSecret}&code={PersonalToken}", null);
		//	var response = await request.Content.ReadAsStringAsync();

		//	Console.WriteLine(response);
		//}

		public async Task<Response<User>> GetAuthorizedUser()
		{
			var response = await MakeCall(HttpMethod.Get, $"user");
			return Unwrap(response, r => r.user);
		}

		public async Task<Response<IEnumerable<Team>>> GetAuthorizedTeams()
		{
			var response = await MakeCall(HttpMethod.Get, "team");
			return Unwrap<IEnumerable<Team>>(response, r => r.teams);
		}

		#endregion [ Authorization ]

		#region [ Attachments ]

		//TODO: CreateTaskAttachment()
		//public async Task<Response<Type>> CreateTaskAttachment()
		//{
		//	var response = new Response<Type>();
		//	client.DefaultRequestHeaders.Add("Authorization", Token);

		// Build parameter string

		//	var request = await client.GetAsync("ENDPOINT");
		//	var httpResponse = await request.Content.ReadAsStringAsync();

		//	response.Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse);
		//	if (!response.Error.IsError)
		//	{
		//		var myDeserializedClass = JsonSerializer.Deserialize<ClickUpContext>(httpResponse);
		//		response.Value = myDeserializedClass.Type;
		//	}

		//	//Console.WriteLine(response);
		//	return response;
		//}

		#endregion [ Attachments ]

		#region [ Comments ]

		public async Task<Response<IEnumerable<Comment>>> GetTaskComments(string task_id, bool custom_task_ids = default, double team_id = default, int start = default, string start_id = default)
		{
			var parameters = new Dictionary<string, object>
			{
				{ nameof(team_id), team_id },
				{ nameof(custom_task_ids), custom_task_ids },
				{ nameof(start), start },
				{ nameof(start_id), start_id },
			};

			var response = await MakeCall(HttpMethod.Get, $"task/{task_id}/comment", parameters);
			return Unwrap<IEnumerable<Comment>>(response, r => r.comments);
		}

		#endregion [ Comments ]

		#region [ Spaces ]

		public async Task<Response<IEnumerable<Space>>> GetSpaces(double team_id, bool archived = false)
		{
			var parameters = new Dictionary<string, object>
			{
				{ nameof(archived), archived }
			};

			var response = await MakeCall(HttpMethod.Get, $"team/{team_id}/space", parameters);
			return Unwrap<IEnumerable<Space>>(response, r => r.spaces);
		}

		private static Response Unwrap(Response<string> wrapped)
		{
			var unwrapped = new Response();

			if (!wrapped.Success)
			{
				unwrapped.Error = wrapped.Error;
			}

			return unwrapped;
		}

		private static Response<T> Unwrap<T>(Response<string> wrapped)
		{
			var unwrapped = new Response<T>();

			if (wrapped.Success)
			{
				unwrapped.Value = JsonSerializer.Deserialize<T>(wrapped.Value);
			}
			else
			{
				unwrapped.Error = wrapped.Error;
			}

			return unwrapped;
		}

		private static Response<T> Unwrap<T>(Response<string> wrapped, Func<ClickUpContext, T> value)
		{
			var unwrapped = new Response<T>();

			if (wrapped.Success)
			{
				var context = JsonSerializer.Deserialize<ClickUpContext>(wrapped.Value);
				unwrapped.Value = value(context);
			}
			else
			{
				unwrapped.Error = wrapped.Error;
			}

			return unwrapped;
		}

		public async Task<Response<Space>> GetSpace(double space_id)
		{
			var response = await MakeCall(HttpMethod.Get, $"space/{space_id}");
			return Unwrap<Space>(response);
		}

		public async Task<Response> DeleteSpace(double space_id)
		{
			var response = await MakeCall(HttpMethod.Delete, $"space/{space_id}");
			return Unwrap(response);
		}

		#endregion [ Spaces ]

		public async Task GetUpdatedTasks()
		{
			throw new NotImplementedException();
			client.DefaultRequestHeaders.Add("Authorization", Token);
			var ListId = "YOUR_list_id_PARAMETER";
			var request = await client.GetAsync($"{Endpoint}/list/{ListId}/task?archived=false&page=0&order_by=string&reverse=true&subtasks=true&statuses=string&include_closed=true&assignees=string&tags=string&due_date_gt=0&due_date_lt=0&date_created_gt=0&date_created_lt=0&date_updated_gt=0&date_updated_lt=0&date_done_gt=0&date_done_lt=0&custom_fields=string");
			var response = await request.Content.ReadAsStringAsync();

			Console.WriteLine(response);
		}

		public ClickUpApi WithPersonalToken(string token)
		{
			Token = token;
			return this;
		}

		private async Task<Response<string>> MakeCall(HttpMethod method, string ApiCall, Dictionary<string, object>? parameters = default)
		{
			var TrueEndpoint = $"{Endpoint}/{ApiCall}";
			if (parameters != null)
			{
				var cleanParms = parameters.ToDictionary(p => p.Key, p => p.Value.ToString().ToLower());
				TrueEndpoint = QueryHelpers.AddQueryString(TrueEndpoint, cleanParms);
			}

			HttpResponseMessage? request;
			client.DefaultRequestHeaders.Add("Authorization", Token);
			switch (method)
			{
				case HttpMethod m when m == HttpMethod.Delete:
					request = await client.DeleteAsync(TrueEndpoint);
					break;
				case HttpMethod n when n == HttpMethod.Get:
				default:
					request = await client.GetAsync(TrueEndpoint);
					break;
			}
			var httpResponse = await request.Content.ReadAsStringAsync();

			var response = new Response<string>
			{
				Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse),
				Value = httpResponse
			};

			return response;
		}

		private async Task<Response<string>> MakeCall(HttpMethod method, string ApiCall, object payload)
		{
			var TrueEndpoint = $"{Endpoint}/{ApiCall}";
			var serializedPayload = JsonSerializer.Serialize(payload);
			var data = new StringContent(serializedPayload, Encoding.UTF8, "application/json");

			HttpResponseMessage? request = null;
			client.DefaultRequestHeaders.Add("Authorization", Token);
			switch (method)
			{
				case HttpMethod m when m == HttpMethod.Post:
					request = await client.PostAsync(TrueEndpoint, data);
					break;
				case HttpMethod m when m == HttpMethod.Put:
					request = await client.PutAsync(TrueEndpoint, data);
					break;
			}
			var httpResponse = await request.Content.ReadAsStringAsync();

			var response = new Response<string>
			{
				Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse),
				Value = httpResponse
			};

			return response;
		}
	}
}