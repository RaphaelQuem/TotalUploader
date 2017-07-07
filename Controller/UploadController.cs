using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TotalUploader.Controller
{
    public class UploadController
    {
        string host;
        string user;
        string pass;
        string dir;

        public UploadController()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);


                key = key.OpenSubKey("TotalUploader", true);

                host = (key.GetValue("Host") == null ? "" : key.GetValue("Host")).ToString();
                user = (key.GetValue("User") == null ? "" : key.GetValue("User")).ToString();
                pass = (key.GetValue("Pass") == null ? "" : key.GetValue("Pass")).ToString();
                dir = (key.GetValue("Dir") == null ? "" : key.GetValue("Dir")).ToString();

            }
            catch
            {
                throw new Exception("Erro ao tentar ler as chaves do registro!");
            }
        }

        public void EnviarNfe(string diretorioArq, string nomeArq)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host + dir + nomeArq);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(user, pass);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;

            string arquivo = diretorioArq + "\\" + nomeArq;

            StreamReader sourceStream = new StreamReader(arquivo);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }
    }
}
