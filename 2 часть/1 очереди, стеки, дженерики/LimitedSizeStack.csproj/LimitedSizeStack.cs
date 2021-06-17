using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private int amount;
        private LinkedList<T> newList = new LinkedList<T>();

        public LimitedSizeStack(int limit)
        {
            amount = limit;
        }

        public void Push(T item)
        {
            newList.AddLast(item);
            if (newList.Count > amount)
                newList.RemoveFirst();
        }

        public T Pop()
        {
            if (newList.Count == 0)
                throw new InvalidOperationException();
            var last = newList.Last.Value;
            newList.RemoveLast();
            return last;
        }

        public int Count
        {
            get
            {
                return newList.Count;
            }
        }
    }
}
