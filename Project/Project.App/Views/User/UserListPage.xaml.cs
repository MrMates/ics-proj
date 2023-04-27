namespace Project.App.Views.User;

public partial class UserListPage : ContentPage
{
    public UserListPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new UserCreatePage());
    }
}

