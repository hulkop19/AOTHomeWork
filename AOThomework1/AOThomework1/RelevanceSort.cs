using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AOThomework1
{
    public static class RelevanceSort
    {
        public static List<double> OkapiBM25(List<Item> items, string query, bool onlyTF)
        {
            var words = TextPreprocessing.GetTokens(query);
            List<double> relevance = new List<double>();

            for (int docNumber = 0; docNumber < items.Count; ++docNumber)
            {
                double tfidf = 0;
                for (int queryWordNumber = 0; queryWordNumber < words.Count(); ++queryWordNumber)
                {
                    double idf = 1;
                    if (!onlyTF)
                    {
                        idf = Math.Log((double)items.Count / items.Where((item) => TextPreprocessing.GetTokens(item.Snippet).Contains(words[queryWordNumber])).Count());
                    }

                    double tf = (double)TextPreprocessing.GetTokens(items[docNumber].Snippet).Where((w) => w == words[queryWordNumber]).Count()
                                / TextPreprocessing.GetTokens(items[docNumber].Snippet).Count();

                    tfidf += idf * tf * 3
                          / (tf + 2 * (0.25 + 0.75 * TextPreprocessing.GetTokens(items[docNumber].Snippet).Count()
                          / (double)(items.Sum((item) => TextPreprocessing.GetTokens(item.Snippet).Count()) / items.Count)));
                }

                relevance.Add(tfidf);
            }

            return relevance;
        }

        public static List<double> TF_IDF(List<Item> items, string query, bool onlyTF)
        {
            var words = TextPreprocessing.GetTokens(query);
            List<double> relevance = new List<double>();

            for (int docNumber = 0; docNumber < items.Count; ++docNumber)
            {
                double tfidf = 0;
                for (int queryWordNumber = 0; queryWordNumber < words.Count(); ++queryWordNumber)
                {
                    double idf = 1;
                    if (!onlyTF)
                    {
                        idf = Math.Log((double)items.Count / items.Where((item) => TextPreprocessing.GetTokens(item.Snippet).Contains(words[queryWordNumber])).Count());
                    }

                    double tf = (double)TextPreprocessing.GetTokens(items[docNumber].Snippet).Where((w) => w == words[queryWordNumber]).Count()
                                / (double)TextPreprocessing.GetTokens(items[docNumber].Snippet).Count();

                    tfidf += idf * tf;
                }

                relevance.Add(tfidf);
            }

            return relevance;
        }

        public static List<int> GetOrderByRelevance(List<double> relevance)
        {
            var list = new List<(double, int)>();

            for (int i = 0; i < relevance.Count; ++i)
            {
                list.Add((relevance[i], i + 1));
            }

            list.Sort();
            list.Reverse();

            var order = new List<int>(new int[20]);

            for (int i = 0; i < list.Count; ++i)
            {
                order[list[i].Item2 - 1] = i + 1;
            }

            return order;
        }
    }
}
