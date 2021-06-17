using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTrees
{
    public class BinaryTree<T> where T : IComparable
    {
        public T Value;
        public BinaryTree<T> LeftTree;
        public BinaryTree<T> RightTree;
        public int Count;

        public BinaryTree()
        {

        }

        public BinaryTree(T element)
        {
            Value = element;
        }

        public void Add(T element)
        {
            if (Count == 0)
            {
                Value = element;
                Count += 1;
            }
            else
            {
                var node = this;
                var source = node;
                Count += 1;
                while (node != null)
                {
                    source = node;
                    node = node.Value.CompareTo(element) < 0 ? node.RightTree : node.LeftTree;
                    source.Count += 1;
                }
                if (source.Value.CompareTo(element) < 0)
                {
                    source.RightTree = new BinaryTree<T>(element);
                    source.RightTree.Count += 1;
                }
                else
                {
                    source.LeftTree = new BinaryTree<T>(element);
                    source.LeftTree.Count += 1;
                }
            }
        }

        public bool Contains(T element)
        {
            if (Count == 0) return false;
            var node = this;
            while (node != null)
            {
                if (node.Value.CompareTo(element) == 0) return true;
                node = node.Value.CompareTo(element) < 0 ? node.RightTree : node.LeftTree;
            }

            return false;
        }
    }
}
