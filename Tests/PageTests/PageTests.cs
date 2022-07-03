using System.Reflection;
namespace PageTests;


public class Pages
{
    [Test]
    public void Test1()
    {        
        var builder = new TreeBuilder();

        var node1 = new Node("b", 1, 0, 0);
        var node2 = new Node("h", 2, 0, 0);
        var node3 = new Node("c", 3, 0, 0);
        var node4 = new Node( "a", 4, 0, 0);
        var node5 = new Node("d", 5, 0, 0);
        var node6 = new Node("g", 5, 0, 0);
        var node7 = new Node("f", 5, 0, 0);
        var node8 = new Node("n", 5, 0, 0);
        var node9 = new Node("e", 5, 0, 0);
        var node10 = new Node("m", 5, 0, 0);
        var node11 = new Node("z", 5, 0, 0);       
        builder.AddNode(node1);
        builder.AddNode(node2);
        Assert.AreEqual(2, builder.Root.Size);        
        Assert.AreEqual(null, builder.Root[0].RightDownPage);
        Assert.AreEqual("b", builder.Root[0].Key1);        
        Assert.AreEqual("h", builder.Root[1].Key1);

        builder.AddNode(node3); // c
        
        Assert.AreEqual(3, builder.Root.Size);        
        Assert.AreEqual(null, builder.Root[0].RightDownPage);
        Assert.AreEqual("b", builder.Root[0].Key1);
        Assert.AreEqual("c", builder.Root[1].Key1);
        Assert.AreEqual("h", builder.Root[2].Key1);

        builder.AddNode(node4); // a
        Assert.AreEqual(1, builder.Root.Size);
        Assert.AreEqual("c", builder.Root[0].Key1);        
        Assert.AreEqual(2, builder.Root[0].RightDownPage!.Size);
        Assert.AreEqual("c", builder.Root[0].RightDownPage![0].Key1);
        Assert.AreEqual("h", builder.Root[0].RightDownPage![1].Key1);
        Assert.AreEqual("a", builder.Root.Head.RightDownPage![0].Key1);
        Assert.AreEqual("b", builder.Root.Head.RightDownPage![1].Key1);

        builder.AddNode(node5); // d
        Assert.AreEqual(1, builder.Root.Size);        
        Assert.AreEqual(3, builder.Root[0].RightDownPage!.Size);
        Assert.AreEqual("c", builder.Root[0].RightDownPage![0].Key1);
        Assert.AreEqual("d", builder.Root[0].RightDownPage![1].Key1);
        Assert.AreEqual("h", builder.Root[0].RightDownPage![2].Key1);
        Assert.AreNotEqual(null, builder.Root[0].RightDownPage!.Parent);
        Assert.AreEqual(1, builder.Root[0].RightDownPage!.Parent!.Size);

        builder.AddNode(node6); // g
        Assert.AreEqual(2, builder.Root.Size);      
        Assert.AreEqual(2, builder.Root[0].RightDownPage!.Size);        
        Assert.AreEqual(2, builder.Root[1].RightDownPage!.Size);
        Assert.AreEqual("g", builder.Root[1].Key1);
        Assert.AreEqual("g", builder.Root[1].RightDownPage![0].Key1);
        Assert.AreEqual("h", builder.Root[1].RightDownPage![1].Key1);
        Assert.AreNotEqual(null, builder.Root[1].RightDownPage);

        builder.AddNode(node7); // f
        Assert.AreEqual(2, builder.Root.Size);        
        Assert.AreEqual(2, builder.Root[1].RightDownPage!.Size);
        Assert.AreEqual(3, builder.Root[0].RightDownPage!.Size); 
        Assert.AreNotEqual(null, builder.Root[0].RightDownPage!.RightPage);

        builder.AddNode(node8); // n
        Assert.AreEqual(2, builder.Root.Size);
        Assert.AreEqual("c", builder.Root[0].Key1);
        Assert.AreEqual("g", builder.Root[1].Key1); 
        Assert.AreEqual(3, builder.Root[0].RightDownPage!.Size);
        Assert.AreEqual(3, builder.Root[1].RightDownPage!.Size);
        Assert.AreNotEqual(null, builder.Root[0].RightDownPage!.RightPage);

        builder.AddNode(node9); // e
        Assert.AreEqual(3, builder.Root.Size);      
        Assert.AreEqual("c", builder.Root[0].Key1);
        Assert.AreEqual("e", builder.Root[1].Key1);
        Assert.AreEqual("g", builder.Root[2].Key1);
        Assert.AreEqual(2, builder.Root[1].RightDownPage!.Size);
        Assert.AreEqual(3, builder.Root[2].RightDownPage!.Size);        
        Assert.AreNotEqual(null, builder.Root[1].RightDownPage!.RightPage);

        builder.AddNode(node10); // m
        Assert.AreEqual(1, builder.Root.Size);
        Assert.AreEqual("g", builder.Root[0].Key1);
        Assert.AreEqual(2, builder.Root[0].RightDownPage!.Size);
        Assert.AreEqual("g", builder.Root[0].RightDownPage![0].Key1);        
        Assert.AreEqual("m", builder.Root[0].RightDownPage![1].Key1);        
        Assert.AreEqual(2, builder.Root.Head.RightDownPage!.Size);
        Assert.AreEqual("c", builder.Root.Head.RightDownPage![0].Key1);
        Assert.AreEqual("e", builder.Root.Head.RightDownPage![1].Key1);
        Assert.AreNotEqual(null, builder.Root[0].RightDownPage![1].RightDownPage);
        Assert.AreNotEqual(null, builder.Root[0].RightDownPage![0].RightDownPage);        

        builder.AddNode(node7.Copy());
        builder.AddNode(node10.Copy());
        builder.AddNode(node11.Copy());        
    }
}