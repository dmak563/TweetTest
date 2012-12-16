using System.Linq;
using System.Collections.Generic;
using Tweets.BL;

namespace Tweets.DL
{
    /// <summary>
    /// TweetDatabase builds on SQLite.Net and represents a specific database, in our case, the Tweet DB.
    /// It contains methods for retrieval and persistance as well as db creation, all based on the 
    /// underlying ORM.
    /// </summary>
    public class TweetDatabase : SQLiteConnection
    {
        static object locker = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="Tweets.DL.TweetDatabase"/> TweetDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public TweetDatabase(string path)
            : base(path)
        {
            // create the tables
            CreateTable<Tweet>();
        }

        public IEnumerable<T> GetItems<T>() where T : BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return (from i in Table<T>() select i).ToList();
            }
        }

        public T GetItem<T>(long id) where T : BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
                return Table<T>().FirstOrDefault(x => x.ID == id);
                // Following throws NotSupportedException - thanks aliegeni
                //return (from i in Table<T> ()
                //        where i.ID == id
                //        select i).FirstOrDefault ();
            }
        }

        public long SaveItem<T>(T item) where T : BL.Contracts.IBusinessEntity
        {
            lock (locker)
            {
                var tmp = GetItem<Tweet>(item.ID);
                if (tmp ==null)
                {
                    return Insert(item);
                }
                return item.ID;
            }
        }

        public int DeleteItem<T>(long id) where T : BL.Contracts.IBusinessEntity, new()
        {
            lock (locker)
            {
#if NETFX_CORE
                return Delete(new T() { ID = id });
#else
                return Delete<T>(new T() { ID = id });
#endif
            }
        }
    }
}