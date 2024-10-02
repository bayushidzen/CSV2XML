namespace CSV2XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string logpath = @"\logs";
            string logpathxml = @"\output_to_bd\";
            string logpathdone = @"\done\done_";

            string[] dirs = Directory.GetFiles(logpath, "*.csv");
            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
                string[] source = File.ReadAllLines(dir);
                string[] editedSource = DeleteFirstLine(source);
                string filename = Path.GetFileName(dir) + ".xml";
                File.Copy(dir, logpathdone + Path.GetFileName(dir), true);
            }

            static string[] DeleteFirstLine(string[] source)
            {
                string[] newSource = new string[source.Length - 1];
                Array.Copy(source, 1, newSource, 0, source.Length - 1);
                return newSource;
            }
        }
    }
}
