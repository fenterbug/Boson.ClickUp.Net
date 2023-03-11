using ClickUp.Net.Model;

using System.Text;
using System.Text.Json;

namespace ClickUp.Net
{
	public class ClickUpApi
	{
		private readonly HttpClient client;
		private string OAuthToken = string.Empty;
		private string Token = string.Empty;
		private string Endpoint = "https://api.clickup.com/api/v2";
		private string ApiV2Endpoint = "https://api.clickup.com/api/v2";
		private string ApiMockServer = "https://a00fb6e0-339c-4201-972f-503b9932d17a.remockly.com";
		private ApiKeys ApiKeys = new ApiKeys();

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
			var response = new Response<User>();
			client.DefaultRequestHeaders.Add("Authorization", Token);

			var request = await client.GetAsync($"{Endpoint}/user");
			var httpResponse = await request.Content.ReadAsStringAsync();

			response.Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse);
			if (!response.Error.IsError)
			{
				var myDeserializedClass = JsonSerializer.Deserialize<ClickUpContext>(httpResponse);
				response.Value = myDeserializedClass.user;
			}

			//Console.WriteLine(response);
			return response;
		}

		public async Task<Response<IEnumerable<Team>>> GetAuthorizedTeams()
		{
			var response = new Response<IEnumerable<Team>>();
			client.DefaultRequestHeaders.Add("Authorization", Token);

			var request = await client.GetAsync($"{Endpoint}/team");
			var httpResponse = await request.Content.ReadAsStringAsync();

			response.Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse);
			if (!response.Error.IsError)
			{
				var myDeserializedClass = JsonSerializer.Deserialize<ClickUpContext>(httpResponse);
				response.Value = myDeserializedClass.teams;
			}

			//Console.WriteLine(response);
			return response;
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
			var response = new Response<IEnumerable<Comment>>();
			client.DefaultRequestHeaders.Add("Authorization", Token);

			#region [ Build parameter string ]

			StringBuilder parmBuilder = new StringBuilder();
			if (custom_task_ids)
			{
				parmBuilder.Append("&custom_task_ids=true");
				if (team_id != default)
				{
					parmBuilder.Append($"&team_id={team_id}");
				}
			}

			if (start != default)
			{
				parmBuilder.Append($"&start={start}");
			}

			if (start_id != default)
			{
				parmBuilder.Append($"&start_id={start_id}");
			}

			string cleanParms = string.Empty;
			if (parmBuilder.Length > 0)
			{
				// We've assumed an ampersand on all parms, but we really need to start with a question mark instead.
				cleanParms = string.Concat("?", parmBuilder.ToString().AsSpan(1));
			}

			#endregion [ Build parameter string ]

			var request = await client.GetAsync($"{Endpoint}/task/{task_id}/comment{cleanParms}");
			var httpResponse = await request.Content.ReadAsStringAsync();

			response.Error = JsonSerializer.Deserialize<ErrorResult>(httpResponse);
			if (!response.Error.IsError)
			{
				var myDeserializedClass = JsonSerializer.Deserialize<ClickUpContext>(httpResponse);
				response.Value = myDeserializedClass.comments;
			}

			//Console.WriteLine(httpResponse);
			return response;
		}

		#endregion [ Comments ]

		public async Task GetUpdatedTasks()
		{
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
	}
}