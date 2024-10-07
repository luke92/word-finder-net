using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace WordFinderApp.Logic
{
    public class WordFinder : IWordFinder
    {
        private const int MAX_COLUMNS_ALLOWED = 64;
        private const int MAX_ROWS_ALLOWED = 64;
        private int _columns = 0;
        private List<string> _matrixRows;
        private List<StringBuilder> _matrixColumns;
        private bool wasMatrixIterated;
        private IEnumerable<string> _matrix;
        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            _matrixRows = new List<string>();

            if (matrix == null)
            {
                throw new ArgumentNullException("Matrix cannot be null");
            }

            foreach (var row in matrix)
            {
                if (row == null)
                {
                    throw new ArgumentNullException("Row cannot be null");
                }

                if (_columns == 0)
                {
                    _columns = row.Length;
                    break;
                }
            }

            if (_columns == 0)
            {
                throw new ArgumentException("First word cannot be empty");
            }

            if (_columns > MAX_COLUMNS_ALLOWED)
            {
                throw new ArgumentException($"The matrix cannot have more than {MAX_COLUMNS_ALLOWED} columns");
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordMap = new Dictionary<string, int>();
            var wordSetStream = new HashSet<string>(wordstream?.Where(
                word => !string.IsNullOrWhiteSpace(word)).Select(word => word.ToLowerInvariant()) ??
                Enumerable.Empty<string>()
            );

            if (!wasMatrixIterated)
            {
                IterateFirstTimeRows(wordMap, wordSetStream);
                wasMatrixIterated = true;
            }
            else
            {
                foreach (var rowInMatrix in _matrixRows)
                {
                    SearchMatches(wordMap, rowInMatrix, wordSetStream);
                }
            }

            foreach (var columnInMatrix in _matrixColumns)
            {
                SearchMatches(wordMap, columnInMatrix.ToString(), wordSetStream);
            }

            return this.GetMoreRepeatedWords(wordMap, 10);
        }

        private void IterateFirstTimeRows(Dictionary<string, int>? wordMap, HashSet<string>? wordSetStream)
        {
            InitializeColumns();

            foreach (var rowInMatrix in _matrix)
            {
                if (_matrixRows.Count == MAX_ROWS_ALLOWED)
                {
                    throw new InvalidOperationException($"The matrix cannot have more than {MAX_ROWS_ALLOWED} rows");
                }

                var newRow = rowInMatrix.ToLowerInvariant();
                _matrixRows.Add(newRow);

                AddNewCharactersInColumns(newRow);

                SearchMatches(wordMap, rowInMatrix, wordSetStream);
            }
        }

        private void InitializeColumns()
        {
            _matrixColumns = new List<StringBuilder>();
            for (var column = 0; column < _columns; column++)
            {
                _matrixColumns.Add(new StringBuilder());
            }
        }

        private void AddNewCharactersInColumns(string newRow)
        {
            for (var column = 0; column < _columns; column++)
            {
                _matrixColumns[column].Append(newRow[column]);
            }
        }

        private void SearchMatches(Dictionary<string, int> wordMap, string fullWordInMatrix, HashSet<string> wordSetStream)
        {
            foreach (var wordInStream in wordSetStream)
            {
                if (fullWordInMatrix.Length >= wordInStream.Length &&
                    fullWordInMatrix.Contains(wordInStream)
                )
                {
                    IncrementCount(wordMap, wordInStream);
                }
            }
        }

        private void IncrementCount(Dictionary<string, int> wordMap, string word)
        {
            if (wordMap.ContainsKey(word))
            {
                wordMap[word]++;
                return;
            }
            wordMap[word] = 1;
        }

        private IEnumerable<string> GetMoreRepeatedWords(Dictionary<string, int> wordMap, int quantity)
        {
            return wordMap
                .OrderByDescending(word => word.Value)
                .Take(quantity)
                .Select(word => word.Key);
        }
    }
}
