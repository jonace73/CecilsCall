using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CecilsCall.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public static string ownersName = "Jon Anderson";// change to "" for the release version
        public static int maxNumberRepeatitions = 2;// change to 7 for the release version
        public Settings ()
		{
			InitializeComponent ();
		}
        async void OnSaveSettingsButtonClicked(object sender, EventArgs e)
		{
			ownersName = OwnersName.Text;

            // Check for Maximum number of repeatitions as integer
            if (int.TryParse(MaxNumberRepeatitions.Text, out int value))
			{
				maxNumberRepeatitions = value;

            } else
			{
                await DisplayAlert("Error:", "Maximum number of repeatition is not an integer.", "OK");
            }
        }

    }
}