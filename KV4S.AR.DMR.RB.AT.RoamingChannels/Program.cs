using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KV4S.AmateurRadio.IRLP.StationTracking
{
    class Program
    {
        private static List<string> _stateList = null;
        private static string StateListString
        {
            set
            {
                string[] callsignArray = value.Split(',');
                _stateList = new List<string>(callsignArray.Length);
                _stateList.AddRange(callsignArray);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the AnyTone Roaming Channels by State application by KV4S!");
                Console.WriteLine(" ");
                Console.WriteLine("Beginning download");
                Console.WriteLine("Please Stand by.....");
                Console.WriteLine(" ");
                using (WebClient wc = new WebClient())
                {
                    StateListString = ConfigurationManager.AppSettings["RB_StateCode"].ToUpper();
                    foreach (string stateCode in _stateList)
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        var rbHTML = wc.DownloadString("https://www.repeaterbook.com/repeaters/feature_search.php?state_id=" + stateCode + "&type=DMR");
                        
                        Console.WriteLine("Checking state " + stateCode);

                        string[] strRowEndSplit = new string[] { "/repeaters/details.php" };
                        string[] strStatusendSplit = rbHTML.Split(strRowEndSplit, StringSplitOptions.RemoveEmptyEntries);
                        

                        //status begin split
                        string[] strHtmlSplit = new string[] { "<td>" };
                        string[] strStatusSplit = strStatusendSplit[0].Split(strHtmlSplit, StringSplitOptions.RemoveEmptyEntries);

                        //status
                        

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Program encountered and error:");
                Console.WriteLine(ex.Message);
               
            }
            finally
            {
                if (ConfigurationManager.AppSettings["Unattended"] == "N")
                {
                    Console.WriteLine("Press any key on your keyboard to quit...");
                    Console.ReadKey();
                }
            }
        }

       
    }
}
