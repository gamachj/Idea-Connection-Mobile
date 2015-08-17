using System;

namespace AutodeskIdeaConnection
{
	/*
	 * FeedCard Model representation.
	 */
	public class FeedCard
	{
		public FeedCard (string postid, string title, string votes, string submittedOn, string description)
		{
			this.Postid = postid;
			this.Title = title;
			this.Votes = votes;
			this.SubmittedOn = submittedOn;
			this.Description = description;
		}

		public FeedCard(){
		} 

		public string Postid { get; set; }

		public string Title { get; set; }

		public string Votes { get; set; }

		public string Seperator { get { return "|"; } }

		public string SubmittedOn { get; set; }

		public string Description { get; set; }
	}
}

