using PRRODriver.Entities;
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PRRODriver
{
    public class Driver
    {
        private const int randomSeedLenght = 18;

        public Driver()
        {
            
        }
        
        //Name of the method means it will return server state.  No need to pass Query here. It has only one targer.
        //In case of the name, some thing like "GetResponse(string aQuery)"  it could be;
        public string GetServerState()
        {
            string response = string.Empty;
            var url = GetRealUri();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = Constants.ContentTypeJson;
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(Queries.ServerState);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            return response;
        }

        public ResponseObjects GetTaxObjects(AccountInfo aAccount)
        {
            string response = String.Empty;

            var ctr = new X509Certificate2(aAccount.SertificatePath, aAccount.Password);
            var provider = ctr.PublicKey.Key as RSACryptoServiceProvider;
            var dta = provider.Encrypt(Encoding.ASCII.GetBytes(Queries.Objects), true);
            // byte[]
            var sig = provider.SignData(dta, "SHA1");

            var serverUrl = GetRealUri();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serverUrl);
            httpWebRequest.ContentType = Constants.ContentTypeStream;

            httpWebRequest.Method = Constants.PostStr;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(sig);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            return null;
        }

        private string GetRandomDigits(int length)
        {
            //I don't realy like such "summary". It hasn't any important information.  Method name = RandomDigits,  summary = Generate random digits. No surprises :)
            //Good practice to use naming of the methods when by name you can undestand what this methos does. GetRandomDigits for example. parameter lenght in this case also obvious and no need to be commented in the summary;
            //Example in the next method,  i think it is clear without summary
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        private Uri GetRealUri()
        {
            var serverUri = new Uri(Constants.BaseUrl);
            return new Uri(serverUri, $"fs/cmd?randomseed={GetRandomDigits(randomSeedLenght)}");
        }
    }
}
