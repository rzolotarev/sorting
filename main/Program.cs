using System.Collections.Concurrent;
// See https://aka.ms/new-console-template for more information

var builder = new TreeBuilder();
var parser = new Parser("input.txt");

// using(var file = File.Create("output.txt"))
// {
//     using(var fs = new StreamWriter(file))
//     {
//         fs.WriteLine(DateTime.Now);
    
        
 
//     }

// }
var blCollection = new BlockingCollection<Node>();

var thr = new Thread(() => {
{
    foreach(var node in parser.Read()) {
        blCollection.Add(node);    
        Console.WriteLine(blCollection.Count);
    }

    blCollection.CompleteAdding();
}
});

thr.Start();

Node node;
while((node = blCollection.Take()) != null)
{
    builder.AddNode(node);
    // Console.WriteLine("processed");
}

blCollection.Dispose();
 using(var file = File.Create("output.txt"))
{
    using(var fs = new StreamWriter(file))
    {
        foreach(var item in builder.GetInOrder())       
            fs.WriteLine(item);
        
        fs.WriteLine(DateTime.Now);
    }
}