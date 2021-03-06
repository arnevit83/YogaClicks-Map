using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace YogaClicks
{
	public partial class App : Application
	{
        public static string DB_PATH = string.Empty;
		public App ()
		{
			InitializeComponent();

			MainPage = new YogaClicks.MainPage();
		}
        public App(String DB_Path)
        {
            InitializeComponent();
            DB_PATH = DB_Path;
            MainPage = new YogaClicks.MainPage();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
