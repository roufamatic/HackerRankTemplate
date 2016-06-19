using System;
using System.Diagnostics;
using System.IO;

class Solution
{
    interface IInputReader
    {
        string ReadLine();
        int ReadLineToInt();
        int[] ReadLineToIntArray();
        bool[] ReadLineToBoolArray(Func<char, bool> converter);
    }

    abstract class ReaderBase : IInputReader, IDisposable
    {
        public abstract string ReadLine();

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

        public int[] ReadLineToIntArray()
        {
            return Array.ConvertAll(ReadLine().Trim().Split(' '), int.Parse);
        }

        public bool[] ReadLineToBoolArray(Func<char, bool> converter)
        {
            return Array.ConvertAll(ReadLine().ToCharArray(), converter.Invoke);
        }
    }

    class ConsoleReader : ReaderBase
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

    }

    class TestFileReader : ReaderBase
    {
        private TextReader _myReader = File.OpenText("input.txt");

        public override string ReadLine()
        {
            return _myReader.ReadLine();
        }

        public override void Dispose()
        {
            _myReader.Close();
            _myReader.Dispose();
            _myReader = null;
        }
    }

    private static IInputReader _reader;

    static void Main(String[] args)
    {
        if (Debugger.IsAttached)
        {
            _reader = new TestFileReader();
        }
        else
        {
            _reader = new ConsoleReader();
        }

        //
        // your code here
        //


        if (Debugger.IsAttached)
        {
            Console.ReadKey(); 
        }
    }
}
