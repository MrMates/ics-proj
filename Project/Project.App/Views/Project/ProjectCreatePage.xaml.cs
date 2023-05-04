using Project.App.ViewModels.Project;

namespace Project.App.Views.Project;

public partial class ProjectCreatePage
{
    public ProjectCreatePage(ProjectListViewModel projectCreateViewModel)
        : base(projectCreateViewModel)
    {
        InitializeComponent();
    }
}