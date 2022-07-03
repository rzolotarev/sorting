// Tree grows bottom to up
public class TreeBuilder
{
    public Page Root { get; private set; }
    public TreeBuilder(int capacity = 31)
    {
        Root = new Page(capacity: capacity);
    }

    public void AddNode(Node node)
    {
        // Console.WriteLine($"adding {node.Key1}");
        Root = Root.Push(node);
    }

    public IEnumerable<string> GetInOrder()
    {
        var page = Root;
        var level = 1;
        Console.WriteLine($"deep {level}");
        page.traverse();
        while(page.Head.RightDownPage != null) {
            page = page.Head.RightDownPage;                        
            level++;    
            Console.WriteLine($"deep {level}");
            // var page1 = page;
            // do {
            // page1.traverse();        
            // } while((page1 = page1.RightPage) != null);
        }
        
        while(page != null)
        {            
            var node = page.Head.Next;
            while (node != null)
            {
                yield return $"{node.Key1}-{node.Key2}";
                node = node.Next;
            }
            // Console.WriteLine();
            page = page.RightPage;
        }
        Console.WriteLine("----------------");
    }
}
