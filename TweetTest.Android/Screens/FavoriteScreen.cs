using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Tweets.BL;
using Tweets.BL.Managers;

namespace TweetTest.Android.Screens
{
    [Activity(Label = "Favorites")]
    public class FavoriteScreen : Activity
    {
        protected IList<Tweet> tweets;
        protected Adapters.TweetListAdapter tweetList;
        protected ListView tweetsListView = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FavoriteScreen);

            tweetsListView = FindViewById<ListView>(Resource.Id.FavTweetsListView);
        }
        protected override void OnResume()
        {
            base.OnResume();
            tweets = TweetManager.GetTweets();

            // create our adapter
            tweetList = new Adapters.TweetListAdapter(this, tweets);

            //Hook up our adapter to our ListView
            tweetsListView.Adapter = tweetList;
        }
    }
}