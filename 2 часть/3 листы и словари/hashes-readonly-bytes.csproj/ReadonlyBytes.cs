using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable
    {
        private int hashCode = -1;
        private byte[] collection;

        public ReadonlyBytes(params byte[] arguments)
        {
            collection = arguments ?? throw new ArgumentNullException();
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= collection.Length) throw new IndexOutOfRangeException();
                return collection[index];
            }
        }

        public int Length => collection.Length;

        public IEnumerator<byte> GetEnumerator()
        {
            return ((IEnumerable<byte>)collection).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != GetType()) return false;
            if (!(obj is ReadonlyBytes comparedBytes) || collection.Length != comparedBytes.Length)
                return false;
            if (ReferenceEquals(this, obj)) return true;
            return !collection.Where((t, i) => t != comparedBytes[i]).Any();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (hashCode != -1)
                    return hashCode;
                hashCode = 1;
                foreach (var e in collection)
                {
                    hashCode *= 654;
                    hashCode += e;
                }

                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"[{string.Join(", ", collection)}]";
        }
    }
}