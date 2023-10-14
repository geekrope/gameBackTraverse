using System;

namespace gameBackTraverse
{
    public interface NamesIterator
    {
        public string First();
        public string Next();
        public bool IsDone();
    }
    public class AlphabetIterator : NamesIterator
    {
        private readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static AlphabetIterator? _instance;
        private int index = 0;

        public static NamesIterator Instance
        {
            get => _instance == null ? _instance = new AlphabetIterator() : _instance;
        }

        public string First()
        {
            index = 0;
            return _alphabet[index].ToString();
        }
        public string Next()
        {
            index++;
            return _alphabet[index].ToString();
        }
        public bool IsDone()
        {
            return index < _alphabet.Length;
        }

        private AlphabetIterator()
        {

        }
    }
    public class NaturalNumberIterator : NamesIterator
    {
        private int number = 0;
        private int start = 0;

        public string First()
        {
            number = start;
            return number.ToString();
        }
        public string Next()
        {
            number++;
            return number.ToString();
        }
        public bool IsDone()
        {
            return false;
        }

        public NaturalNumberIterator(int start)
        {
            this.start = start;
            this.number = start;
        }
    }
}
