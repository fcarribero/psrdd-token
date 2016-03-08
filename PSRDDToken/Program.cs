using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PSRDDToken
{
    class Program
    {
        static void Main(string[] args)
        {
            //Trust all certificates
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            Autenticacion.AutenticacionClient autenticador = new Autenticacion.AutenticacionClient();

            autenticador.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(getExecutionPath() + @"\CSD_Pruebas_PDK050301MW1.pfx");
            try
            {
                string token = autenticador.Autentica();
                File.WriteAllText(getExecutionPath() + @"\token.txt", token);
                log("Ok");
            }
            catch (Exception e)
            {
                log("Connection Error to Service<" + e.Message + ">");
            }
        }

        static void log(string message)
        {
            File.AppendAllText(getExecutionPath() + @"\execution.log", DateTime.Now + ": " + message + "\r\n");
            Console.WriteLine(DateTime.Now + ": " + message);
        }

        static string getExecutionPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }
    }
}
