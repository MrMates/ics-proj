using Project.App.ViewModels.Project;

namespace Project.App.Views.Project;

public partial class ProjectListPage
{
    public ProjectListPage(ProjectListViewModel projectListViewModel)
        : base(projectListViewModel)
    {
		InitializeComponent();
	}
}