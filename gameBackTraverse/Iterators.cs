using System;

namespace gameBackTraverse
{
    public interface NamesIterator
    {
        public static NamesIterator? Instance { get; }

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
        private static NaturalNumberIterator? _instance;
        private int number = 0;

        public static NamesIterator Instance
        {
            get => _instance == null ? _instance = new NaturalNumberIterator() : _instance;
        }

        public string First()
        {
            number = 1;
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

        private NaturalNumberIterator()
        {

        }
    }
}
