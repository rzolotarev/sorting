public class Node : IComparable<Node>
{
    public Node(string key1, int key2, long position, int length)
    {
        Key1 = key1;
        Key2 = key2;
        Position = position;
        Length = length;
    }

    private Node() {}

    public static Node GetHead(Page rightDownPage)
    {
        return new Node() { Key2 = Int32.MinValue, RightDownPage =  rightDownPage};
    }

    public Node Copy()
    {
        var copy = new Node(Key1, Key2, Position, Length);
        copy.Next = Next;
        copy.RightDownPage = RightDownPage;
        return copy;
    }

    public int Key2 { get; private set; }
    public string Key1 { get; private set; }
    public long Position { get; private set; }
    public int Length { get; private set; }
    public Node Next {get;set;}
    public Page? RightDownPage { get; set; }
    public int CompareTo(Node? other)
    {
        if (other is null)
            throw new NullReferenceException("compare with null");

        if (string.IsNullOrWhiteSpace(Key1) && Key2 == Int32.MinValue)
            return -1;


        var firstComparison = Key1.CompareTo(other.Key1);
        var secondComparison = Key2.CompareTo(other.Key2);
        return firstComparison == 0 ? secondComparison: firstComparison;
    }
}