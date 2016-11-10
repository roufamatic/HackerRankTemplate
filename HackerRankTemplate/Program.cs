using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

class Solution
{
    // Implement this method to solve the puzzle. Use readerWriter methods to read input / write output.
    // For local runs: 
    //    place input in a file called "input.txt" alongside Program.cs. 
    //    optionally, place output in a file called "expectedOutput.txt" -- an error will be thrown as soon as an expected line doesn't match your output.
    //    Make sure the files are copied to the output directory (in properties).

    private static void SolvePuzzle(IReaderWriter readerWriter)
    {
        // Your code goes in this method.
        // Consider using the following snippet if there are T test cases presented on line one.
        int testCases = readerWriter.ReadLineToInt();
        for (int t = 0; t < testCases; t++)
        {
        }
    }

    #region Stuff to make life easier...

    static void Main(String[] args)
    {
        IReaderWriter readerWriter = null;
        try
        {
            if (Debugger.IsAttached)
            {
                readerWriter = new TestFileReaderWriter();
            }
            else
            {
                // The normal case.
                readerWriter = new ConsoleReaderWriter();
            }

            SolvePuzzle(readerWriter);

            if (Debugger.IsAttached)
            {
                Console.WriteLine("All finished!");
                Console.ReadKey();
            }
        }
        finally
        {
            if (readerWriter != null) readerWriter.Dispose();
        }
    }

    interface IReaderWriter : IDisposable
    {
        string ReadLine();
        int ReadLineToInt();
        long ReadLineToLong();
        int[] ReadLineToIntArray();
        bool[] ReadLineToBoolArray(Func<char, bool> converter);
        void WriteLine();
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteLine(object o);
    }

    abstract class ReaderWriterBase : IReaderWriter
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
            string line = ReadLine();
            return int.Parse(line.Trim());
        }

        public long ReadLineToLong()
        {
            string line = ReadLine();
            return long.Parse(line.Trim());
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
            Console.WriteLine(_currentLine);
            if (_myOutputReader != null)
            {
                CheckCurrentLine();
                _currentLine.Clear();
            }
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
            var nextLine = _myOutputReader.ReadLine().Trim();
            var currentLine = _currentLine.ToString().Trim();

            if (nextLine != currentLine) { throw new Exception("oy: line " + currentLineNumber); }
        }

        public override void Dispose()
        {
            foreach (var tr in new[] { _myReader, _myOutputReader })
            {
                if (tr != null)
                {
                    tr.Close();
                    tr.Dispose();
                }
            }
        }
    }
    #endregion
}

