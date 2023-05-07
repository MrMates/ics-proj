using Project.App.Services;
using Project.App.ViewModels.User;

namespace Project.App
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetService<AppShell>();
        }
    }
}