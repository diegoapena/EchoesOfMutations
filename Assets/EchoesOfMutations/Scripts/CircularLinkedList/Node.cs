using UnityEngine;

public class Node<T>
{
    #region Properties
    private T value = default ;
    private Node<T> next;
    private Node<T> prev;
    #endregion
    #region Methods
    public Node(T value)
    {
        this.value = value;
    }
    public void SetNext(Node<T> next)
    {
        this.next = next;
    }
    public void SetPrev(Node<T> prev)
    {
        this.prev = prev;
    }
    #endregion
    #region Getter
    public T Value => value;
    public Node<T> Next => next;
    public Node<T> Prev => prev;
    #endregion
}
