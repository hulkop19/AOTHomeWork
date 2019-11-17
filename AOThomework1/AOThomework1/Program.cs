using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;

namespace AOThomework1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            var resultFileName = Path.Combine(Environment.CurrentDirectory, "result.txt");
            File.Create(resultFileName).Close();
            List<string> queries = new List<string>
            {
                "Вконтакте"
              , "Танк"
              , "Морской бой"
              , "Конные скачки"
              , "Сплин Выхода нет"
              , "Король и Шут Проклятый старый дом"
            };

            foreach (var query in queries)
            {
                var items = GoogleAPI.GetItems(query);
                var relevanceOkapiBM25WithIDF = RelevanceSort.OkapiBM25(items.ListItems, query, false);
                var orderOkapiBM25WithIDF = RelevanceSort.GetOrderByRelevance(relevanceOkapiBM25WithIDF);
                var relevanceOkapiBM25OnlyTF = RelevanceSort.OkapiBM25(items.ListItems, query, true);
                var orderOkapiBM25OnlyTF = RelevanceSort.GetOrderByRelevance(relevanceOkapiBM25OnlyTF);
                var relevanceTFIDFWithIDF = RelevanceSort.TF_IDF(items.ListItems, query, false);
                var orderTFIDFWithIDF = RelevanceSort.GetOrderByRelevance(relevanceTFIDFWithIDF);
                var relevanceTFIDFOnlyTF = RelevanceSort.TF_IDF(items.ListItems, query, true);
                var orderTFIDFOnlyTF = RelevanceSort.GetOrderByRelevance(relevanceTFIDFOnlyTF);

                PrintResult(items, query, resultFileName
                          , relevanceOkapiBM25WithIDF, orderOkapiBM25WithIDF
                          , relevanceOkapiBM25OnlyTF, orderOkapiBM25OnlyTF
                          , relevanceTFIDFWithIDF, orderTFIDFWithIDF
                          , relevanceTFIDFOnlyTF, orderTFIDFOnlyTF);
            }
        }

        static void PrintResult(Items items, string query, string resultFileName
                              , List<double> relevanceOkapiBM25WithIDF, List<int> orderOkapiBM25WithIDF
                              , List<double> relevanceOkapiBM25OnlyTF, List<int> orderOkapiBM25OnlyTF
                              , List<double> relevanceTFIDFWithIDF, List<int> orderTFIDFWithIDF
                              , List<double> relevanceTFIDFOnlyTF, List<int> orderTFIDFOnlyTF)
        {
            using (var writer = new StreamWriter(resultFileName, true))
            {
                writer.WriteLine($"query: {query}\n");

                for (int i = 0; i < items.ListItems.Count; ++i)
                {
                    writer.WriteLine($"title: {items.ListItems[i].Title}");
                    writer.WriteLine($"link: {items.ListItems[i].Link}");
                    writer.WriteLine($"snippet: {items.ListItems[i].Snippet}");
                    writer.WriteLine($"Text #{i + 1}");
                    writer.WriteLine($"relevanceOkapiBM25WithIDF: {relevanceOkapiBM25WithIDF[i]},\torderOkapiBM25WithIDF: {orderOkapiBM25WithIDF[i]}");
                    writer.WriteLine($"relevanceOkapiBM25OnlyTF: {relevanceOkapiBM25OnlyTF[i]},\torderOkapiBM25OnlyTF: {orderOkapiBM25OnlyTF[i]}");
                    writer.WriteLine($"relevanceTFIDFWithIDF: {relevanceTFIDFWithIDF[i]},\torderTFIDFWithIDF: {orderTFIDFWithIDF[i]}");
                    writer.WriteLine($"relevanceTFIDFOnlyTF: {relevanceTFIDFOnlyTF[i]},\torderTFIDFOnlyTF: {orderTFIDFOnlyTF[i]}");
                }

                writer.WriteLine("###############################################");
            }
        }
    }
}
