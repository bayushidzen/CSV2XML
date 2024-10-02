using System.Text.RegularExpressions;

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
            static string SchoolLoginSearch(string line)
            {
                string schoolLogin = "";
                Match repetitor = Regex.Match(line, @"[r][e][p]\d+");
                Match admin = Regex.Match(line, @"[a][d][m][i][n]\d+");
                Match cource = Regex.Match(line, @"[c][o][u][r][c][e]\d+");
                Match school = Regex.Match(line, @"[s][c][h][o][o][l]\d+");
                Match anon = Regex.Match(line, @"AnonymousUser");
                if (repetitor.Success)
                {
                    schoolLogin = repetitor.Captures[0].Value;
                }
                else if (admin.Success)
                {
                    schoolLogin = admin.Captures[0].Value;
                }
                else if (cource.Success)
                {
                    schoolLogin = cource.Captures[0].Value;
                }
                else if (school.Success)
                {
                    schoolLogin = school.Captures[0].Value;
                }
                else if (anon.Success)
                {
                    schoolLogin = anon.Captures[0].Value;
                }
                else
                {
                    schoolLogin = "unknown_user";
                }
                return schoolLogin;
            }
            static string LogTypeSearch(string line)
            {
                Match mainLog = Regex.Match(line, @"[[][I][N][F][O][]]");
                Match errorLog = Regex.Match(line, @"[[][E][R][R][O][R][]]");
                Match warningLog = Regex.Match(line, @"[[][W][A][R][N][I][N][G][]]");
                Match criticalLog = Regex.Match(line, @"[[][C][R][I][T][I][C][A][L][]]");

                if (mainLog.Success)
                {
                    return mainLog.Captures[0].Value;
                }
                else if (errorLog.Success)
                {
                    return errorLog.Captures[0].Value;
                }
                else if (warningLog.Success)
                {
                    return warningLog.Captures[0].Value;
                }
                else if (criticalLog.Success)
                {
                    return criticalLog.Captures[0].Value;
                }
                return "[unknown_log_type]";
            }
        }
    }
}
