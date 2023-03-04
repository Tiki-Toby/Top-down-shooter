using System;
using System.Collections;
using System.Collections.Generic;

namespace Tools.PriorityTools
{
    public class PrioritizeLinkedList<T> : IEnumerable, IDisposable where T : IPrioritizatedModule
    {
        private readonly LinkedList<T> _valueList;

        public PrioritizeLinkedList()
        {
            _valueList = new LinkedList<T>();
        }

        public void Add(T value)
        {
            for (var valueNode = _valueList.First; valueNode != null; valueNode = valueNode.Next)
            {
                if (valueNode.Value.Priority == value.Priority || 
                    valueNode.Value.Priority > value.Priority)
                {
                    _valueList.AddBefore(valueNode, new LinkedListNode<T>(value));
                    return;
                }
            }
        }

        public void Remove(T value)
        {
            _valueList.Remove(value);
        }

        public IEnumerator GetEnumerator()
        {
            return _valueList.GetEnumerator();
        }

        public void Dispose()
        {
            _valueList.Clear();
        }
    }
}