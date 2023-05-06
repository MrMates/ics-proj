﻿using Project.BL.Models;
using Project.App.Views.Project;
using CommunityToolkit.Mvvm.Input;
using Project.BL.Facades;
using Project.App.Services;
using Project.DAL;
using Project.App.Messages;
using Project.DAL.Seeds;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace Project.App.ViewModels.Project;

public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectCreatedMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private bool _isTimeSpentSortedAscending = true;
    private bool _isProjectNameSortedAscending = true;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public IEnumerable<UserListModel> Users { get; set; } = null!;


    public ProjectListViewModel(
        IProjectFacade projectFacade,
        IMessengerService messengerService,
        INavigationService navigationService)
        : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        //Simulace dat 
        await _projectFacade.AddActivityToProject(ActivitySeeds.DefaultActivity.Id, ProjectSeeds.DefaultProject.Id);

        Projects = (await _projectFacade.GetAsyncWithUsers()).ToList();


        foreach (ProjectListModel project in Projects)
        {
            project.TimeSpent = await _projectFacade.TotalTimeSpent(project.Id);
        }
    }
    
    [RelayCommand]
    private void SortByProjectName()
    {
        if (_isProjectNameSortedAscending)
        {
            Projects = Projects.OrderBy(x => x.ProjectName);
        }
        else
        {
            Projects = Projects.OrderByDescending(x => x.ProjectName);
        }

        _isProjectNameSortedAscending = !_isProjectNameSortedAscending;
    }

    [RelayCommand]
    private void SortByTimeSpent()
    {
        if (_isTimeSpentSortedAscending)
        {
            Projects = Projects.OrderBy(x => x.TimeSpent);
        }
        else
        {
            Projects = Projects.OrderByDescending(x => x.TimeSpent);
        }

        _isTimeSpentSortedAscending = !_isTimeSpentSortedAscending;
    }

    [RelayCommand]
    private async Task User_Join_Project(Guid projectId)
    {
        if (Shell.Current.Resources.TryGetValue("userId", out object userIdObj) && userIdObj is Guid userId)
        {
            await _projectFacade.AddUserToProject(userId,projectId);
        }
    }

    [RelayCommand]
    private async Task DeleteProject(Guid projectId)
    {
        bool confirmed = await Application.Current.MainPage.DisplayAlert("Delete Project", "Are you sure you want to delete this project?", "Yes", "No");

        if (confirmed)
        {
            await _projectFacade.DeleteAsync(projectId);
            await LoadDataAsync();
        }
    }


    [RelayCommand]
    private async Task GoToCreateProject()
    {
        await _navigationService.GoToAsync("/create");       
    }

    public async void Receive(ProjectCreatedMessage message)
    {
        await LoadDataAsync();
    }
}