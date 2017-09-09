using System;
using System.Collections.Generic;
using System.Text;

namespace YenneferBotCore.Utils
{
    public abstract class BaseLogger
    {
        protected readonly object LockObject = new object();

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public abstract void Log(string message);
    }
}
