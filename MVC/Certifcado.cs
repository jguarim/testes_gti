using System.Net.Http;

namespace MVC
{
    public static class Certifcado
    {
        public static HttpClientHandler HabilitarSSL()
        {
            HttpClientHandler clientHandler = new();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            return clientHandler;
        }
    }
}
