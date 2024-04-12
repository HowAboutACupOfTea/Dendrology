using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("BalancedSearchTreesMadeSimple.Test")]

namespace BalancedSearchTreesMadeSimple.Lib;

public class SearchTree<T> where T : IComparable<T>
{
    internal static readonly Node<T> _bottom = new(default, 0, _bottom, _bottom);
    internal static Node<T> _deleted = _bottom;
    internal static Node<T> _last = _bottom;
    internal Node<T> _rootNode;

    public SearchTree()
    {
        this._rootNode = _bottom;
        this._rootNode.leftNode = _bottom;
        this._rootNode.rightNode = _bottom;
    }

    /// <summary>
    /// This method insert the given value into the tree.
    /// </summary>
    /// <param name="value">The value to insert.</param>
    /// <exception cref="ArgumentNullException">This exception gets thrown when the given value is null.</exception>
    public void Insert(T value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        this.Insert(value, ref this._rootNode);
    }

    /// <summary>
    /// This method insert the given value into the tree and performs the necessary skew and split operations.
    /// </summary>
    /// <param name="value">The value to insert.</param>
    /// <param name="node">The node where the value might get inserted.</param>
    private void Insert(T value, ref Node<T> node)
    {
        if (node == _bottom)
        {
            node = new Node<T>(value, 1, _bottom, _bottom);
        }
        else
        {
            if (value.CompareTo(node.Key) <= 0)
            {
                this.Insert(value, ref node.leftNode);
            }
            else if (value.CompareTo(node.Key) >= 0)
            {
                this.Insert(value, ref node.rightNode);
            }

            this.Skew(ref node);
            this.Split(ref node);
        }
    }

    /// <summary>
    /// This method deletes the given value from the tree.
    /// </summary>
    /// <param name="value">The value to delete.</param>
    /// <returns>The number of removed values.</returns>
    public int Delete(int value)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method removes all values from the tree.
    /// </summary>
    public void Clear()
    {
        this._rootNode = _bottom;
        this._rootNode.leftNode = _bottom;
        this._rootNode.rightNode = _bottom;
    }

    /// <summary>
    /// This method returns the total number of stored values.
    /// </summary>
    public int Count()
    {
        return this.TraverseInOrder().Count();
    }

    /// <summary>
    /// This method returns the number of occurrences of a specific value.
    /// </summary>
    /// <param name="value">The value to count.</param>
    /// <returns>The number of occurrences.</returns>
    public int Count(T value)
    {
        List<T> list = this.TraverseInOrder().ToList();
        // no exception but zero
        List<T> sameValues = list.FindAll(key => key.CompareTo(value) == 0) ?? throw new ArgumentNullException();
        return sameValues.Count;
    }

    /// <summary>
    /// This method finds the smallest value stored in the tree.
    /// </summary>
    /// <returns>The smallest value in the tree.</returns>
    public T Minimum()
    {
        if (_rootNode == _bottom)
        {
            throw new InvalidOperationException("The tree does not contain any values.");
        }

        var currentNode = _rootNode;
        T minimum = currentNode.Key;

        while (currentNode != _bottom)
        {
            minimum = currentNode.Key;
            currentNode = currentNode.leftNode;
        }

        return minimum;
    }

    /// <summary>
    /// This method finds the largest value stored in the tree.
    /// </summary>
    /// <returns>The largest value in the tree.</returns>
    public T Maximum()
    {
        if (_rootNode == _bottom)
        {
            throw new InvalidOperationException("The tree does not contain any values.");
        }

        var currentNode = _rootNode;
        T maximum = currentNode.Key;

        while (currentNode != _bottom)
        {
            maximum = currentNode.Key;
            currentNode = currentNode.rightNode;
        }

        return maximum;
    }

    /// <summary>
    /// This method checks for the occurrence of a specific value.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is in the tree, otherwise false.</returns>
    public bool Contains(T value)
    {
        var currentNode = _rootNode;

        while (currentNode != _bottom)
        {
            if (value.CompareTo(currentNode.Key) < 0)
            {
                currentNode = currentNode.leftNode;
            }
            else if (value.CompareTo(currentNode.Key) > 0)
            {
                currentNode = currentNode.rightNode;
            }
            else if (value.CompareTo(currentNode.Key) == 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// This method traverses the tree pre-order, in-order (default) or post-order.
    /// </summary>
    /// <param name="order">The order in which to traverse the tree.</param>
    /// <returns>A list of the data contained in the tree.</returns>
    public IEnumerable<T> Traverse(OrderEnum order)
    {
        return order switch
        {
            OrderEnum.preOrder => TraversePreOrder(),
            OrderEnum.inOrder => TraverseInOrder(),
            OrderEnum.postOrder => TraversePostOrder(),
            _ => TraverseInOrder(),
        };
    }

    /// <summary>
    /// This method traverses the tree pre-order.
    /// </summary>
    /// <returns>A list of the data contained in the tree.</returns>
    private List<T> TraversePreOrder()
    {
        throw new NotImplementedException();
    }

    /// This method traverses the tree in-order which is the default way.
    /// </summary>
    /// <returns>A list of the data contained in the tree.</returns>
    private IEnumerable<T> TraverseInOrder()
    {
        Node<T> temp;
        List<Node<T>> nodes = new();
        nodes.AddRange(this.GetLeftNodePath(this._rootNode));

        while (nodes.Any())
        {
            yield return nodes.Last().Key;

            if (nodes.Last().rightNode != _bottom)
            {
                temp = nodes.Last();
                nodes.Remove(nodes.Last());
                nodes.AddRange(this.GetLeftNodePath(temp.rightNode));
            }
            else
            {
                nodes.Remove(nodes.Last());
            }
        }
    }

    private List<Node<T>> GetLeftNodePath(Node<T> currentNode)
    {
        List<Node<T>> nodePath = new();

        if (currentNode != _bottom)
        {
            nodePath.Add(currentNode);
        }

        while (currentNode.leftNode != _bottom)
        {
            nodePath.Add(currentNode.leftNode);
            currentNode = currentNode.leftNode;
        }

        return nodePath;
    }

    /// This method traverses the tree post-order.
    /// </summary>
    /// <returns>A list of the data contained in the tree.</returns>
    private List<T> TraversePostOrder()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method rotates the given node to the right.
    /// </summary>
    /// <param name="node">The node that gets rotates.</param>
    private void Skew(ref Node<T> node)
    {
        Node<T> temp;

        if (node.leftNode.Level == node.Level)
        {
            temp = node;
            node = node.leftNode;
            temp.leftNode = node.rightNode;
            node.rightNode = temp;
        }
    }

    /// <summary>
    /// This method rotates the given node to the left.
    /// </summary>
    /// <param name="node">The node that gets rotates.</param>
    private void Split(ref Node<T> node)
    {
        Node<T> temp;

        if (node.rightNode.rightNode.Level == node.Level)
        {
            temp = node;
            node = node.rightNode;
            temp.rightNode = node.leftNode;
            node.leftNode = temp;
            node.Level++;
        }
    }
}
