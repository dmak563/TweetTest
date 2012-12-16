using System.Collections.Generic;
using System.Linq;
using Tweets.SAL;
using Tweets.SAL.Abstract;


namespace Tweets.BL.Managers
{
    public static class TweetManager
    {
        static TweetManager()
        {
        }

        public static IList<Tweet> GetTweets()
        {
            var tweets = new List<Tweet>(DAL.TweetRepository.GetTweets());

            foreach (var tweet in tweets)
            {
                tweet.IsFavorite = true;
            }
            IEnumerable<Tweet> sortedTweets = tweets.OrderByDescending(tw => tw.CreatedAt);
            return sortedTweets.ToList();
        }

        public static long SaveTweet(Tweet item)
        {
            return DAL.TweetRepository.SaveTweet(item);
        }

        public static long DeleteTweet(long id)
        {
            return DAL.TweetRepository.DeleteTweet(id);
        }
        public static IList<Tweet> GetTweetsFromTwitter(int count = 20, string twitterUser = "ciklum")
        {
            var tweets = new List<Tweet>();
            ITwitterProvider twitterProvider = new TwitterProvider();
            tweets = twitterProvider.GetTweetsFromTwitter(twitterUser, count);

            foreach (var tweet in tweets)
            {
                Tweet tweetClosure = tweet;
                foreach (var tw in GetTweets().Where(tw => tw.ID== tweetClosure.ID))
                {
                    tweet.IsFavorite = true;
                }
            }
            IEnumerable<Tweet> sortedTweets = tweets.OrderByDescending(tw => tw.CreatedAt);
            return sortedTweets.ToList();
        }
    }
}