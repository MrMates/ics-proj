using Project.App.Models;
using Project.App.ViewModels;
using Project.App.ViewModels.Project;
using Project.App.ViewModels.User;
using Project.App.Views.Project;
using Project.App.Views.User;

namespace Project.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//users", typeof(UserListPage), typeof(UserListViewModel)),
        new("//users/create", typeof(UserCreatePage), typeof(UserCreateViewModel)),
        new("//projects", typeof(ProjectListPage), typeof(ProjectListViewModel)),
        new("//projects/create", typeof(ProjectCreatePage), typeof(ProjectCreateViewModel)),
        new("//projects/report" , typeof(ProjectReportListPage),typeof(ProjectReportListViewModel))
        new("//projects/report" , typeof(ProjectReportListPage),typeof(ProjectReportListViewModel)),
        new("//projects/detail" , typeof(ProjectDetailPage), typeof(ProjectDetailViewModel))
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}