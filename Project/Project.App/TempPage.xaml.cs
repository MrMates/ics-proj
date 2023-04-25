using System.Diagnostics;

namespace Project.App

{
   public partial class TempPage : ContentPage
    {
        public TempPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new UserCreatePage());
        }



    }
}

