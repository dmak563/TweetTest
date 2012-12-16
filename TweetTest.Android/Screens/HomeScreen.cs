using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TweetTest.Android.Adapters;
using Tweets.BL;
using Tweets.BL.Managers;

namespace TweetTest.Android.Screens
{
    [Activity(Label = "TweetTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class HomeScreen : Activity
    {
        protected IList<Tweet> tweets;
        protected Adapters.TweetListAdapter tweetList;
        protected Button GoToFavorites = null;
        protected Button RefreshButton = null;
        protected ListView TweetsListView = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.HomeScreen);

            TweetsListView = FindViewById<ListView>(Resource.Id.TweetsListView);
            GoToFavorites = FindViewById<Button>(Resource.Id.MyButton);
            // wire up go to favorites button handler
            if (GoToFavorites != null)
            {
                GoToFavorites.Click += (sender, e) => StartActivity(typeof(FavoriteScreen));
            }
            // wire up go to refresh button handler
            RefreshButton = FindViewById<Button>(Resource.Id.Refresh);
            if (RefreshButton != null)
            {
                RefreshButton.Click += RefreshClick;
            }

        }
        private void RefreshClick(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Refreshing", ToastLength.Short).Show();
            ThreadPool.QueueUserWorkItem(o => RefreshAsync());
        }
        private void RefreshAsync()
        {
            tweets = TweetManager.GetTweetsFromTwitter();
            tweetList = new TweetListAdapter(this, tweets);
            RunOnUiThread(() =>
            {
                TweetsListView.Adapter = tweetList;
                Toast.MakeText(this, "Refreshed", ToastLength.Short).Show();
            });
        }

        protected override void OnResume()
        {
            base.OnResume();
            tweets = TweetManager.GetTweetsFromTwitter();

            // create our adapter
            tweetList = new Adapters.TweetListAdapter(this, tweets);

            //Hook up our adapter to our ListView
            TweetsListView.Adapter = tweetList;
        }
    }
}