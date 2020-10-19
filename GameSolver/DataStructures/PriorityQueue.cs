using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Priority_Queue;

namespace GameSolver.DataStructures
{
    public class PriorityQueue<T> : InOutCollection<T> where T : class
    {
        private readonly SimplePriorityQueue<T> _queue = new SimplePriorityQueue<T>();
        private readonly Func<T, double> _fn;
        
        public PriorityQueue(Func<T, double> fn) : base(false)
        {
            _fn = fn;
        }

        public override void Add(T value)
        {
            _queue.Enqueue(value, Convert.ToSingle(_fn.Invoke(value)));
        }

        public override T Remove()
        {
            return _queue.Dequeue();
        }

        public override int Length => _queue.Count;
        public override bool Empty()
        {
            return !_queue.Any();
        }

        public override void Clear()
        {
            _queue.Clear();
        }

        public override T Peek()
        {
            return _queue.First;
        }
    }
}