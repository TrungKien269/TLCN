using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class PythonHelper
    {
        public static List<string> GetRelatedBooks(string id)
        {
            try
            {
                string fileName = @"E:\CodePython\Draft\Draft\Draft.py";
                string python_path = @"C:\Users\Trung Kien\AppData\Local\Programs\Python\Python35\python.exe";

                ProcessStartInfo start = new ProcessStartInfo();

                start.FileName = python_path;
                start.Arguments = fileName + " " + id;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;

                List<string> RelatedList = new List<string>();
                string line;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            RelatedList.Add(line);
                        }
                    }
                }

                return RelatedList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<string> GetSuggestedBooks(int userID)
        {
            try
            {
                string fileName = @"E:\CodePython\BookSuggestion\BookSuggestion\BookSuggestion.py";
                string python_path = @"C:\Users\Trung Kien\AppData\Local\Programs\Python\Python35\python.exe";

                ProcessStartInfo start = new ProcessStartInfo();

                start.FileName = python_path;
                start.Arguments = fileName + " " + userID;
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;

                List<string> RelatedList = new List<string>();
                string line;

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            RelatedList.Add(line);
                        }
                    }
                }
                return RelatedList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
