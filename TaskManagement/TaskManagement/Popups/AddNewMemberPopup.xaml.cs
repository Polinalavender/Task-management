using CommunityToolkit.Maui.Views;
using TaskManagement.ViewModels;

namespace TaskManagement.Popups;

public partial class AddNewMemberPopup : Popup
{
	public AddNewMemberPopup()
	{
		InitializeComponent();
		BindingContext = new AddNewMemberViewModel();
	}
}