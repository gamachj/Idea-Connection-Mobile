using System;

using Xamarin.Forms;

namespace AutodeskIdeaConnection
{
	/*
	 * FeedCardTemplate generates a card that can be embedded in IdeaFeedPage.
	 */
	public class FeedCardTemplate : ContentView
	{
		private TabbedHomePage TabbedHomePage;
		public FeedCardTemplate (FeedCard card, TabbedHomePage tabbedHomepage)
		{
			this.BackgroundColor = Color.White;

			this.Padding = new Thickness (10, 10, 10, 5);

			this.TabbedHomePage = tabbedHomepage;

			//Elements of the Layout
			Label title = new Label {
				Text = card.Title,
				TextColor = Color.FromRgb (3, 100, 100),
				FontAttributes = FontAttributes.Bold,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null)
			};

			Label votes = new Label {
				Text = card.Votes,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null),
			};

			Label seperation_bar = new Label {
				Text = card.Seperator,
				TextColor = Color.FromRgb (3, 100, 100),
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null),
			};

			Label submittedOn_label = new Label {
				Text = card.SubmittedOn,
				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null),
			};

			Label description = new Label {
				Text = card.Description,
				HeightRequest = 40,
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
				FontFamily = Device.OnPlatform(iOS: "sans-serif", Android: "sans-serif", WinPhone: null),
				TextColor = Color.Gray
			};

			//ADDING THE ELEMENTS TO THE STACK
			StackLayout middleStack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Children = { votes, seperation_bar, submittedOn_label }
			};

			StackLayout feedCardTemplate = new StackLayout{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { title, middleStack, description }
			};

			var tgr = new TapGestureRecognizer ();
			tgr.Tapped += (sender, e) => onFeedCardClicked(card);

			this.GestureRecognizers.Add (tgr);

			Content = feedCardTemplate;
		}

		//New Individual Page is fed into the Navigation Stack on pressing a Post.
		public void onFeedCardClicked(FeedCard feedCard){
			Navigation.PushAsync (new IndividualPostPage (feedCard, TabbedHomePage));
		}
	}
}


