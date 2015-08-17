using System;
using Newtonsoft.Json;

namespace AutodeskIdeaConnection
{
	/*
	 * Post Model Representation. 
	 */
	public class PostModel
	{
		[JsonProperty(PropertyName = "postid")]
		public string postid { get; set; }

		[JsonProperty(PropertyName = "type")]
		public string type { get; set; }

		[JsonProperty(PropertyName = "parentid")]
		public string parentid { get; set; }

		[JsonProperty(PropertyName = "categoryid")]
		public string categoryid { get; set; }

		[JsonProperty(PropertyName = "catidpath1")]
		public string catidpath1 { get; set; }

		[JsonProperty(PropertyName = "catidpath2")]
		public string catidpath2 { get; set; }

		[JsonProperty(PropertyName = "catidpath3")]
		public string catidpath3 { get; set; }

		[JsonProperty(PropertyName = "acount")]
		public string acount { get; set; }

		[JsonProperty(PropertyName = "amaxvote")]
		public string amaxvote { get; set; }

		[JsonProperty(PropertyName = "selchildid")]
		public string selchildid { get; set; }

		[JsonProperty(PropertyName = "closedbyid")]
		public string closedbyid { get; set; }

		[JsonProperty(PropertyName = "userid")]
		public string userid { get; set; }

		[JsonProperty(PropertyName = "cookieid")]
		public string cookieid { get; set; }

		[JsonProperty(PropertyName = "createip")]
		public string createip { get; set; }

		[JsonProperty(PropertyName = "lastuserid")]
		public string lastuserid { get; set; }

		[JsonProperty(PropertyName = "lastip")]
		public string lastip { get; set; }

		[JsonProperty(PropertyName = "upvotes")]
		public string upvotes { get; set; }

		[JsonProperty(PropertyName = "downvotes")]
		public string downvotes { get; set; }

		[JsonProperty(PropertyName = "netvotes")]
		public string netvotes { get; set; }

		[JsonProperty(PropertyName = "lastviewip")]
		public string lastviewip { get; set; }

		[JsonProperty(PropertyName = "views")]
		public string views { get; set; }

		[JsonProperty(PropertyName = "hotness")]
		public string hotness { get; set; }

		[JsonProperty(PropertyName = "flagcount")]
		public string flagcount { get; set; }

		[JsonProperty(PropertyName = "format")]
		public string format { get; set; }

		[JsonProperty(PropertyName = "created")]
		public string created { get; set; }

		[JsonProperty(PropertyName = "updated")]
		public string updated { get; set; }

		[JsonProperty(PropertyName = "updatedtype")]
		public string updatedtype { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string title { get; set; }

		[JsonProperty(PropertyName = "content")]
		public string content { get; set; }

		[JsonProperty(PropertyName = "tags")]
		public string tags { get; set; }

		[JsonProperty(PropertyName = "notify")]
		public string notify { get; set; }

		[JsonProperty(PropertyName = "statusid")]
		public string statusid { get; set; }

		[JsonProperty(PropertyName = "files")]
		public string files { get; set; }

		[JsonProperty(PropertyName = "tagsback")]
		public string tagsback { get; set; }

		[JsonProperty(PropertyName = "PropertyName")]
		public string feedbacks { get; set; }

		[JsonProperty(PropertyName = "groupid")]
		public string groupid { get; set; }

		[JsonProperty(PropertyName = "votenotify")]
		public string votenotify { get; set; }

		[JsonProperty(PropertyName = "ownerlastview")]
		public string ownerlastview { get; set; }

		[JsonProperty(PropertyName = "linkedid")]
		public string linkedid { get; set; }

		[JsonProperty(PropertyName = "duplicateid")]
		public string duplicateid { get; set; }

		[JsonProperty(PropertyName = "needvolunteer")]
		public string needvolunteer { get; set; }
	}
}
