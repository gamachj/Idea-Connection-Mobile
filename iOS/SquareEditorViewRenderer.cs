using System;
using Xamarin.Forms;
using AutodeskIdeaConnection;
using AutodeskIdeaConnection.iOS;

[assembly: ExportRenderer (typeof (SquarePickerView), typeof (SquarePickerViewRenderer))]
namespace AutodeskIdeaConnection.iOS
{
	using Xamarin.Forms;
	using Xamarin.Forms.Platform.iOS;
	using UIKit;

	/*
	 * This class is intended for Square Picker View Renderer
	 */ 
	public class SquarePickerViewRenderer : PickerRenderer
	{
		public SquarePickerViewRenderer () 
		{
		}

		//Overriding the OnElementChanged method to custom UI element
		protected override void OnElementChanged (ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				return;
			}
		
			this.Control.Layer.CornerRadius = 0;
			this.Control.BackgroundColor = Color.White.ToUIColor();
		}
	}
}

