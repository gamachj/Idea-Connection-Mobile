using System;

using Xamarin.Forms;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace AutodeskIdeaConnection
{
	public class PostPage : ContentPage
	{
		public TabbedHomePage NavTabHomePage { set; get; }

		private SquareEntryView titleEntry;
		private Editor describeEditor;
		private SquarePickerView categoryPicker;
		private Button postButton;
		private Label titleLabel;
		private Label describeLabel;
		private Label categoryLabel;

		public PostPage(TabbedHomePage navTabPage){
			  
			//Padding
			this.Padding = new Thickness (15, Device.OnPlatform (20, 0, 0), 15, 5);

			//Background Color
			this.BackgroundColor = Color.FromRgb(232, 237, 236);

			//Icon Title and Image
			this.Title = "Post";
			this.Icon = "add.png";

			this.NavTabHomePage = navTabPage;

			NavTabHomePage.Title = "Submit Idea";

			//Title Label
			titleLabel = new Label {
				Text = "Title",
				FontSize = 20
			};
			//Title Entry
			titleEntry = new SquareEntryView {
				HeightRequest = 35
			};

			//Describe your Idea Label
			describeLabel = new Label {
				Text = "Describe your idea",
				FontSize = 20
			};

			//Describe your Idea Editor
			describeEditor = new Editor {
				HeightRequest = 100
			};

			//Category Label
			categoryLabel = new Label {
				Text = "Category",
				FontSize = 20
			};
			//Category Dropdown
			categoryPicker = new SquarePickerView {
				HeightRequest = 35,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			//Post Your Idea
			postButton = new Button {
				Text = "POST YOUR IDEA",
				BackgroundColor = Color.FromRgb(3,100,100),
				TextColor = Color.White,
				Font = Font.SystemFontOfSize(NamedSize.Large),
				BorderRadius = 0
			};

			postButton.Clicked += PostButton_Clicked;

		}

		async void PostButton_Clicked (object sender, EventArgs e)
		{
			//In Progress!
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			handler.Credentials = new System.Net.NetworkCredential (NavTabHomePage.Username, NavTabHomePage.Password);

			HttpClient client = new HttpClient (handler);

			client.BaseAddress = new Uri (targetURL);

			//var authinfo = Convert.ToBase64String(System.Text.Encoding.GetEncoding ("ISO-8859-1").GetBytes (string.Format ("{0}:{1}", "", "")));
			//client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authinfo);

			string relativeURL = "checkIdentity";

			var jsonRequest = new { title = titleEntry.Text, description = describeEditor.Text};
			var serializedJsonRequest = JsonConvert.SerializeObject ( jsonRequest );	
			HttpContent content = new StringContent (serializedJsonRequest, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync( relativeURL, content );
			//HttpResponseMessage response = await client.GetAsync(relativeURL );

			if (!response.IsSuccessStatusCode) {
				Debug.WriteLine (response.StatusCode.ToString ());
				OnAppearing ();
			}

			var responseJson = response.Content.ReadAsStringAsync().Result;


			//var submittedPost = JsonConvert.DeserializeObject<List<PostModel>> (responseJson);

			Debug.WriteLine(responseJson);

		}

		async protected override void OnAppearing ()
		{
			base.OnAppearing ();

			//Title Stack of Label and Entry
			StackLayout titleStack = new StackLayout {
				Children = { titleLabel, titleEntry }
			};

			//Describe your Idea Stack
			StackLayout describeStack = new StackLayout {
				Children = { describeLabel, describeEditor }
			};

			//calling the categories
			List<CategoryModel> categories = await getCategories();

			for (int i = 0; i < categories.Count; i++) {
				categoryPicker.Items.Add (categories[i].Title);	
			};

			//Category Stack
			StackLayout categoryStack = new StackLayout {
				Children = { categoryLabel, categoryPicker }
			};

			Content = new ScrollView {
				Content = new StackLayout{
					Spacing = 20,
					Children = {titleStack, describeStack, categoryStack, postButton }
				}
			};

		}

		async Task<List<CategoryModel>> getCategories(){
			string targetURL = "http://shacng83212x1:8087/Idea_Mobile_Dev/mobile_api/index.php/";
			HttpClientHandler handler = new HttpClientHandler ();

			HttpClient client = new HttpClient (handler);

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