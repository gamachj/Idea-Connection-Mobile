using System;

using Xamarin.Forms;
using System.Diagnostics;

namespace AutodeskIdeaConnection
{
	/*
	 * CategoryCardTemplate generates a card that can be embedded in CategoriesPage.
	 */
	public class CategoryCardTemplate : ContentView
	{
		private TabbedHomePage NavTabHomePage;

		/*
		 *  Data binding of Category card and template.
		 */
		public CategoryCardTemplate (TabbedHomePage navTabHomePage, CategoryCard categoryCard) 
		{
			this.NavTabHomePage = navTabHomePage;

			this.BindingContext = categoryCard;

			var categoryName = new Label {
				HorizontalOptions = LayoutOptions.CenterAndExpand
			};
			categoryName.SetBinding (Label.TextProperty, new Binding ("CategoryName"));

			var navigationIcon = new Label {
				HorizontalOptions = LayoutOptions.End,
				TextColor = Color.FromRgb (3, 100, 100)
			};
			navigationIcon.SetBinding (Label.TextProperty, new Binding ("NavigationIcon"));

			var RowTemplate = new StackLayout {
				BackgroundColor = Color.White,
				HeightRequest = 30,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness (5, Device.OnPlatform (5, 0, 0), 5, 5),
				Children = { categoryName, navigationIcon }
			};

			var tgr = new TapGestureRecognizer ();
			tgr.Tapped +=  (sender, e) => onCategoryCardClicked(categoryCard);
			this.GestureRecognizers.Add (tgr);

			Content = RowTemplate;
		}

		private void onCategoryCardClicked(CategoryCard categoryCard){

			NavTabHomePage.IdeaFeed.Category = categoryCard;

			NavTabHomePage.CurrentPage = NavTabHomePage.IdeaFeed;
		}
	}
}


