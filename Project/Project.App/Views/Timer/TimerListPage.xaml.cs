using Project.App.ViewModels.Timer;

namespace Project.App.Views.Timer;

public partial class TimerListPage
{
	public TimerListPage(TimerViewModel timerViewModel) : base(timerViewModel)
	{
		InitializeComponent();
	}
}