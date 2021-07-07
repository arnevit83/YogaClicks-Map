using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Preview.IO
{
    public class FileWriter : IDisposable
    {
        private readonly StreamWriter _writer;
        private readonly object _locker;

        private bool _disposed;

        public FileWriter(string path)
        {
            _disposed = false;

            _writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read));
            _locker = new object();

            _writer.AutoFlush = true;
        }

        public void RecordEmailAddress(string emailAddress)
        {
            lock (_locker)
            {
                if (_disposed)
                    throw new ObjectDisposedException("FileWriter");

                _writer.WriteLine(emailAddress);
            }
        }

        public void Dispose()
        {
            lock (_locker)
            {
                if (_disposed)
                    return;

                _writer.Dispose();
            }
        }
    }
}