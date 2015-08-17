using System;

using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AutodeskIdeaConnection
{
	/*
	 * 	Individual Post Page contains the Individual Post content.
	 */
	public class IndividualPostPage : ContentPage
	{
		private TabbedHomePage NavTabHomePage;
		private FeedCard Card;
		private StackLayout VoteStack;
		private Label PostTitleLabel;
		private Label PostDescriptionLabel;

		public IndividualPostPage (FeedCard feedCard, TabbedHomePage tabbedHomePage)
		{

			//Padding
			this.Padding = new Thickness (15, Device.OnPlatform (20, 0, 0), 15, 5);

			//Background Color
			this.BackgroundColor = Color.FromRgb(232, 237, 236);

			this.NavTabHomePage = tabbedHomePage;
			this.Card = feedCard;

			PostTitleLabel = new Label{
				Text = feedCard.Title,
				FontSize = 20,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.FromRgb(3, 100, 100)
			};

			PostDescriptionLabel = new Label {
				Text = feedCard.Description,
				FontSize = 20,
				TextColor = Color.FromRgb (133, 133, 133)
			};

		}

		async protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var vote = await getVote ();

			Button likeButton = new Button {
				Image = "like.png",
				BackgroundColor = Color.FromRgb(3, 100, 100),
				WidthRequest = 100,
				BorderRadius = 0,
				HorizontalOptions = LayoutOptions.Start
			};

			Button dislikeButton = new Button {
				Image = "dislike.png",
				HorizontalOptions = LayoutOptions.End,
				WidthRequest = 100,
				BorderRadius = 0,
				BackgroundColor = Color.Gray
			};

			if (vote == 1) {
				likeButton.Image = "success.png";
			} else if (vote == -1) {
				dislikeButton.Image = "success.png";
			}

			StackLayout postStack = new StackLayout {
				Spacing = 10,
				BackgroundColor = Color.White,
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(10, 10, 5, 10),
				Children = { PostTitleLabel, PostDescriptionLabel }
			};

			VoteStack = new StackLayout {
				Spacing = 20,
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Children = { likeButton, dislikeButton }
			};

			Content = new ScrollView {
				Content = new StackLayout{
					Orientation = StackOrientation.Vertical,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					Spacing = 20,
					Children = { postStack, VoteStack}
				}
			};

		}

		//Get the votes data asynchronously.
		async private Task<int> getVote(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "userVotes/" + Card.Postid;

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;

			var userVotes = JsonConvert.DeserializeObject<List<UserVotesModel>> (responseJson);
			if (userVotes.Count > 0) {
				return Int32.Parse (userVotes [0].Vote);
			}

			return 0;

		}
	}
}


