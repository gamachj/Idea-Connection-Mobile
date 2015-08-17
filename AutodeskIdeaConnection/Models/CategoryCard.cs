using System;

namespace AutodeskIdeaConnection
{
	/*
	 * CategoryCard Model representation.
	 */
	public class CategoryCard
	{
		public CategoryCard (string categoryId, string categoryName)
		{
			this.CategoryId = categoryId;
			this.CategoryName = categoryName;
		}

		public string CategoryId{ get; set; }
		public string CategoryName { set; get; }
		public string NavigationIcon { get { return ">"; } }
	}
}

