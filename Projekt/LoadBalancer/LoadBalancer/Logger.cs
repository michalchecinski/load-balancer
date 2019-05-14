using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class Logger
    {
        private static List<LoggingEntity> logs = new List<LoggingEntity>();
        private static readonly string filePath = $@"../log.txt";

        public static List<LoggingEntity> GetLogs()
        {
            List<LoggingEntity> logs = new List<LoggingEntity>();
            const int BufferSize = 128;
            using (var fileStream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                string line;
                for (int i=1;  (line = streamReader.ReadLine()) != null; i++)
                {
                    if (i > logs.Count)
                    {
                        var splittedLine = line.Split(" ");
                        var logLine = new LoggingEntity(DateTime.Parse(splittedLine[0] + " " + splittedLine[1]),
                                                        splittedLine[2],
                                                        splittedLine[3],
                                                        new Instance(splittedLine[4].Split(":")[0],
                                                        Int32.Parse(splittedLine[4].Split(":")[1])),
                                                        splittedLine[5],
                                                        Int32.Parse(splittedLine[6]));
                        logs.Add(logLine);
                    }
                }
            }
            return logs;
        }

        public static void Log(LoggingEntity loggingEntity)
        {
            logs.Add(loggingEntity);
        }

        public static void Log(string requestUrl, string method, Instance instance, string requestToInstanceUrl, int responseStatusCode)
        {
            var loggingEntity = new LoggingEntity(requestUrl, method, instance, requestToInstanceUrl, responseStatusCode);
            Log(loggingEntity);
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(loggingEntity.ToString());
            }            
        }
    }
}
