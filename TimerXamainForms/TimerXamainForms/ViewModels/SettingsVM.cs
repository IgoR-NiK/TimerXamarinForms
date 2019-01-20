using System;
using Xamarin.Forms;

namespace TimerXamainForms.ViewModels
{
    class SettingsVM : BaseVM
    {
		public string Title { get; set; }
		public DateTime EndDate { get; set; }
		public TimeSpan EndTime { get; set; }

		public Command SaveClick { get; }

		public SettingsVM(INavigation navigation)
		{
			Navigation = navigation;

			Title = App.Current.Properties.TryGetValue("title", out var title) ? (string)title : "До Нового Года осталось";
			EndDate = App.Current.Properties.TryGetValue("endDate", out var endDate) ? (DateTime)endDate : new DateTime(2020, 1, 1);
			EndTime = App.Current.Properties.TryGetValue("endTime", out var endTime) ? (TimeSpan)endTime : TimeSpan.Zero;

			SaveClick = new Command(async () =>
			{
				MessagingCenter.Send(this, "settingsClose");
				await Navigation.PopAsync();
			});			
		}
	}
}