using System;
using Xamarin.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace AutodeskIdeaConnection
{
	/*
	 * 	Login Page is the starting activity of the Application.
	 */
	public class LoginPage : ContentPage
	{
	
		private Image logoHeading;

		private Label usernameLabel;
		private SquareEntryView usernameEntry;

		private Label passwordLabel;
		private SquareEntryView passwordEntry;

		public Label ErrorLabel { set; get; }

		private Button submitButton;
		ActivityIndicator loginActivityIndicator;

		public LoginPage ()
		{

			//Background Color
			this.BackgroundColor = Color.FromRgb (232, 237, 236);

			//Padding
			this.Padding = new Thickness (10, Device.OnPlatform (20, 20, 0), 10, 5);


			//Heading Image
			logoHeading = new Image { Aspect = Aspect.AspectFit };
			logoHeading.Source = "logo_copy.png";
		
			//Error Message
			ErrorLabel = new Label {
				Text = "Username or password is invalid. Please try again.",
				TextColor = Color.Red,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null),
				IsVisible = false
			};

			// Username Label
			usernameLabel = new Label {
				Text = "USERNAME",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor = Color.FromRgb(3,100,100)
			};
			//Username Entry
			 usernameEntry = new SquareEntryView {
				IsPassword = false,
				HeightRequest = 35
			};

			//Password Label
			passwordLabel = new Label {
				Text = "PASSWORD",
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				TextColor = Color.FromRgb(3,100,100)
			};
			//Passwrod Entry
			passwordEntry = new SquareEntryView {
				IsPassword = true,
				HeightRequest = 35
			};


			//Submit Button
			submitButton = new Button {
				Text = "SUBMIT",
				BackgroundColor = Color.FromRgb(3,100,100),
				TextColor = Color.White,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderRadius = 0
			};

			// Activity Indicator
			loginActivityIndicator = new ActivityIndicator {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsRunning = false,
				Color = Color.FromRgb(3, 100, 100),
				IsVisible = false
			};

			//Event Registration
			submitButton.Clicked += LoginApplication;
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (loginActivityIndicator.IsRunning == true) {
				loginActivityIndicator.IsRunning = false;
				loginActivityIndicator.IsVisible = false;
			}

			//Heading
			StackLayout headingStack = new StackLayout {
				Children = { logoHeading } 
			};
			//Username Stack
			StackLayout usernameStack = new StackLayout {
				Spacing = 0,
				Children = { usernameLabel, usernameEntry }
			};
			//Password Stack
			StackLayout passwordStack = new StackLayout {
				Spacing = 0,
				Children = { passwordLabel, passwordEntry }
			};


			StackLayout loginPage = new StackLayout {
				Spacing = 20,
				Padding = new Thickness(0, Device.OnPlatform(20,0,0), 0, 0),
				Children = { headingStack, ErrorLabel, usernameStack, passwordStack, submitButton, loginActivityIndicator }
			};

			//Finally set the Content to Relative Layout Object
			this.Content = loginPage;
		}

		async void LoginApplication(object sender, EventArgs args){


			string username = usernameEntry.Text;
			string password = passwordEntry.Text;

			loginActivityIndicator.IsRunning = true;
			loginActivityIndicator.IsVisible = true;

			var TargetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (TargetURL);
			handler.Credentials = new System.Net.NetworkCredential (username, password);


			HttpResponseMessage response = await client.GetAsync("getIdentity");

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				ErrorLabel.IsVisible = true;
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;

			if (responseJson == username) {
				//Respond appropriately
				await Navigation.PushAsync (new TabbedHomePage (username, password));
			}

			//await Navigation.PushAsync(new ErrorPage());
				

		}

		protected override bool OnBackButtonPressed ()
		{
			Debug.WriteLine ("Back");
			return base.OnBackButtonPressed ();
		}

	}
}

