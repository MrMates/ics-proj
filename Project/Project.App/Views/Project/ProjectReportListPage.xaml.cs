using Microsoft.Maui.Controls;
using Project.App.ViewModels.Project;
using Project.BL.Facades;

namespace Project.App.Views.Project;

public partial class ProjectReportListPage
{
	public ProjectReportListPage(ProjectReportListViewModel projectReportListViewModel)
		: base(projectReportListViewModel) 
	{
		InitializeComponent();
	}
}