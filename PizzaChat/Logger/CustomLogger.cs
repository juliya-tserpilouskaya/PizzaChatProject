using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class CustomLogger
    {
        public void UseLogger(string target, string messaage, string thread, string codePlace)
        {
            string writePath = InitMyLogger();
            switch (target)
            {
                case "INFO":
                    messaage = $"{DateTime.Now:hh:mm:ss yyyy.MM.dd} {target} At {codePlace}. {messaage} Thread: {thread}.";
                    LogWrite(messaage, writePath);
                    break;
                case "DEBUG":
                    messaage = $"{DateTime.Now:hh:mm:ss yyyy.MM.dd} {target} At {codePlace}. {messaage} Thread: {thread}.";
                    LogWrite(messaage, writePath);
                    break;
                case "ERROR":
                    messaage = $"{DateTime.Now:hh:mm:ss yyyy.MM.dd} {target} At {codePlace}. {messaage} Thread: {thread}. \"Error\" item ({{ {Guid.NewGuid()} }})";
                    LogWrite(messaage, writePath);
                    break;
                default:
                    return;
            }
        }

        private string InitMyLogger()
        {
            string LogDir = Path.Combine(Environment.CurrentDirectory, @"..\..\..\" + @"\Log");
            if (!Directory.Exists(LogDir))
            {
                Directory.CreateDirectory(LogDir);
            }
            string[] myFiles = Directory.GetFiles(LogDir);
            int counter = 0;
            string writePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\" + $@"\Log\log {DateTime.Now:yyyyMMdd}_{counter}.txt");
            short logFileLenght = 30_720;
            int[] countNum = new int[myFiles.Length];

            if (File.Exists(writePath))
            {
                for (int i = 0; i < myFiles.Length; i++)
                {
                    string[] split2 = System.IO.Path.GetFileName(myFiles[i]).Split(new char[] { '_', '.' });
                    countNum[i] = Convert.ToInt32(split2[1]);
                }
                for (int i = 0; i < countNum.Length; i++)
                {
                    if (countNum[i] > counter)
                        counter = countNum[i];
                }

                writePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\" + $@"\Log\log {DateTime.Now:yyyyMMdd}_{counter}.txt");
                FileInfo fileInfo = new FileInfo(writePath);

                if (fileInfo.Length >= logFileLenght)
                {
                    counter += 1;
                }
                writePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\" + $@"\Log\log {DateTime.Now:yyyyMMdd}_{counter}.txt");
            }
            return writePath;
        }

        private async void LogWrite(string text, string writePath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void Main()
        {
        }
    }
}
