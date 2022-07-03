public class Page
{
    #region init
    public int Capacity { get; private set; }
    public int Size { get; private set; }

    public Page? Parent { get; private set; }
    public Node Head { get; private set; }
    public Node? Half { get; private set; }    
    public Page? RightPage { get; private set; }
    private static int pages = 0;
    // odd capacity to have even after adding a node without 
    public Page(Page? parent = null, Node? first = null, Page? rightDownPage = null, int capacity = 31)
    {
        Console.WriteLine($"page {++pages}");
        Head = Node.GetHead(rightDownPage);
        Head.Next = first;
        Parent = parent;        
        Capacity = capacity;
    }

    public Page(Node? first, Page? rightDownPage = null, int size = 0, int capacity = 31)
    {
        Console.WriteLine($"page {++pages}");
        Head = Node.GetHead(rightDownPage);
        Head.Next = first;        
        Size = size;
        Capacity = capacity;        
    }
    #endregion

    public Node this[int index]
    {
        get 
        {
            if(index >= Size)
                throw new NotSupportedException("node doesn't exist");

            var i = -1;
            var currentNode = Head;
            while(i++ < index) {
                currentNode = currentNode.Next;              
            }
            
            if(currentNode is null)
                throw new NotSupportedException("node is null");


            return currentNode;
        }
    }
    private Page FindPage(Node node)
    {
        var currentNode = Head;
        while(currentNode.Next != null && currentNode.Next.CompareTo(node) <= 0)
            currentNode = currentNode.Next;

        if (currentNode.RightDownPage is not null)
            return currentNode.RightDownPage.FindPage(node);

        return this;
    }

    public Page Push(Node node)
    {
        var page = FindPage(node);
        // Console.WriteLine($"found page {page.Size}");
        page.Insert(node);
        return GetRoot(this);
    }
    
    private Page GetRoot(Page root)
    {
        while(root.Parent is not null)
            return GetRoot(root.Parent);

        return root;        
    }
    
    private void Insert(Node node)
    {        
        if (Size == 0) {   
            Head.Next = node;
            Size++;
            return; 
        }

        var size = Size;        
        var currentNode = Head;        
        while(currentNode.Next is not null) {
            if (currentNode.Next.CompareTo(node) < 0) {                
                currentNode = currentNode.Next;
            }
            else {                
                node.Next = currentNode.Next;
                currentNode.Next = node;                
                Size++;
                break;
            }                        
        }
        
        // the last element
        if (size == Size) {
            currentNode.Next = node;
            Size++;
        }

        // traverse();
        
        if (Size == Capacity + 1) {
            // Console.WriteLine($"max size is reached {Size}");
            // traverse();
            // Console.ReadLine();
            Half = Center();
            IncreaseDepth();
            return;
        }
    }    

    public void traverse()
    {
        var current = Head;
        while(current != null) {
            Console.Write(current.Key1 + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    // TODO: refactor
    private Node Center() 
    {
        if (Size != Capacity + 1)
            throw new InvalidOperationException("mnethod Center is invoked while collection is not full");
        
        int index = 1;
        Node node = Head.Next;
        while(index++ < Size / 2)
            node = node.Next;
        return node;
    }

    private void IncreaseDepth()
    {
        if (Size < Capacity + 1)
            throw new NotSupportedException("there are rooms for emplacing");
      
        // Console.WriteLine("increasing...");

        this.Parent = this.Parent ?? new Page(rightDownPage: this);
        var copyForSiblingNode = Half!.Next.Copy(); // should keep all references
        var siblingPage = new Page(this.Parent, copyForSiblingNode);
        
        siblingPage.RightPage = this.RightPage;
        this.RightPage = siblingPage;

        // update parent if splitting in more than two levels
        if (copyForSiblingNode.RightDownPage is not null)
        {
            var depPage = copyForSiblingNode.RightDownPage;
            while(depPage is not null) {
                depPage.Parent = siblingPage;
                depPage = depPage.RightPage;
            }
        }

        var copyInNewLevelNode = Half!.Next.Copy();
        copyInNewLevelNode.Next = null; // level shouldn't have references to the sibling node
        copyInNewLevelNode.RightDownPage = this.RightPage; // updated right down link
        Half!.Next = null; // reset links for the left page

        this.Size = RightPage.Size =  this.Size / 2; // updated sizes of pages
        // Console.ReadLine();
        this.Parent.Insert(copyInNewLevelNode);
    }
}
