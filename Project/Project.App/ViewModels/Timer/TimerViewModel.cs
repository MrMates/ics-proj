using Project.BL.Models;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.Common.Enums;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using System.Timers;

namespace Project.App.ViewModels.Timer
{
    public partial class TimerViewModel : ViewModelBase, IRecipient<UserJoinedProjectMessage>, IRecipient<UserPickedMessage>
    {
        private System.Timers.Timer _timer;
        private readonly IActivityFacade _activityFacade;
        private readonly IUserFacade _userFacade;
        private readonly INavigationService _navigationService;

        public string ActivityName { get; set; }
        public TimeSpan CurrentTime { get; set; } = TimeSpan.Zero;
        public string ActivityDescription { get; set; }
        private int _typeIndex = 0;
        public int ActivityTypeIndex { get => _typeIndex; set { _typeIndex = value; ActivityType = (ActivityType)value; } }
        public ActivityType ActivityType { get; set; } = ActivityType.Work;
        public Guid UserId { get; set; }
        public Guid ActivityId { get; set; }
        public ProjectListModel SelectedProject { get; set; }
        public ActivityDetailModel SelectedActivity { get; set; }
        public bool IsRunning { get; set; } = false;

        public string ImageSource { get; set; }

        public IEnumerable<ActivityListModel> Activities { get; set; } = null;
        public IEnumerable<ProjectListModel> Projects { get; set; }


        public TimerViewModel(
            IActivityFacade activityFacade,
            IUserFacade userFacade,
            IMessengerService messengerService,
            INavigationService navigationService)
            : base(messengerService)
        {
            _activityFacade = activityFacade;
            _userFacade = userFacade;
            _navigationService = navigationService;

            _timer = new System.Timers.Timer();
            _timer.Interval = 500;
            _timer.Elapsed += Tick;
            _timer.Enabled = false;
        }

        [RelayCommand]
        private void CreateActivity()
        {
            IsRunning = !IsRunning;
            ImageSource = IsRunning ? "pause.png" : "play.png";
            if (ActivityName != null)
            {
                if (IsRunning)
                {
                    _timer.Enabled = true;
                    Task.Run(async () =>
                    {
                        var model = new ActivityDetailModel
                        {
                            ActivityName = ActivityName,
                            Type = ActivityType,
                            ActivityDescription = ActivityDescription,
                            ProjectId = (SelectedProject == null) ? null : SelectedProject.Id,
                            UserId = UserId,
                            TimeBegin = DateTime.Now
                        };
                        SelectedActivity = await _activityFacade.SaveAsync(model);
                    });
                }
                else
                {
                    _timer.Enabled = false;
                    Task.Run(async () =>
                    {
                        var model = new ActivityDetailModel
                        {
                            Id = SelectedActivity.Id,
                            ActivityName = SelectedActivity.ActivityName,
                            ActivityDescription = SelectedActivity.ActivityDescription,
                            TimeBegin = SelectedActivity.TimeBegin,
                            TimeEnd = DateTime.Now,
                            UserId = SelectedActivity.UserId,
                            Type = ActivityType
                        };
                        await _activityFacade.SaveAsync(model);
                        MessengerService.Send(new ActivityFinishedMessage(SelectedActivity.Id));
                        SelectedActivity = ActivityDetailModel.Empty;
                        await LoadDataAsync();
                    });
                }
            }
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            if (SelectedActivity != null)
            {
                CurrentTime = (DateTime.Now - SelectedActivity.TimeBegin);
            }
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            UserId = (Guid)Shell.Current.Resources["userId"];
            Activities = (await _activityFacade.GetAsync()).ToList();
            Projects = (await _userFacade.GetUsersProjects(UserId)).ToList();

            var nullActivity = Activities.Where(i => i.UserId == UserId && i.TimeEnd == null).FirstOrDefault();
            if (nullActivity != null)
            {
                SelectedActivity = await _activityFacade.GetAsync(nullActivity.Id);
                IsRunning = true;
            }
            else
            {
                SelectedActivity = ActivityDetailModel.Empty;
                IsRunning = false;
            }
            _timer.Enabled = IsRunning;
            CurrentTime = (DateTime.Now - SelectedActivity.TimeBegin);
            ImageSource = IsRunning ? "pause.png" : "play.png";
            ActivityName = SelectedActivity.ActivityName;
            ActivityDescription = SelectedActivity.ActivityDescription;
            ActivityType = SelectedActivity.Type;
            SelectedProject = Projects.Where(i => i.Id == SelectedActivity.ProjectId).SingleOrDefault();

        }

        public async void Receive(UserJoinedProjectMessage message)
        {
            await LoadDataAsync();
        }

        public async void Receive(UserPickedMessage message)
        {
            await LoadDataAsync();
        }
    }
}
