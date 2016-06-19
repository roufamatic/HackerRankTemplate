using System;
using System.Diagnostics;
using System.IO;
using System.Text;

class Solution
{
    static void Main(String[] args)
    {
        if (Debugger.IsAttached)
        {
            // Place input in a file called "input.txt" alongside Program.cs
            // Optionally, place expected output in a file called "expectedOutput.txt" alongside Program.cs
            // Make sure both files are copied to the output directory.
            _readerWriter = new TestFileReaderWriter();
        }
        else
        {
            // The normal case.
            _readerWriter = new ConsoleReaderWriter();
        }

        //
        // YOUR CODE HERE
        //

        if (Debugger.IsAttached)
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }

    #region Stuff to make using HackerRank easier...
    interface IInputReaderWriter
    {
        string ReadLine();
        long ReadLineToLong();
        int ReadLineToInt();
        int[] ReadLineToIntArray();
        bool[] ReadLineToBoolArray(Func<char, bool> converter);
        void WriteLine();
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteLine(object o);
    }

    abstract class ReaderWriterBase : IInputReaderWriter, IDisposable
    {
        public abstract string ReadLine();

        public abstract void Write(string format, params object[] args);

        public abstract void WriteLine(string format, params object[] args);

        public void WriteLine(object o)
        {
            WriteLine(o.ToString());
        }

        public abstract void WriteLine();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            // no-op
        }

        public int ReadLineToInt()
        {
            return int.Parse(ReadLine().Trim());
        }

        public long ReadLineToLong()
        {
            return long.Parse(ReadLine().Trim());
        }

        public int[] ReadLineToIntArray()
        {
            return Array.ConvertAll(ReadLine().Trim().Split(' '), int.Parse);
        }

        public bool[] ReadLineToBoolArray(Func<char, bool> converter)
        {
            return Array.ConvertAll(ReadLine().ToCharArray(), converter.Invoke);
        }

    }

    class ConsoleReaderWriter : ReaderWriterBase
    {
        /// <summary>
        /// Reads a line of characters from the text reader and returns the data as a string.
        /// </summary>
        /// <returns>
        /// The next line from the reader, or null if all characters have been read.
        /// </returns>
        /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception><exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception><exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader"/> is closed. </exception><exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue"/></exception>
        public override string ReadLine()
        {
            return Console.ReadLine();
        }

        public override void WriteLine()
        {
            Console.WriteLine();
        }

        public override void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public override void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
    }

    class TestFileReaderWriter : ReaderWriterBase
    {
        private TextReader _myReader = File.OpenText("input.txt");
        private TextReader _myOutputReader = File.Exists("expectedOutput.txt") ? File.OpenText("expectedOutput.txt") : null;
        private StringBuilder _currentLine = new StringBuilder();
        private int currentLineNumber = 1;
        public override string ReadLine()
        {
            return _myReader.ReadLine();
        }

        public override void WriteLine()
        {
            CheckCurrentLine();
            _currentLine.Clear();
            currentLineNumber++;
        }

        public override void Write(string format, params object[] args)
        {
            _currentLine.AppendFormat(format, args);
        }

        public override void WriteLine(string format, params object[] args)
        {
            Write(format, args);
            WriteLine();
        }

        private void CheckCurrentLine()
        {
            Console.WriteLine(_currentLine);
            if (_myOutputReader != null)
            {
                var nextLine = _myOutputReader.ReadLine().Trim();
                var currentLine = _currentLine.ToString().Trim();

                if (nextLine != currentLine) { throw new Exception("oy: line " + currentLineNumber); }
            }
        }

        public override void Dispose()
        {
            foreach (var tr in new[] { _myReader, _myOutputReader })
            {
                tr.Close();
                tr.Dispose();
            }
        }
    }

    private static IInputReaderWriter _readerWriter;
    #endregion
}
