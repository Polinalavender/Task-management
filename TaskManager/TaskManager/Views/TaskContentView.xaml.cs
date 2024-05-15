namespace TaskManager.Views;

public partial class TaskContentView : ContentView
{
	public TaskContentView()
	{
		InitializeComponent();
		BindingContext = new TaskContentViewModel(teamId);
	}
}
