using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutodeskIdeaConnection
{
	/*
	 * NotificationCardTemplate generates a card that can be embedded in NotificationsPage.
	 */
	public class NotificationCardTemplate : ContentView
	{
		private TabbedHomePage NavTabHomePage;

		public NotificationCardTemplate (TabbedHomePage navTabHomePage, NotificationsCard notificationCard)
		{
			this.BindingContext = notificationCard;

			this.NavTabHomePage = navTabHomePage;

			var date = new Label {
				FontSize = 15,
				TextColor = Color.FromRgb (3, 100, 100)
			};
			date.SetBinding (Label.TextProperty, new Binding ("Date"));

			var time = new Label {
				FontSize = 10,
				TextColor = Color.FromRgb(133, 133, 133)
			};
			time.SetBinding (Label.TextProperty, new Binding ("Time"));

			var dateTimeStack = new StackLayout {
				MinimumWidthRequest = 60,
				Spacing = 0,
				VerticalOptions = LayoutOptions.StartAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = { date, time }
			};

			var message = new Label {
			};
			message.SetBinding (Label.TextProperty, new Binding ("Message"));

			var navigationIcon = new Label {
				HorizontalOptions = LayoutOptions.End,
				TextColor = Color.FromRgb (3, 100, 100)
			};
			navigationIcon.SetBinding (Label.TextProperty, new Binding ("NavigationIcon"));

			var RowTemplate = new StackLayout {
				BackgroundColor = Color.White,
				HeightRequest = 60,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (5, Device.OnPlatform (5, 0, 0), 5, 5),
				Children = { dateTimeStack, message, navigationIcon }
			};

			var tgr = new TapGestureRecognizer ();
			tgr.Tapped += (sender, e) => onNotificationCardClicked(notificationCard.Parentid);

			this.GestureRecognizers.Add (tgr);

			Content = RowTemplate;
		}

		//Push the Individual post on the navigation stack asynchronously.
		async private void onNotificationCardClicked(string postid){

			Debug.WriteLine ("Getting Post" + postid);
			PostModel post = await getPost (postid);

			FeedCard card = new FeedCard(post.postid, post.title, post.netvotes, post.created, post.content);

			Debug.WriteLine ("Feedcard is writen");

			await Navigation.PushAsync (new IndividualPostPage (card, NavTabHomePage));
		}

		//Get the Post that is clicked on.
		async private Task<PostModel> getPost(string postid){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "viewIdea/" + postid;

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				//OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;


			var individualPost = JsonConvert.DeserializeObject<List<PostModel>> (responseJson);

			return individualPost[0];

		}
	}
}

