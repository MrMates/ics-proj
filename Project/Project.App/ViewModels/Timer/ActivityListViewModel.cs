using Project.BL.Models;
using Project.App.Views.Timer;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.App.ViewModels.Timer
{
    public partial class ActivityListViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;
        public IEnumerable<ActivityListModel> Activities { get; set; } = null;

        public ActivityListViewModel(IActivityFacade activityFacade, IMessengerService messengerService, INavigationService navigationService) : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;
        }
        
        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activities = await _activityFacade.GetAsync();
        }
    }
}
