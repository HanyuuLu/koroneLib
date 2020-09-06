using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoroneLibrary.Models
{
    public class Search
    {
        static Random random = new Random();

        public List<Article> articleList { get; set; }
        public Search SearchDetails(Dictionary<String,String> searchDict = null)
        {
            Search result = new Search();
            result.articleList = new List<Article>();
            int count = random.Next(1,100);
            for(var i = 0;i<count;++i)
            {
                Article article = new Article();
                article.author = $"示例作者{random.Next().ToString()}";
                article.body = $"示例正文{random.Next().ToString()}";
                article.title = $"示例标题{random.Next().ToString()}";
                article.grade = $"示例年级{random.Next().ToString()}";
                var dict = new Dictionary<string, string>();
                dict.Add("N1", $"示例注释{random.Next().ToString()}");
                article.node = dict;
                article.tag = $"示例标签{random.Next().ToString()}";
                result.articleList.Add(article);
            }
            return result;
        }
    }
}
