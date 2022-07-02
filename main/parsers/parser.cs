public class Parser
{
    private readonly string filePath;

    public Parser(string path)
    {
        this.filePath = path;
    }

    public IEnumerable<Node> Read()
    {
        using(var fs = File.OpenRead(filePath))
        using (var sr = new StreamReader(fs)) 
        {
            while(!sr.EndOfStream) 
            {
                var position = sr.BaseStream.Position;
                var line = sr.ReadLine();
                var parsed = line.Split(". ");

                yield return new Node(parsed[1], Int32.Parse(parsed[0]), position, line.Length);
            }
        }
    }

}