using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Splite_Big_Files_For_FTP
{
    internal class ftp2
    {
        public static int putfile(string filePath, string remotePath)
        {

            string ftpServer = "";
            string username = "";
            string password = "";
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpServer + "/" + remotePath);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                byte[] fileContents = File.ReadAllBytes(filePath);
                request.ContentLength = fileContents.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
                return 200;
            }
            catch (Exception x)
            {

                Console.WriteLine(x);
                return 510;
            }



        }
    }

}
