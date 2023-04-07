using Boson.ClickUp.Net.Model;

using Microsoft.AspNetCore.WebUtilities;

using System.Text;
using System.Text.Json;

namespace Boson.ClickUp.Net
{
	public class ClickUpApi
	{
		private readonly string ApiMockServer = "https://a00fb6e0-339c-4201-972f-503b9932d17a.remockly.com";
		private readonly string ApiV2Endpoint = "https://api.clickup.com/api/v2";
		private readonly HttpClient client;
		private string Endpoint = "https://api.clickup.com/api/v2";
		private string Token = string.Empty;

		public ClickUpApi()
		{
			var handler = new SocketsHttpHandler
			{
				PooledConnectionLifetime = TimeSpan.FromMinutes(15) // Recreate every 15 minutes
			};
			client = new HttpClient(handler);
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

		public ClickUpApi WithPersonalToken(string token)
		{
			Token = token;
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

		public async Task<Response<IEnumerable<Team>>> GetAuthorizedTeams()
		{
			var response = await MakeCall(HttpMethod.Get, "team");
			return Unwrap<IEnumerable<Team>>(response, r => r.teams);
		}

		public async Task<Response<User>> GetAuthorizedUser()
		{
			var response = await MakeCall(HttpMethod.Get, $"user");
			return Unwrap(response, r => r.user);
		}

		#endregion [ Authorization ]

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

        #region [ Lists ]

		public async Task<Response<IEnumerable<List>>> GetLists (int folderId)
		{
			var response = await MakeCall(HttpMethod.Get, $"folder/{folderId}/list?archived=false");
			return Unwrap<IEnumerable<List>>(response, r => r.lists);
        }

        #endregion [ Lists ]

        #region [ Spaces ]

        public async Task<Response<Space>> CreateSpace(double team_id, Space space)
		{
			var response = await MakeCall(HttpMethod.Post, $"team/{team_id}/space", space);
			return Unwrap<Space>(response);
		}

		public async Task<Response> DeleteSpace(double space_id)
		{
			var response = await MakeCall(HttpMethod.Delete, $"space/{space_id}");
			return Unwrap(response);
		}

		public async Task<Response<Space>> GetSpace(double space_id)
		{
			var response = await MakeCall(HttpMethod.Get, $"space/{space_id}");
			return Unwrap<Space>(response);
		}

		public async Task<Response<IEnumerable<Space>>> GetSpaces(double team_id, bool archived = false)
		{
			var parameters = new Dictionary<string, object>
			{
				{ nameof(archived), archived }
			};

			var response = await MakeCall(HttpMethod.Get, $"team/{team_id}/space", parameters);
			return Unwrap<IEnumerable<Space>>(response, r => r.spaces);
		}

		public async Task<Response<Space>> UpdateSpace(double space_id, Space space)
		{
			var response = await MakeCall(HttpMethod.Put, $"space/{space_id}", space);
			return Unwrap<Space>(response);
		}

		#endregion [ Spaces ]

		#region [ Unwrap ]

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

		#endregion [ Unwrap ]

		#region [ MakeCall ]

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

		#endregion [ MakeCall ]
	}
}