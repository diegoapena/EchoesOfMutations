using System;
using UnityEngine;

public class DoubleLinkedList<T> 
{
    private Node<T> head;
    private Node<T> tail;
    private int count;

    public Node<T> AddLast(T value)
    {
        Node<T> newNode = new (value);
        
        if(head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.SetPrev(tail);
            tail.SetPrev(newNode);
            tail = newNode;
        }
        count++;
        return newNode;
    }

    public void Remove(Node<T> node)
    {
        if (node == null) return;
        if (node.Prev != null)
        {
            node.Prev.SetNext(node.Next);
        }
        else
        {
            head = node.Next;
        }

        if (node.Prev != null)
        {
            node.Next.SetPrev(node.Prev);
        }
        else 
        {
            tail = node.Prev;
        } 

        node.SetNext(null);
        node.SetPrev(null);
        count--;
    }
    public Node<T> Find(Predicate<T> match)
    {
        Node<T> current = head;

        while (current != null)
        {
            if (match(current.Value)) return current;
            current = current.Next;

        }
        return null;
    }
    public T[] ToArray()
    {
        T[] result = new T[count];
        Node<T> current = head;

        for(int i = 0; i < count; i++)
        {
            result[i] = current.Value;
            current = current.Next;
        }
        return result;
    }

    public Node<T> Head => head;
    public Node<T> Tail => tail;
    public int Count => count;
}
