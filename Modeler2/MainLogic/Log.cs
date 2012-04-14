using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MainLogic
{
    public static class Log
    {
        internal static string Message { get; private set; }
        internal static List<string> History { get; set; }
        public static event EventHandler MessageChanged;
        internal static void SetMessage(string message)
        {
            Message = message;
            MessageChanged(message, new EventArgs());
            History.Add(message);
        }
        static Log()
        {
            MessageChanged = new EventHandler((o, e) => { Debug.WriteLine(o.ToString()); });
            Message = string.Empty;
            History = new List<string>(10);
        }
    }

}
