namespace BalancedSearchTreesMadeSimple.Lib;

public class Node<T> where T : IComparable<T>
{
    /// <summary>
    /// The node to the left of the current node.
    /// </summary>
    internal Node<T> leftNode;

    /// <summary>
    /// The node to the right of the current node.
    /// </summary>
    internal Node<T> rightNode;

    /// <summary>
    /// The data contained in the node.
    /// </summary>
    private T _key;

    /// <summary>
    /// The level of the node. It corresponds to the vertical height of the node.
    /// Nodes on the bottom of the tree are level 1.
    /// </summary>
    private int _level;

    /// <summary>
    /// Initializes a new instance of the <see cref="Node{T}"/> class.
    /// </summary>
    /// <param name="value">The data contained in the node.</param>
    /// <param name="level">The level of the node.</param>
    /// <param name="leftNode">The node to the left of the current node.</param>
    /// <param name="rightNode">The node to the right of the current node.</param>
    public Node(T value, int level, Node<T> leftNode, Node<T> rightNode)
    {
        this.Key = value;
        this.Level = level;
        this.leftNode = leftNode;
        this.rightNode = rightNode;
    }

    /// <summary>
    /// The level of the node. It corresponds to the vertical height of the node.
    /// Nodes on the bottom of the tree are level 1.
    /// </summary>
    public int Level
    {
        get
        {
            return this._level;
        }

        set
        {
            this._level = value;
        }
    }

    /// <summary>
    /// Gets or sets the data contained in the node.
    /// </summary>
    public T Key
    {
        get
        {
            return this._key;
        }

        set
        {
            this._key = value;
        }
    }
}
