// Tree grows bottom to up
public class TreeBuilder
{
    public Page Root { get; private set; }
    public TreeBuilder(int capacity = 63)
    {
        Root = new Page(capacity: capacity);
    }

    public void AddNode(Node node)
    {
        Root = Root.Push(node);
    }

    public IEnumerable<string> GetInOrder()
    {
        var page = Root;
        while(page.Head.RightDownPage != null)
            page = page.Head.RightDownPage;
        
        while(page != null)
        {
            var node = page.Head.Next;
            while (node != null)
            {
                yield return $"{node.Key1}-{node.Key2}";
                node = node.Next;
            }
            page = page.RightPage;
        }
    }
}
