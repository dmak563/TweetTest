using System;
using Tweets.DL;

namespace Tweets.BL.Contracts {
	/// <summary>
	/// Business entity base class. Provides the ID property.
	/// </summary>
	public abstract class BusinessEntityBase : IBusinessEntity {
		public BusinessEntityBase ()
		{
		}
		/// <summary>
		/// Gets or sets the Database ID.
		/// </summary>
		[PrimaryKey]
        public long ID { get; set; }
	}
}

