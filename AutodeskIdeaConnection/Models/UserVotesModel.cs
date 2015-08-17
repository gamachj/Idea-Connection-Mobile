using System;
using Newtonsoft.Json;

namespace AutodeskIdeaConnection
{
	/*
	 * UserVotes Model representation.
	 */
	public class UserVotesModel
	{
		[JsonProperty(PropertyName = "postid")]
		public string Postid{ get; set; }

		[JsonProperty(PropertyName = "userid")]
		public string Userid{ get; set; }

		[JsonProperty(PropertyName = "vote")]
		public string Vote{ get; set; }

		[JsonProperty(PropertyName = "flag")]
		public string Flag{ get; set; }

		[JsonProperty(PropertyName = "voted")]
		public string Voted{ get; set; }

		[JsonProperty(PropertyName = "point")]
		public string Point{ get; set; }

	}
}

