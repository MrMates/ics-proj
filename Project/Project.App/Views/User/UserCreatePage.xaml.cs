namespace Project.App.Views.User;

public partial class UserCreatePage : ContentPage
{
    private string firstName;
    private string SurName;
    public UserCreatePage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        firstName = FirstNameEntry.Text;
        SurName = SurNameEntry.Text;
        
    }
    private async void OnTapped(object sender, EventArgs e)
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            var imageSource = ImageSource.FromFile(result.FullPath);
            profilePic.Source = imageSource;
            frame.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
        }
    }
}
