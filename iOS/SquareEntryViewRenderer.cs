using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using AutodeskIdeaConnection;
using AutodeskIdeaConnection.iOS;

[assembly: ExportRenderer (typeof (SquareEntryView), typeof (SquareEntryViewRenderer))]
namespace AutodeskIdeaConnection.iOS
{
	/*
	 * This is a Custom Renderer for Square Entry View.
	 */

	public class SquareEntryViewRenderer : EntryRenderer
	{
		public SquareEntryViewRenderer ()
		{
		}

		// Override the OnElementChanged method so we can tweak this renderer post-initial setup
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				return;
			}

			this.Control.BorderStyle = UIKit.UITextBorderStyle.None;
			this.Control.BackgroundColor =  Color.White.ToUIColor();

		}
	}
}

