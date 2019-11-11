using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++) 
            {

                string writePath = $@"D:\Dir\log {DateTime.Now:yyyyMMdd}_0.txt";
                writePath = InitMyLogger(writePath);
                string target = "INFO";
                string text;
                string messaage = "Hello!";

                switch (target)
                {
                    case "INFO":
                        text = $" Thread {DateTime.Now:hh:mm:ss yyyy.MM.dd} {target.GetType()} INFO {messaage} \"Error\" item ({{ {Guid.NewGuid()} }})";
                        INFOLog(text, writePath);
                        break;
                    case "DEBUG":
                        break;
                    case "ERROR":
                        break;
                    default:
                        return;
                }
                Console.ReadKey();
            }        
        }

        public static string InitMyLogger(string writePath) 
        {
            string[] myFiles = Directory.GetFiles(@"D:\Dir");
            Console.WriteLine(myFiles.Length);
            var counter = 0;
            string Name;

            if (!File.Exists(writePath))
                //File.Create(writePath);
                writePath = writePath;
            else
            {
                Name = myFiles[myFiles.Length - 1];
                Console.WriteLine(Name);
                FileInfo fileInfo = new FileInfo(Name);
                Console.WriteLine(fileInfo.Length);
                if (fileInfo.Length >= 30_000)
                {
                    string[] split = Name.Split(new Char[] { '_', '.' });
                    counter = Convert.ToInt32(split[1]) + 1;
                    writePath = $@"D:\Dir\log {DateTime.Now:yyyyMMdd}_{counter}.txt";
                    //File.Create(writePath);
                }
                else
                {
                    string[] split = Name.Split(new Char[] { '_', '.' });
                    counter = Convert.ToInt32(split[1]);
                    writePath = $@"D:\Dir\log {DateTime.Now:yyyyMMdd}_{counter}.txt";
                }
            }
            return writePath;
        }

        public static async void INFOLog(string text, string writePath)
        {
            try
            {
                //using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                //{
                //    await sw.WriteLineAsync(text);
                //}

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(text);
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
