using TaskManagement.ViewModels;

namespace TaskManagement.Views;

public partial class TaskContentView : ContentView
{
	public TaskContentView(int? teamId)
	{
		InitializeComponent();
		BindingContext = new TaskContentViewModel(teamId);
	}
}