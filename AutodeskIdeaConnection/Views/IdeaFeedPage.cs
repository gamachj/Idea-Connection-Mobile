using System;

using Xamarin.Forms;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AutodeskIdeaConnection
{
	/*
	 * 	IdeaFeed Page contains the contents of the Idea Feeds.
	 */
	public class IdeaFeedPage : ContentPage
	{
		public TabbedHomePage NavTabHomePage { set; get; }
		public int StartingIndex { set; get; }
		public CategoryCard Category { set; get; }

		private Button moreButton;

		private StackLayout Feed;


		public IdeaFeedPage (TabbedHomePage navTabPage)
		{
			//Padding
			this.Padding = new Thickness (15, Device.OnPlatform (20, 0, 0), 15, 5);

			//Background Color
			this.BackgroundColor = Color.FromRgb(232, 237, 236);

			this.Title = "Feed";

			this.Icon = "feed.png";

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



		async protected override void OnAppearing ()
		{
			base.OnAppearing ();

			NavTabHomePage.Title = "Feed";

			List<PostModel> postFeed;
			if (Category == null) {
				postFeed = await getLatestFeeds ();
			} else {
				postFeed = await getCategoryFeeds ();
			}

			List<FeedCardTemplate> feedCards = new List<FeedCardTemplate>();
			for (int i = 0; i < postFeed.Count; i++) {
				FeedCard card = new FeedCard ();
				card.Postid = postFeed [i].postid;
				card.Title = postFeed [i].title;
				card.Votes = postFeed [i].netvotes;
				card.SubmittedOn = postFeed [i].created;
				card.Description = postFeed [i].content;
				feedCards.Add(new FeedCardTemplate(card, NavTabHomePage));
			}
				
			Feed = new StackLayout {
				Spacing = 20,
			};

			for (int i = 0; i < feedCards.Count; i++) {
				Feed.Children.Add (feedCards [i]);
			}

			Feed.Children.Add (moreButton);

			ScrollView ideaFeedPage = new ScrollView () {
				Orientation = ScrollOrientation.Vertical,
				Content = Feed
			};
			this.Content = ideaFeedPage;
		}

		//Get the latest feeds from PHP slim server asynchronously.
		async Task<List<PostModel>> getLatestFeeds(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "latestIdeas/" + StartingIndex;

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;
		 
			var lastestFeedPostList = JsonConvert.DeserializeObject<List<PostModel>> (responseJson);

			return lastestFeedPostList;

		}

		//Get the category Feeds asynchronously.
		async Task<List<PostModel>> getCategoryFeeds(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "categories/getFeed/" + Category.CategoryId + "/" + StartingIndex;

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;

			var categoryPostList = JsonConvert.DeserializeObject<List<PostModel>> (responseJson);
			return categoryPostList;

		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			//Set the starting index to zero and clear Category and Feed.
			StartingIndex = 0;
			Category = null;
			if (Feed != null && Feed.Children.Count != 0) {
				Feed.Children.Clear ();
			}

			Content =  Feed;
		}


	}


}


