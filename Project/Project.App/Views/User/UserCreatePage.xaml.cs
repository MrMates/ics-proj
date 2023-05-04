using Project.App.ViewModels.User;

namespace Project.App.Views.User;

public partial class UserCreatePage
{
    public UserCreatePage(UserCreateViewModel UserCreateViewModel)
        : base(UserCreateViewModel)
    {
        InitializeComponent();
    }
}
