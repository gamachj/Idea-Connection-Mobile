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
	 * 	Categories Page contains the contents of the Categories.
	 */
	public class CategoriesPage : ContentPage
	{
		public TabbedHomePage NavTabHomePage { set; get; }

		public int StartingIndex { get; set; }

		private Button moreButton;

		private List<CategoryModel> categories;

		public CategoriesPage (TabbedHomePage navTabPage)
		{
			
			//Padding
			this.Padding = new Thickness (15, Device.OnPlatform (40, 0, 0), 15, 5);

			//Background Color
			this.BackgroundColor = Color.FromRgb(232, 237, 236);

			this.Title = "Categories";

			this.Icon = "categories.png";

			this.NavTabHomePage = navTabPage;

			this.StartingIndex = 0;

			//More Button
			moreButton = new Button {
				Text = "Load More...",
				TextColor = Color.FromRgb(3,100,100),
				BorderWidth = 0
			};

			moreButton.Clicked += (sender, e) => {
				this.StartingIndex += 6;
				OnAppearing();
			};
		}

		async protected override void OnAppearing ()
		{
			base.OnAppearing ();

			//Setting the Page Title
			NavTabHomePage.Title = "Categories";

			//Get Categories
			categories  = await getCategories ();

			//Create a new List of CategoryCards and add categories
			List<CategoryCard> categoryCards = new List<CategoryCard> ();
			for (int i = 0; i < categories.Count; i++) {
				categoryCards.Add (new CategoryCard (categories [i].CategoryId, categories[i].Title));
			}

			//Create a category list and add 6 categories based on the starting index.
			var categoryList = new StackLayout {
				Spacing = 1,
				Padding = new Thickness (5, Device.OnPlatform (5, 0, 0), 5, 5),
				Orientation = StackOrientation.Vertical, 
			};

			for (int i = StartingIndex; i < StartingIndex + 6 && i < categoryCards.Count; i++) {
				categoryList.Children.Add (new CategoryCardTemplate(NavTabHomePage, categoryCards[i]));
			}

			if (StartingIndex + 6 < categoryCards.Count) {
				categoryList.Children.Add (new StackLayout{
					Orientation = StackOrientation.Vertical,
					VerticalOptions = LayoutOptions.CenterAndExpand,
					HorizontalOptions = LayoutOptions.CenterAndExpand,
					Children = { moreButton }
				});
			}

			Content = categoryList;

		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();
			//Set the starting index to zero on disappearing.
			this.StartingIndex = 0;
		}

		/*
		 * Method to retrieve the categories asynchronosly.
		 */
		async Task<List<CategoryModel>> getCategories(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

			//Add the targetURL and credentials for authentication.
			client.BaseAddress = new Uri (targetURL);
			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			string relativeURL = "categories/";

			HttpResponseMessage response = await client.GetAsync(relativeURL);

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;


			var categoryList = JsonConvert.DeserializeObject<List<CategoryModel>> (responseJson);

			return categoryList;

		}
	}

}


