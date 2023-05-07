using Project.BL.Models;
using Project.App.Views.Timer;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Microsoft.EntityFrameworkCore.Metadata;
using Project.App.Views;

namespace Project.App.ViewModels.Timer
{
    public partial class ActivityDetailViewModel : ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public string activity_Name { get; set; }
        public string activity_Type { get; set; }
        public DateTime time_Begin { get { return date_Date; } set { date_Converted = value.Date.ToString("dd.MM.yyyy"); date_Date = value; } }
        public string date_Converted { get; set; }
        public string activity_Description { get; set; }
        public Guid project_Id { get; set; }
        public Guid user_Id { get; set; }
        public bool is_Visible { get; set; } = false;

        private DateTime date_Date;

        public IEnumerable<ActivityListModel> Activities { get; set; } = null;

        public ActivityDetailViewModel(IActivityFacade activityFacade, IMessengerService messengerService, INavigationService navigationService) : base(messengerService)
        {
            _activityFacade = activityFacade;
            _navigationService = navigationService;
            time_Begin= DateTime.Now;
        }

        [RelayCommand]
        private Task DateClicked()
        {
            is_Visible = !is_Visible;

            return Task.CompletedTask;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Activities = await _activityFacade.GetAsync();
            if (activity_Name != null)
            {
                await _activityFacade.SaveAsync(new BL.Models.ActivityDetailModel { ActivityName = activity_Name, ActivityType = activity_Type,  ActivityDescription = activity_Description, ProjectId = project_Id, UserId = user_Id});
            }
        }
    }
}
