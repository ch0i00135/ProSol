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

        // ť�� ��Ҹ� �߰��մϴ�.
        public void Enqueue(T data)
        {
            list.Add(data);
        }

        // ť���� ��Ҹ� �����ϰ� ��ȯ�մϴ�.
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

        public Stack() 
        {
            queue=new Queue<T> ();
        }
        public void Push(T data) 
        {
            queue.Enqueue(data);
        }
        public T Pop()
        {
            Queue<T> empty=new Queue<T>();
            empty.Enqueue(queue.Dequeue());
        }    
    }
}
