using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AutodeskIdeaConnection;
using AutodeskIdeaConnection.iOS;
using UIKit;

[assembly: ExportRenderer (typeof (IdeaFeedPage), typeof (IdeaFeedPageRenderer))]
namespace AutodeskIdeaConnection.iOS
{
	/*
	 * This page is intended to modify the Idea Feed Render Page.
	 */
	public class IdeaFeedPageRenderer : PageRenderer
	{
		public IdeaFeedPageRenderer(){
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
		}

			

	}
}

