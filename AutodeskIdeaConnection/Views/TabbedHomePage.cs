using System;

using Xamarin.Forms;
using System.Diagnostics;

namespace AutodeskIdeaConnection
{
	public class TabbedHomePage : TabbedPage
	{ 
		public IdeaFeedPage IdeaFeed { set; get; }
		public PostPage Post { set; get; }
		public CategoriesPage Categories { get; set; }
		public NotificationsPage Notifications { get; set; }

		public string Username { get; }
		public string Password { get; }

		public TabbedHomePage (string username, string password)
		{
			this.Title = "Idea Connection";

			this.Username = username;
			this.Password = password;

			IdeaFeed = new IdeaFeedPage (this);
			Post = new PostPage (this);
			Categories = new CategoriesPage (this);
			Notifications = new NotificationsPage (this);


			this.Children.Add (IdeaFeed);

			this.Children.Add (Post);

			this.Children.Add (Categories);

			this.Children.Add (Notifications);

		}

		protected override bool OnBackButtonPressed ()
		{
			Debug.WriteLine ("back");
			return base.OnBackButtonPressed ();

		}

		protected override void OnCurrentPageChanged ()
		{
			base.OnCurrentPageChanged ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
		}

	}
}


