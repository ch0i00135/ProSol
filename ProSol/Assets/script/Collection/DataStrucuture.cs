using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataStrucuture
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T>
    {
        public LinkedListNode<T> head;

        public LinkedList()
        {
            head = null;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                LinkedListNode<T> current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }
    }

    public class Queue<T>
    {
        private LinkedList<T> list;

        public Queue()
        {
            list = new LinkedList<T>();
        }

        // 큐에 요소를 추가합니다.
        public void Enqueue(T data)
        {
            list.Add(data);
        }

        // 큐에서 요소를 제거하고 반환합니다.
        public T Dequeue()
        {
            if (list.head == null)
            {
                throw new InvalidOperationException("Queue is empty.");
            }

            T data = list.head.Data;
            list.head = list.head.Next;
            return data;
        }

    }

    public class Stack<T>
    {
        public Queue<T> queue;
        public int count;

        public Stack() 
        {
            queue=new Queue<T> ();
            count=0;
        }
        public void Push(T data) 
        {
            queue.Enqueue(data);
            count++;
            Debug.Log(count);
        }
        public T Pop()
        {
            if (count == 1)
            {
                count--;
                return queue.Dequeue();
            }
            else
            {
                Queue<T> empty = new Queue<T>();
                for (int i = 1; i < count; i++)
                {
                    empty.Enqueue(queue.Dequeue());
                }
                T data = queue.Dequeue();
                queue = empty;
                count--;
                Debug.Log(count);
                return data;
            }
        }    
    }
}
