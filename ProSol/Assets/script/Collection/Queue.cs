using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<Template>
{
    public Template data;
    public Node<Template> next;

    public Node(Template newData)
    {
        data = newData;
        next = null;
    }
}

public class Queue<Template>
{
    private Node<Template> head;
    private Node<Template> tail;
    private int count;

    public Queue()
    {
        head = null;
        tail = null;
        count = 0;
    }

    public void Enqueue(Template data)
    {
        Node<Template> newNode = new Node<Template>(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.next = newNode;
            tail = newNode;
        }
        count++;
    }

    public Template Dequeue()
    {
        if (head == null)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        Template data = head.data;
        head = head.next;
        if (head == null)
        {
            tail = null;
        }
        return data;
    }

    public int Count()
    {
        return count;
    }
}