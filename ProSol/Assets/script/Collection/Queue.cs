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

    public Queue()
    {
        head = null;
        tail = null;
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
        int count = 0;
        Node<Template> current = head;
        while (current != null)
        {
            count++;
            current = current.next;
        }
        return count;
    }
}