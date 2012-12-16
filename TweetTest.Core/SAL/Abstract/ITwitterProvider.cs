using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweets.BL;

namespace Tweets.SAL.Abstract
{
    interface ITwitterProvider
    {
        List<Tweet> GetTweetsFromTwitter(string twitterUser, int count);
    }
}
