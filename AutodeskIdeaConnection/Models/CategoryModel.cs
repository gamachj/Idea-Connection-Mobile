using System;
using Newtonsoft.Json;

namespace AutodeskIdeaConnection
{
	/*
	 * Category Model representation.
	 */
	public class CategoryModel
	{
		[JsonProperty(PropertyName = "categoryid")]
		public string CategoryId { get; set; }

		[JsonProperty(PropertyName = "parentid")]
		public string ParentId { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "tags")]
		public string Tags { get; set; }

		[JsonProperty(PropertyName = "Content")]
		public string Content { get; set; }

		[JsonProperty(PropertyName = "qcount")]
		public string QCount { get; set; }

		[JsonProperty(PropertyName = "position")]
		public string Position { get; set; }

		[JsonProperty(PropertyName = "backpath")]
		public string Backpath { get; set; }

		[JsonProperty(PropertyName = "community_managerid")]
		public string CommunityManagerId { get; set; }
	}
}

