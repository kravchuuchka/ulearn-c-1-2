using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace rocket_bot
{
    public class Channel<T> where T : class
    {
        private readonly List<T> channel;

        public Channel()
        {
            channel = new List<T>();
        }

        public T this[int index]
        {
            get
            {
                lock (channel)
                {
                    if (index < 0 || index >= channel.Count)
                        return null;
                    return channel[index];
                }
            }
            set
            {
                lock (channel)
                {
                    if (index == channel.Count)
                        channel.Add(value);
                    else
                    {
                        channel[index] = value;
                        for (var i = channel.Count - 1; i > index; i--)
                            channel.RemoveAt(i);
                    }
                }
            }
        }

        public T LastItem()
        {
            lock (channel)
            {
                if (channel.Count == 0) return null;
                return channel[channel.Count - 1];
            }
        }

        public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
        {
            lock (channel)
            {
                if (channel.Count == 0)
                    channel.Add(item);
                else if (channel[channel.Count - 1].Equals(knownLastItem))
                    channel.Add(item);
            }
        }

        public int Count
        {
            get
            {
                lock (channel)
                {
                    return channel.Count;
                }
            }
        }
    }
}