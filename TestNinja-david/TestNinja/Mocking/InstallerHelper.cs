using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly IClient _client;

        public InstallerHelper(IClient client)
        {
            _client = client;
        }

        public bool DownloadInstaller(string url, string path)
        {
            try
            {
                _client.DownloadFile(url, path);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }

    public interface IClient
    {
        bool DownloadFile(string url, string path);
    }
}