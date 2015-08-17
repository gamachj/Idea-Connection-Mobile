using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace AutodeskIdeaConnection
{
	/*
	 * 	Notifications Page contains the contents of the notifications.
	 */
	public class NotificationsPage : ContentPage
	{
		private TabbedHomePage NavTabHomePage { set; get; }
		public int StartingIndex { get; set; }

		private Button moreButton;

		private List<NotificationModel> Notifications;

		public NotificationsPage (TabbedHomePage navTabPage)
		{

			//Padding
			this.Padding = new Thickness (15, Device.OnPlatform (40, 0, 0), 15, 5);

			//Background Color
			this.BackgroundColor = Color.FromRgb(232, 237, 236);

			this.Title = "Notifications";

			this.Icon = "notification.png";

			this.NavTabHomePage = navTabPage;

			this.StartingIndex = 0;

			//More Button
			moreButton = new Button {
				Text = "Load More...",
				TextColor = Color.FromRgb(3,100,100),
				BorderWidth = 0
			};

			moreButton.Clicked += (sender, e) => {
				this.StartingIndex += 20;
				OnAppearing();
			};
		}

		//overload on appearing method to display the notifications.
		async protected override void OnAppearing ()
		{
			base.OnAppearing ();


			NavTabHomePage.Title = "Notifications";

			Notifications = await getNotifications ();


			//Debug.WriteLine (myDate.Month + "/" + myDate.Day + "/" + myDate.Year + " " + myDate.Hour + ":" + myDate.Minute + " AM");

			List<NotificationsCard> notificationCards = new List<NotificationsCard> ();


			for (int i = 0; i < Notifications.Count; i++) {
				notificationCards.Add (new NotificationsCard (Notifications[i].Postid, Notifications[i].Parentid,Notifications[i].Displayname,Notifications[i].Type,Notifications[i].Created,Notifications[i].Updated));
			}

			var navigationList = new StackLayout {
				Spacing = 1,
				Padding = new Thickness (5, Device.OnPlatform (5, 0, 0), 5, 5),
				Orientation = StackOrientation.Vertical, 
			};

			for (int i = StartingIndex; i < StartingIndex + 6 && i < notificationCards.Count; i++) {
				navigationList.Children.Add (new NotificationCardTemplate(NavTabHomePage, notificationCards[i]));
			}

			if (StartingIndex + 6 < notificationCards.Count) {
				navigationList.Children.Add (new StackLayout{
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					Children = { moreButton }
				});
			}

			Content = navigationList;
		}

		//Fectch the notifications asynchronously.
		async private Task<List<NotificationModel>> getNotifications(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "notifications/" + StartingIndex;

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;


			var notificationList = JsonConvert.DeserializeObject<List<NotificationModel>> (responseJson);

			return notificationList;

		}

	}
}


