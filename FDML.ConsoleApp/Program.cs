// This file was auto-generated by ML.NET Model Builder. 

using System;
using FDML.Model;

namespace FDML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                CaseCode = @"c5bf1468-205c-487a-a137-b87624e35511",
                FkCode = @"1f4a9275-d461-4034-9d26-c911a0be222c",
                DateSoure = @"买债到款",
                JinETypes = @"分公司分配金额",
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual JinE with predicted JinE from sample data...\n\n");
            Console.WriteLine($"CaseCode: {sampleData.CaseCode}");
            Console.WriteLine($"FkCode: {sampleData.FkCode}");
            Console.WriteLine($"DateSoure: {sampleData.DateSoure}");
            Console.WriteLine($"JinETypes: {sampleData.JinETypes}");
            Console.WriteLine($"\n\nPredicted JinE: {predictionResult.Score}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}