using System.Collections.Generic;
using System.Linq;

namespace GameSolver.DataStructures
{
    public class InOutCollection<T> where T : class
    {
        private readonly IEnumerable<T> _list = new LinkedList<T>();

        private readonly bool _isStack;
        
        public InOutCollection(bool isStack)
        {
            _isStack = isStack;
        }

        public void Add(T value)
        {
            if (_isStack)
            {
                (_list as Stack<T>)?.Push(value);
            }
            else
            {
                (_list as Queue<T>)?.Enqueue(value);
            }
        }
        
        public T Remove()
        {
            return _isStack ? (_list as Stack<T>)?.Pop() : (_list as Queue<T>)?.Dequeue();
        }

        public int Length => _list.Count();

        public bool Empty() => !_list.Any();
    }
}