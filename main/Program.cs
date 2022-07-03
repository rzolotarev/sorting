using System.Collections.Concurrent;
using System.Diagnostics;
// See https://aka.ms/new-console-template for more information

var builder = new TreeBuilder();
var parser = new Parser("input.txt");

var stopW = new Stopwatch();
stopW.Start();
File.Delete("output.txt");
using(var file = File.Create("output.txt"))
{
    using(var fs = new StreamWriter(file))
    {
        fs.WriteLine(stopW.Elapsed);             
    }

}
var blCollection = new BlockingCollection<Node>();


var thr = new Thread(() => {
{
    foreach(var node in parser.Read()) {
        blCollection.Add(node);    
        // Console.WriteLine(blCollection.Count);
    }

    Console.WriteLine("read");
    blCollection.CompleteAdding();
}
});

thr.Start();

Node node;
var delat = TimeSpan.FromSeconds(1.0);
while(blCollection.TryTake(out node, delat))
{
    // stopW.Start();
    builder.AddNode(node);    
    // Console.WriteLine(stopW.Elapsed);
}
Console.WriteLine("processed");
Console.WriteLine($"pages {Page.pages}");
var dur = stopW.Elapsed;
Console.WriteLine(dur);
blCollection.Dispose();
 using(var file = File.Open("output.txt", FileMode.Append))
{
    using(var fs = new StreamWriter(file))
    {
        foreach(var item in builder.GetInOrder())
            fs.WriteLine(item);
        
        // fs.WriteLine(dur);
    }
}