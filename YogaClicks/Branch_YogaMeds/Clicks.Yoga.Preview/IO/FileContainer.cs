using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Preview.IO
{
    public class FileContainer : IDisposable
    {
        public static FileContainer Value = new FileContainer();

        private bool _disposed;

        public FileContainer()
        {
            _disposed = false;

            var path = ConfigurationManager.AppSettings["directoryPath"];

            Teachers = new FileWriter(Path.Combine(path, "Teachers.txt"));
            Venues = new FileWriter(Path.Combine(path, "Venues.txt"));
            Students = new FileWriter(Path.Combine(path, "Students.txt"));
        }

        public FileWriter Teachers { get; private set; }

        public FileWriter Venues { get; private set; }

        public FileWriter Students { get; private set; }

        public void Dispose()
        {
            if (_disposed)
                return;

            Teachers.Dispose();
            Venues.Dispose();
            Students.Dispose();
        }
    }
}