using Project.App.ViewModels.User;

namespace Project.App.Views.User;

public partial class UserListPage
{
    public UserListPage(UserListViewModel userListViewModel)
        : base(userListViewModel)
    {
        InitializeComponent();
    }
}

