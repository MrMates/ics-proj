﻿using Project.BL.Models;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.Common.Enums;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using System.Timers;

namespace Project.App.ViewModels.Timer
{
    public partial class TimerViewModel : ViewModelBase, IRecipient<UserJoinedProjectMessage>
    {
        private System.Timers.Timer _timer;
        private readonly IActivityFacade _activityFacade;
        private readonly IUserFacade _userFacade;
        private readonly INavigationService _navigationService;

        public string ActivityName { get; set; }
        public TimeSpan CurrentTime { get; set; } = TimeSpan.Zero;
        private DateTime _timeBegin;
        public DateTime TimeBegin { get { return _timeBegin; } set { DateConverted = value.Date.ToString("dd.MM.yyyy"); _timeBegin = value; } }
        public string DateConverted { get; set; }
        public string ActivityDescription { get; set; }
        public bool IsVisible { get; set; } = false;
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
            TimeBegin = DateTime.Now;
        }

        [RelayCommand]
        private void DateClicked()
        {
            IsVisible = !IsVisible;
        }

        [RelayCommand]
        private void CreateActivity()
        {
            IsRunning = !IsRunning;
            ImageSource = IsRunning ? "pause.png" : "play.png";
            if (ActivityName != null)
            {
                if(IsRunning)
                {
                    _timer = new System.Timers.Timer();
                    _timer.Interval = 1000;
                    _timer.Elapsed += Tick;
                    _timer.Enabled = true;
                    Task.Run(async () =>
                    {
                        var model = new ActivityDetailModel
                        {
                            ActivityName = ActivityName,
                            Type = ActivityType,
                            ActivityDescription = ActivityDescription,
                            ProjectId = SelectedProject.Id,
                            UserId = UserId,
                            TimeBegin = TimeBegin
                        };
                        SelectedActivity = await _activityFacade.SaveAsync(model);
                    });
                }
                else
                {
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
            ImageSource = IsRunning ? "pause.png" : "play.png";
            UserId = (Guid)Shell.Current.Resources["userId"];
            Activities = await _activityFacade.GetAsync();
            Projects = (await _userFacade.GetUsersProjects(UserId)).ToList();
            var nullActivity = Activities.Where(i => i.Id == UserId && i.TimeEnd == null).FirstOrDefault();
            if (nullActivity != null)
            {
                SelectedActivity = await _activityFacade.GetAsync(nullActivity.Id);
                IsRunning = true;
                ImageSource = IsRunning ? "pause.png" : "play.png";
                ActivityName = SelectedActivity.ActivityName;
                ActivityDescription = SelectedActivity.ActivityDescription;
                TimeBegin = SelectedActivity.TimeBegin;
                ActivityType = SelectedActivity.Type;
                SelectedProject = Projects.Where(i => i.Id == SelectedActivity.ProjectId).Single();
            }
            
        }

        public async void Receive(UserJoinedProjectMessage message)
        {
            await LoadDataAsync();
        }
    }
}
