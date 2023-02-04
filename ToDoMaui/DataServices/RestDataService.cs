using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoMaui.Models;

namespace ToDoMaui.DataServices
{
		public class RestDataService:IRestDataServices
		{
				private readonly HttpClient _httpClient;
				private readonly string _baseAddress;
				private readonly string _url;
				private readonly JsonSerializerOptions _jsonSerializerOptions;

				public RestDataService() { 
						_httpClient = new HttpClient();
						_baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5066" : "http://localhost:5066";
						_url = $"{_baseAddress}/api";

						_jsonSerializerOptions = new JsonSerializerOptions
						{
								PropertyNamingPolicy= JsonNamingPolicy.CamelCase
						};
				}

				public async Task AddToDoAsync(ToDo toDo)
				{
						if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
						{
								Debug.WriteLine("No Internet");

								return ;
						}

						try
						{
								string jsonToDo = JsonSerializer.Serialize(toDo, _jsonSerializerOptions);
								StringContent content = new StringContent(jsonToDo, Encoding.UTF8,"application/json");
								HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);
								if (response.IsSuccessStatusCode)
								{
										Debug.WriteLine("Success posting");
								}
								else
								{
										Debug.WriteLine("Non-2xx response");


								}
						}
						catch (Exception ex) {
								Debug.WriteLine("Error " + ex.Message);

						}
						return;
				}

				public async Task DeleteToDoAsync(int toDo)
				{

						if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
						{
								Debug.WriteLine("No Internet");
						}

						try
						{
								HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{toDo}");
								if (response.IsSuccessStatusCode)
								{
										Debug.WriteLine("Success DELETE");
								}
								else
								{
										Debug.WriteLine("Error DELETE");
								}
						}
						catch (Exception ex)
						{
								Debug.WriteLine("Error " + ex.Message);

						}
				}

				public async Task<List<ToDo>> GetAllToDos()
				{
						List<ToDo> todos = new List<ToDo>();

						if(Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
						{
								return todos;
						}

						try
						{
								HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
								if (response.IsSuccessStatusCode)
								{
										string content = await response.Content.ReadAsStringAsync();
										todos = JsonSerializer.Deserialize<List<ToDo>>(content, _jsonSerializerOptions);
								}
								else
								{
										return todos;
								}
						}
						catch (Exception ex)
						{
								Debug.WriteLine("Error " + ex.Message);

						}
						return todos;
				}

				public async Task UpdateToDoAsync(ToDo toDo)
				{

						if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
						{
								Debug.WriteLine("No Internet");
						}

						try
						{
								string jsonToDo = JsonSerializer.Serialize(toDo, _jsonSerializerOptions);
								StringContent content = new StringContent(jsonToDo, Encoding.UTF8, "application/json");
								HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{toDo.Id}", content);
								if (response.IsSuccessStatusCode)
								{
										Debug.WriteLine("Success PUT");
								}
								else
								{
										Debug.WriteLine("Error PUT");
								}
						}
						catch (Exception ex)
						{
								Debug.WriteLine("Error " + ex.Message);

						}
				}
		}
}
