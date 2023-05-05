using Project.App.ViewModels.Project;

namespace Project.App.Views.Project;

public partial class ProjectCreatePage
{
    public ProjectCreatePage(ProjectCreateViewModel projectCreateViewModel)
        : base(projectCreateViewModel)
    {
        InitializeComponent();
    }
}