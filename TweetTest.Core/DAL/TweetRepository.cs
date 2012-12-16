using System;
using System.Collections.Generic;
using System.IO;
using Tweets.BL;
using Tweets.DL;

namespace Tweets.DAL {
	public class TweetRepository {
		DL.TweetDatabase db = null;
		protected static string dbLocation;		
		protected static TweetRepository me;		

		static TweetRepository ()
		{
			me = new TweetRepository();
		}

        protected TweetRepository()
		{
			// set the db location
			dbLocation = DatabaseFilePath;
			
			// instantiate the database	
			db = new TweetDatabase(dbLocation);
		}
		
		public static string DatabaseFilePath {
			get { 
				var sqliteFilename = "TweetDB.db3";

#if NETFX_CORE
                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);
#else

#if SILVERLIGHT
				// Windows Phone expects a local path, not absolute
	            var path = sqliteFilename;
#else

#if __ANDROID__
				// Just use whatever directory SpecialFolder.Personal returns
	            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
				// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
				// (they don't want non-user-generated data in Documents)
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "../Library/"); // Library folder
#endif
				var path = Path.Combine (libraryPath, sqliteFilename);
#endif		

#endif
				return path;	
			}
		}
        public static Tweet GetTweet(long id)
		{
            return me.db.GetItem<Tweet>(id);
		}
        public static IEnumerable<Tweet> GetTweets()
		{
            return me.db.GetItems<Tweet>();
		}
        public static long SaveTweet(Tweet item)
		{
            return me.db.SaveItem<Tweet>(item);
		}
        public static long DeleteTweet(long id)
		{
            return me.db.DeleteItem<Tweet>(id);
		}
	}
}

