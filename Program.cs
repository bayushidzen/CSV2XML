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
                string filename = Path.GetFileName(dir) + ".xml";
                File.Copy(dir, logpathdone + Path.GetFileName(dir), true);
            }
        }
    }
}
