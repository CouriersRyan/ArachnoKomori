using System.Collections.Generic;

/**
 * A Generic heap class. Unlike Java's priority queue, this heap doesn't just
 * store Comparable objects. Instead, it can store any type of object
 * (represented by type T) and an associated priority value.
 * 
 * @author Kyle Zhang!
 */
public class MinPriorityQueue<T>
{
    /**
     * An List that stores the nodes in this binary heap.
     */
    private readonly IList<Node> _contents;

    private readonly IDictionary<T, int> _itemToIndex;

    /**
     * A constructor that initializes an empty ArrayHeap.
     */
    public MinPriorityQueue()
    {
        _contents = new List<Node> {null};
        _itemToIndex = new Dictionary<T, int>();
    }

    /**
     * Returns the number of elements in the priority queue.
     */
    public int Size()
    {
        return _contents.Count - 1;
    }

    /**
     * Returns the node at index INDEX.
     */
    private Node GetNode(int index)
    {
        if (index >= _contents.Count) return null;

        return _contents[index];
    }

    /**
     * Sets the node at INDEX to N
     */
    private void SetNode(int index, Node n)
    {
        // In the case that the List is not big enough
        // add null elements until it is the right size
        while (index + 1 > _contents.Count) _contents.Add(null);
        _contents[index] = n;
        _itemToIndex[n.Item()] = index;
    }

    /**
     * Returns and removes the node located at INDEX.
     */
    private Node RemoveNode(int index)
    {
        if (index >= _contents.Count)
            return null;
        var output = _contents[index];
        _contents.RemoveAt(index);
        _itemToIndex.Remove(output.Item());
        return output;
    }

    /**
     * Swap the nodes at the two indices.
     */
    private void Swap(int index1, int index2)
    {
        var node1 = GetNode(index1);
        var node2 = GetNode(index2);
        SetNode(index1, node2);
        SetNode(index2, node1);
    }

    /**
     * Returns the index of the left child of the node at i.
     */
    private static int GetLeftOf(int i)
    {
        return i * 2;
    }

    /**
     * Returns the index of the right child of the node at i.
     */
    private static int GetRightOf(int i)
    {
        return 2 * i + 1;
    }

    /**
     * Returns the index of the node that is the parent of the
     * node at i.
     */
    private static int GetParentOf(int i)
    {
        return i / 2;
    }

    /**
     * Returns the item with the smallest priority value, but does
     * not remove it from the heap. If multiple items have the minimum
     * priority value, returns any of them. Returns default if heap is
     * empty.
     */
    public T Peek()
    {
        var rootNode = GetNode(1);
        if (rootNode == null) return default;
        return rootNode.Item();
    }

    /**
     * Bubbles up the node currently at the given index until no longer
     * needed.
     */
    private void BubbleUp(int index)
    {
        var parentIndex = GetParentOf(index);
        while (GetNode(parentIndex) != null
               && GetNode(index).Priority() < GetNode(parentIndex).Priority())
        {
            Swap(index, parentIndex);
            index = parentIndex;
            parentIndex = GetParentOf(index);
        }
    }

    /**
     * Bubbles down the node currently at the given index until no longer
     * needed.
     */
    private void BubbleDown(int index)
    {
        var childIndex = FindMinChild(index);
        while (GetNode(childIndex) != null)
        {
            Swap(index, childIndex);
            index = childIndex;
            childIndex = FindMinChild(index);
        }
    }

    private int FindMinChild(int index)
    {
        Node node = GetNode(index),
            left = GetNode(GetLeftOf(index)),
            right = GetNode(GetRightOf(index));
        bool rightWorks = true, leftWorks = true;
        if (right == null || right.Priority() > node.Priority()) rightWorks = false;
        if (left == null || left.Priority() > node.Priority()) leftWorks = false;

        if (leftWorks && rightWorks)
            return left.Priority() < right.Priority()
                ? GetLeftOf(index)
                : GetRightOf(index);
        if (leftWorks) return GetLeftOf(index);
        if (rightWorks) return GetRightOf(index);
        return 0;
    }

    /**
     * Inserts an item with the given priority value. Assume that item is
     * not already in the heap. Same as enqueue, or offer.
     */
    public void Insert(T item, double priority)
    {
        if (Contains(item))
        {
            ChangePriority(item, priority);
            return;
        }
        SetNode(Size() + 1, new Node(item, priority));
        BubbleUp(Size());
    }

    /**
     * Returns the element with the smallest priority value, and removes
     * it from the heap. If multiple items have the minimum priority value,
     * removes any of them. Returns default if the heap is empty. Same as
     * dequeue, or poll.
     */
    public T RemoveMin()
    {
        if (Size() == 0) return default;
        Swap(1, Size());
        var output = GetNode(Size()).Item();
        RemoveNode(Size());
        BubbleDown(1);
        return output;
    }

    public bool Contains(T item)
    {
        return _itemToIndex.ContainsKey(item);
    }

    /**
     * Changes the node in this heap with the given item to have the given
     * priority. You can assume the heap will not have two nodes with the
     * same item. Does nothing if the item is not in the heap. Check for
     * item equality with .equals(), not ==
     */
    public void ChangePriority(T item, double priority)
    {
        var index = _itemToIndex[item];
        SetNode(index, new Node(item, priority));
        BubbleDown(index);
        BubbleUp(index);
    }

    /**
     * A Node class that stores items and their associated priorities.
     */
    private class Node
    {
        private readonly T _item;
        private readonly double _priority;

        public Node(T item, double priority)
        {
            _item = item;
            _priority = priority;
        }

        public T Item()
        {
            return _item;
        }

        public double Priority()
        {
            return _priority;
        }
    }
}