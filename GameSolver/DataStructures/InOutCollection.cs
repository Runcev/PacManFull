using System.Collections.Generic;
using System.Linq;

namespace GameSolver.DataStructures
{
    public class InOutCollection<T> where T : class
    {
        private readonly LinkedList<T> _list = new LinkedList<T>();

        private readonly bool _isStack;

        public InOutCollection(bool isStack)
        {
            _isStack = isStack;
        }

        public virtual void Add(T value)
        {
            if (_isStack)
            {
                _list.AddFirst(value);
            }
            else
            {
                _list.AddLast(value);
            }
        }

        public virtual T Remove()
        {
            var last = _list.First();
            _list.RemoveFirst();
            return last;
        }

        public virtual int Length => _list.Count();

        public virtual bool Empty() => !_list.Any();

        public override string ToString()
        {
            return string.Join(", ", _list);
        }

        public virtual void Clear()
        {
            _list.Clear();
        }

        public virtual T Peek()
        {
            return _isStack ? _list.First() : _list.Last();
        }
    }
}