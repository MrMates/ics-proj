using Project.App.ViewModels.Project;
using Project.BL.Facades;

namespace Project.App.Views.Project;

public partial class ProjectDetailPage
{
    public ProjectDetailPage(ProjectDetailViewModel projectDetailViewModel)
        : base(projectDetailViewModel)
    {
        InitializeComponent();
    }
}