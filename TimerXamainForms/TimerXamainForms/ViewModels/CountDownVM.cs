using System;
using TimerXamainForms.Helpers;
using TimerXamainForms.Views;
using Xamarin.Forms;

namespace TimerXamainForms.ViewModels
{
	class CountDownVM : BaseVM
	{
		private string title;
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}

		public DateTime EndDate { get; set; }

		public Command SettingsClick { get; }

		public CountDownVM(INavigation navigation)
		{
			Navigation = navigation;

			Title = App.Current.Properties.TryGetValue("title", out var title) ? (string)title : "До Нового Года осталось";
			EndDate = App.Current.Properties.TryGetValue("endDate", out var endDate) ? (DateTime)endDate : new DateTime(2020, 1, 1);
			EndDate = EndDate.Add(App.Current.Properties.TryGetValue("endTime", out var endTime) ? (TimeSpan)endTime : TimeSpan.Zero);
			
			Device.StartTimer(TimeSpan.FromTicks(500000), TimerTick);

			SettingsClick = new Command(async () => await Navigation.PushAsync(new SettingsPage()));
			MessagingCenter.Subscribe<SettingsVM>(this, "settingsClose", viewModel =>
			{
				App.Current.Properties["title"] = Title = viewModel.Title;
				App.Current.Properties["endDate"] = viewModel.EndDate;
				App.Current.Properties["endTime"] = viewModel.EndTime;
				EndDate = viewModel.EndDate.Add(viewModel.EndTime);
			});
		}

		private int days;
		public int Days
		{
			get => days;
			set => SetProperty(ref days, value, () =>
			{
				OnPropertyChanged(nameof(DaysVisible));
				OnPropertyChanged(nameof(DaysLabel));
			});
		}

		private int hours;
		public int Hours
		{
			get => hours;
			set => SetProperty(ref hours, value, () =>
			{
				OnPropertyChanged(nameof(HoursVisible));
				OnPropertyChanged(nameof(HoursLabel));
			});
		}

		private int minutes;
		public int Minutes
		{
			get => minutes;
			set => SetProperty(ref minutes, value, () =>
			{
				OnPropertyChanged(nameof(MinutesVisible));
				OnPropertyChanged(nameof(MinutesLabel));
			});
		}

		private int seconds;
		public int Seconds
		{
			get => seconds;
			set => SetProperty(ref seconds, value, () =>
			{
				OnPropertyChanged(nameof(SecondsVisible));
				OnPropertyChanged(nameof(SecondsLabel));
			});
		}

		public bool DaysVisible => Days != 0;
		public bool HoursVisible => Days != 0 || Hours != 0;
		public bool MinutesVisible => Days != 0 || Hours != 0 || Minutes != 0;
		public bool SecondsVisible => Days != 0 || Hours != 0 || Minutes != 0 || Seconds != 0;

		public string DaysLabel => DeclensionHelper.GetDeclension(Days, "День", "Дня", "Дней");
		public string HoursLabel => DeclensionHelper.GetDeclension(Hours, "Час", "Часа", "Часов");
		public string MinutesLabel => DeclensionHelper.GetDeclension(Minutes, "Минута", "Минуты", "Минут");
		public string SecondsLabel => DeclensionHelper.GetDeclension(Seconds, "Секунда", "Секунды", "Секунд");

		private bool TimerTick()
		{
			var currentDate = DateTime.Now;
			var delta = EndDate - currentDate;

			if (delta < TimeSpan.Zero)
				delta = TimeSpan.Zero;

			Days = delta.Days;
			Hours = delta.Hours;
			Minutes = delta.Minutes;
			Seconds = delta.Seconds;

			return true;
		}
	}
}