using System.IO;
using edu.stanford.nlp.ie.crf;
using Console = System.Console;

namespace AOTHW4
{
    class Program
    {
        static void Main(string[] args)
        {

            var jarRoot = @"stanford-ner-2016-10-31";
            var classifiersDirecrory = jarRoot + @"\classifiers";


            var classifier = CRFClassifier.getClassifierNoExceptions(
                classifiersDirecrory + @"\english.all.3class.distsim.crf.ser.gz");

            var rawFileNames = Directory.GetFiles(@"Texts");
            var markedFileNames = Directory.GetFiles(@"MarkedTexts");

            for (int i = 0; i < rawFileNames.Length; ++i)
            {
                using (var rawReader = new StreamReader(rawFileNames[i]))
                using (var markedReader = new StreamReader(markedFileNames[i]))
                {
                    string rawText = rawReader.ReadToEnd();
                    string rightMarkedText = markedReader.ReadToEnd();

                    var markedText = classifier.classifyWithInlineXML(rawText);
                    Console.WriteLine($"File Name: {Path.GetFileName(rawFileNames[i])}\n");
                    Console.WriteLine($"{markedText}\n\n");

                    Console.WriteLine($"{rightMarkedText}\n");
                }
            }
        }
    }
}
