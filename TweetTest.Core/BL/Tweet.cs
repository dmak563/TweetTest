//using Newtonsoft.Json;

using System;
using Tweets.BL.Contracts;
using Tweets.DL;


namespace Tweets.BL
{
    /// <summary>
    /// Represents a Tweet.
    /// </summary>
    public class Tweet : IBusinessEntity
    {
        public Tweet()
        {
            IsFavorite = true;
        }
        [PrimaryKey]
        public long ID { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public string User { get; set; }

        public string FormatedTweet
        {
            get
            {
                return String.Format("{0} / {1} / {2} ", CreatedAt, User, Text);
            }
        }
        public bool IsFavorite { get; set; }
    }
}
