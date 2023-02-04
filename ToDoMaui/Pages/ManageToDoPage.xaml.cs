using System.Diagnostics;
using ToDoMaui.DataServices;
using ToDoMaui.Models;

namespace ToDoMaui.Pages;
[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
		private readonly IRestDataServices _dataService;
		ToDo _toDo;
		bool _isNew;

		public ToDo ToDo { get => _toDo; set
				{
						_isNew = IsNew(value);
						_toDo = value;
						OnPropertyChanged(nameof(ToDo));
				}
		}

		public ManageToDoPage(IRestDataServices dataServices)
		{
				InitializeComponent();
						_dataService = dataServices;
						BindingContext = this;
		}

		public bool IsNew(ToDo todo)
		{
				return todo.Id == 0;
		}

		async void OnDeleteButtonClicked(object sender, EventArgs e)
		{
				await _dataService.DeleteToDoAsync(_toDo.Id);
				await Shell.Current.GoToAsync("..");
		}

		async void OnSaveButtonClicked(object sender, EventArgs e)
		{
				if (_isNew)
				{
						Debug.WriteLine("Adding new Item");
						await _dataService.AddToDoAsync(_toDo);
				}
				else
				{
						Debug.WriteLine("We're updating " + _toDo.Id);
						await _dataService.UpdateToDoAsync(_toDo);
				}

				await Shell.Current.GoToAsync("..");

		}

		async void OnCancelButtonClicked(object sender, EventArgs e)
		{
				await Shell.Current.GoToAsync("..");
		}
}