using Project.App.ViewModels.Timer;

namespace Project.App.Views.Timer;

public partial class TimerListPage
{
	private TimeOnly time = new();
	private bool isRunning;
	public TimerListPage(ActivityListViewModel activityListViewModel) : base(activityListViewModel)
	{
		InitializeComponent();
	}

	private async void OnStartStop(object sender, EventArgs e)
	{
		isRunning = !isRunning;
		startStopButton.Source = isRunning ? "pause.png" : "play.png";

		while(isRunning)
		{
			time = time.Add(TimeSpan.FromSeconds(1));
			SetTime();
			await Task.Delay(TimeSpan.FromSeconds(1));
		}
	}
	private void SetTime()
	{
		timeLabel.Text = $"{time.Hour}:{time.Minute}:{time.Second}";
	}

	private void OnDatePickerClicked(object sender, EventArgs e)
	{
		datePicker.IsVisible = true;
	}

	private void OnDateSelected(object sender, DateChangedEventArgs e)
	{
		selectedDateLabel.Text = e.NewDate.ToString("dd.MM.yyyy");
		datePicker.IsVisible = false;
	}
}