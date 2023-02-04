using Microsoft.Maui.Controls;
using System.Diagnostics;
using ToDoMaui.DataServices;
using ToDoMaui.Models;
using ToDoMaui.Pages;

namespace ToDoMaui;

public partial class MainPage : ContentPage
{
		private readonly IRestDataServices _dataService;

		private string _currentDirectory = "";

		public string CurrentDirectory { get => _currentDirectory; set
				{
						if(_currentDirectory == value) return;
						_currentDirectory = value;
						OnPropertyChanged("CurrentDirectory");
				} }

		public MainPage(IRestDataServices dataService)
			{
				InitializeComponent();
						_dataService = dataService;
				BindingContext = this;

		}


		protected async override void OnAppearing()
		{
				base.OnAppearing();

				collectionView.ItemsSource = await _dataService.GetAllToDos();

				if (!Preferences.ContainsKey("currentDirectory")) Preferences.Set("currentDirectory", DefaultDirectory());
				txtCurrentDirectory.Text = Preferences.Get("currentDirectory", DefaultDirectory());
				txtCurrentDirectory.IsReadOnly = true;

				string[] files = Directory.EnumerateDirectories(txtCurrentDirectory.Text).ToArray();
				files.ToList().ForEach(e => Debug.WriteLine(e));

		}

		async void OnAddToDoClicked(object sender, EventArgs e)
		{
				Debug.WriteLine("Add To Do Clicked");
				var navigationParameter = new Dictionary<string, object>
				{
						{ nameof(ToDo), new ToDo()}
				};

				await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
		}

		async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
				var navigationParameter = new Dictionary<string, object>
				{
						{ nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo}
				};

				await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
		}

		public static string DefaultDirectory()
		{
#if ANDROID
				return Android.OS.Environment.DirectoryDownloads;
#else
				return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif


		}
}

