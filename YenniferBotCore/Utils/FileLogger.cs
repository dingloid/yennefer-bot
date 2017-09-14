using System;
using System.IO;

namespace YenneferBotCore.Utils
{
    /// <summary>
    /// Represents a logger that will log to a file
    /// </summary>
    public class FileLogger : BaseLogger
    {
        public string FilePath { get; set; }
        public FileLogger(string filePath = @"./logs/yennifer-bot.log")
        {
            FilePath = filePath;
        }

        public override void Log(string message)
        {
            // lock to show that we are currently using this resorce so nothing else can currently write do it
            lock (LockObject)
            {
                using (var file = File.AppendText(FilePath))
                {
                    file.WriteLine($"yennefer-bot: {DateTime.Now:G} - {message}");
                }
            }
        }
    }
}
