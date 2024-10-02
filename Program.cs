using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CSV2XML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string logpath = @"\logs";
            string logpathxml = @"\output_to_bd\";
            string logpathdone = @"\done\done_";

            string[] logs = Directory.GetFiles(logpath, "*.csv");
            foreach (string log in logs)
            {
                Console.WriteLine(log);
                string[] source = File.ReadAllLines(log);
                string[] editedSource = DeleteFirstLine(source);
                string filename = Path.GetFileName(log) + ".xml";
                CreateXML(editedSource, logpathxml, filename);
                File.Copy(log, logpathdone + Path.GetFileName(log), true);
                File.Delete(log);
            }
            static void CreateXML(string[] source, string logpathxml, string filename)
            {
                XDocument cust = new XDocument(new XElement("Root"));
                foreach (string line in source)
                {
                    cust.Root.Add(new XElement("User",
                        new XElement("login", SchoolLoginSearch(line)),
                        new XElement("Type", LogTypeSearch(line)),
                        new XElement("Date", DateSearch(line)),
                        new XElement("Time", TimeSearch(line)),
                        new XElement("IP_Address", IPAdressSearch(line)),
                        new XElement("Action", ActionSearch(line)),
                        new XElement("UserAgent", UserAgentSearch(line))));
                }
                cust.Save(logpathxml + filename);
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

            static string DateSearch(string line)
            {
                Match match = Regex.Match(line, @"\d+[-]\d+[-]\d+");
                if (match.Success)
                {
                    return match.Captures[0].Value;
                }
                return "bad date";
            }

            static string TimeSearch(string line)
            {
                Match match = Regex.Match(line, @"\d+[:]\d+[:]\d+");
                if (match.Success)
                {
                    return match.Captures[0].Value;
                }
                return "bad time";
            }

            static string IPAdressSearch(string line)
            {
                Match match = Regex.Match(line, @"\d+[.]\d+[.]\d+[.]\d+");
                if (match.Success)
                {
                    return match.Captures[0].Value;
                }
                return "bad IP";
            }

            static string ActionSearch(string line)
            {
                string action = "";
                string noAction = "noAction";

                string sampleActionText1 = "sample action text 1";
                string sampleActionText2 = "sample action text 2";
                string sampleActionText3 = "sample action text 3";

                string sampleNoActionText1 = "sample action text 1";
                string sampleNoActionText2 = "sample action text 2";
                string sampleNoActionText3 = "sample action text 3";

                int indexOfSampleActionText1 = line.IndexOf(sampleActionText1);
                int indexOfSampleActionText2 = line.IndexOf(sampleActionText2);
                int indexOfSampleActionText3 = line.IndexOf(sampleActionText3);

                int indexOfSampleNoActionText1 = line.IndexOf(sampleNoActionText1);
                int indexOfSampleNoActionText2 = line.IndexOf(sampleNoActionText2);
                int indexOfSampleNoActionText3 = line.IndexOf(sampleNoActionText3);

                if (indexOfSampleActionText1 != -1)
                {
                    action = line.Substring(indexOfSampleActionText1, line.Length - indexOfSampleActionText1);
                }
                else if (indexOfSampleActionText2 != -1)
                {
                    action = line.Substring(indexOfSampleActionText2, line.Length - indexOfSampleActionText2);
                }
                else if (indexOfSampleActionText3 != -1)
                {
                    action = line.Substring(indexOfSampleActionText3, line.Length - indexOfSampleActionText3);
                }
                else if ((indexOfSampleNoActionText1 != -1) || (indexOfSampleNoActionText2 != -1) || (indexOfSampleNoActionText3 != -1))
                {
                    action = noAction;
                }

                return action;
            }

            static string UserAgentSearch(string line)
            {
                string useragent = "";

                string sampleUserAgentText1 = "sample user agent text 1";
                string sampleUserAgentText2 = "sample user agent text 2";
                string sampleUserAgentText3 = "sample user agent text 3";

                int indexOfSampleUserAgentText1 = line.IndexOf(sampleUserAgentText1);
                int indexOfSampleUserAgentText2 = line.IndexOf(sampleUserAgentText2);
                int indexOfSampleUserAgentText3 = line.IndexOf(sampleUserAgentText3);

                if (indexOfSampleUserAgentText1 != -1)
                {
                    useragent = line.Substring(indexOfSampleUserAgentText1, line.Length - indexOfSampleUserAgentText1);
                }
                else if (indexOfSampleUserAgentText2 != -1)
                {
                    useragent = line.Substring(indexOfSampleUserAgentText2, line.Length - indexOfSampleUserAgentText2);
                }
                else if (indexOfSampleUserAgentText3 != -1)
                {
                    useragent = line.Substring(indexOfSampleUserAgentText3, line.Length - indexOfSampleUserAgentText3);
                }
                else
                {
                    useragent = "unknown_useragent";
                }
                return useragent;
            }
        }
    }
}
