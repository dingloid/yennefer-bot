namespace YenneferBotCore.Utils
{
    public static class Logger
    {
        private static BaseLogger _logger;

        /// <summary>
        /// Logs the message to the log.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            _logger = new FileLogger();
            _logger.Log(message);
        }
    }
}
