public class Parser
{
    private readonly string filePath;

    public Parser(string path)
    {
        this.filePath = path;
    }

    public IEnumerable<Node> Read()
    {
        var lines = File.ReadLines(filePath);
        foreach (var line in lines) 
        {
            var parsed = line.Split(". ");
            yield return new Node(parsed[1], Int32.Parse(parsed[0]), 0, 0);
        }        
    }

}