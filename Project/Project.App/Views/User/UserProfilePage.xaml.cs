using Project.App.ViewModels.User;

namespace Project.App.Views.User;

public partial class UserProfilePage
{
    public UserProfilePage(UserProfileViewModel UserProfileViewModel)
        : base(UserProfileViewModel)
    {
        InitializeComponent();
    }
}
