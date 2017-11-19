using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Zinc.Extensions
{
    public class Logger
    {
        public Logger(string file_name, string message)
        {
            var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/" + file_name + ".txt";

            using (var writer = new StreamWriter(file, true))
            {
                writer.WriteLine("----------" + DateTime.Now + "----------");
                writer.WriteLine(message);
            }
        }

        public void Dispose(Logger logger)
        {
            logger = null;
        }
    }
}