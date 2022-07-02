// See https://aka.ms/new-console-template for more information

using System.Text;

var logPath = "../main/input.txt"; //System.IO.Path.GetTempFileName();
using (var logFile = System.IO.File.Create (logPath)) {
    // Generate a random number  
    Random random = new Random ();

    using (var sw = new StreamWriter (logFile)) {
        for (var i = 0; i < 883647; i++) {
            var str = RandomString (random, 10);
            sw.WriteLine ($"{random.Next()}. {str}");
        }
    }
}

string RandomString (Random random, int size, bool lowerCase = true) {
    StringBuilder builder = new StringBuilder ();
    char ch;
    for (int i = 0; i < size; i++) {
        ch = Convert.ToChar (Convert.ToInt32 (Math.Floor (26 * random.NextDouble () + 65)));
        builder.Append (ch);
    }
    if (lowerCase)
        return builder.ToString ().ToLower ();
    return builder.ToString ();
}