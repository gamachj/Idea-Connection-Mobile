using System;

namespace AutodeskIdeaConnection
{
	/*
	 * NotificationsCard Model representation.
	 */
	public class NotificationsCard
	{
		public NotificationsCard (string postid, string parentid, string displayname, string type, string created, string updated)
		{
			this.Postid = postid;
			this.Displayname = displayname;
			this.Type = type;
			this.Parentid = parentid;

			DateTime convertedDateTime;
			if (updated == null) {
			 	convertedDateTime = DateTime.ParseExact (created, "yyyy-MM-dd HH:mm:ss",
					                             System.Globalization.CultureInfo.InvariantCulture);
				this.Date = convertedDateTime.Month + "/" + convertedDateTime.Day + "/" + (convertedDateTime.Year % 2000);
				this.Time = convertedDateTime.Hour + ":" + convertedDateTime.Minute;
			}else{
				convertedDateTime = DateTime.ParseExact (created, "yyyy-MM-dd HH:mm:ss",
					System.Globalization.CultureInfo.InvariantCulture);
				this.Date = convertedDateTime.Month + "/" + convertedDateTime.Day + "/" + (convertedDateTime.Year % 2000);
				this.Time = convertedDateTime.Hour + ":" + convertedDateTime.Minute;
			}

			if (Type == "C") {
				Message = Displayname + " commented on your post";
			} else {
				Message = Displayname + " answered on your post";
			}
		}

		public string Postid { get; set; }

		public string Parentid { get; set; }

		public string Displayname {set; get; }

		public string Type { get; set; }

		public string Date { get; set; }

		public string Time { get; set; }

		public string Message { get; }

		public string NavigationIcon { get { return ">"; } }
	}
}

