
using Microsoft.Maui.Controls;

namespace Project.App
{
    public partial class UserCreatePage : ContentPage
    {
        public UserCreatePage()
        {
            InitializeComponent();
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
}
