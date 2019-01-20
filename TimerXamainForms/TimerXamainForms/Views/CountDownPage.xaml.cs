using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TimerXamainForms.ViewModels;

namespace TimerXamainForms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CountDownPage : ContentPage
	{
		private CountDownVM viewModel;

		public CountDownPage ()
		{
			InitializeComponent ();

			BindingContext = viewModel = new CountDownVM(Navigation);
			settingsImage.GestureRecognizers.Add(new TapGestureRecognizer() { Command = viewModel.SettingsClick });
			NavigationPage.SetHasNavigationBar(this, false);			
		}
	}
}