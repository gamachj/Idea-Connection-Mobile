using System;
using Newtonsoft.Json;

namespace AutodeskIdeaConnection
{
	/*
	 * Notification Model representation.
	 */
	public class NotificationModel
	{

		[JsonProperty(PropertyName = "postid")]
		public string Postid { get; set; }

		[JsonProperty(PropertyName = "parentid")]
		public string Parentid { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		[JsonProperty(PropertyName = "displayname")]
		public string Displayname { get; set; }

		[JsonProperty(PropertyName = "created")]
		public string Created { get; set; }

		[JsonProperty(PropertyName = "updated")]
		public string Updated { get; set; }
	}
}

