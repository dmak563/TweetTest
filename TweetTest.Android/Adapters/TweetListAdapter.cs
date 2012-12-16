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

namespace TweetTest.Android.Adapters
{
    public class TweetListAdapter : BaseAdapter<Tweet>
    {
        protected Activity context = null;
        protected IList<Tweet> tweets = new List<Tweet>();

        public TweetListAdapter(Activity context, IList<Tweet> tweets)
            : base()
        {
            this.context = context;
            this.tweets = tweets;
        }

        public override long GetItemId(int position)
        {
            return tweets[position].ID;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = tweets[position];

            var view = context.LayoutInflater.Inflate(
                    Android.Resource.Layout.MySecondSimpleListItem,
                    parent,
                    false);
            
            view.FindViewById<TextView>(Android.Resource.Id.txtTitle).SetText(item.CreatedAt.ToString("dd MMM yyyy "), TextView.BufferType.Normal);
            view.FindViewById<TextView>(Android.Resource.Id.txtContent).SetText(item.Text, TextView.BufferType.Normal);
            ImageButton imageButton = view.FindViewById<ImageButton>(Android.Resource.Id.IsFavorite) ?? view.FindViewById<ImageButton>(position);

            int imageId = item.IsFavorite
                              ? Android.Resource.Drawable.star_on
                              : Android.Resource.Drawable.star_off;
            imageButton.Id = position;
            imageButton.SetImageResource(imageId);
            imageButton.Click += (o, e) =>
                                     {
                                         if (!item.IsFavorite) 
                                         {
                                             Save(item);
                                             imageButton.SetImageResource(Android.Resource.Drawable.star_on);
                                         }
                                         else
                                         {
                                             Delete(item);
                                             imageButton.SetImageResource(Android.Resource.Drawable.star_off);
                                         }
                                     };
            return view;
        }

        private void Delete(Tweet tweet)
        {
            tweet.IsFavorite = false;
            TweetManager.DeleteTweet(tweet.ID);
        }

        private void Save(Tweet tweet)
        {
            tweet.IsFavorite = true;
            TweetManager.SaveTweet(tweet);
        }

        public override int Count
        {
            get { return tweets.Count; }
        }

        public override Tweet this[int position]
        {
            get { return tweets[position]; }
        }
    }
}