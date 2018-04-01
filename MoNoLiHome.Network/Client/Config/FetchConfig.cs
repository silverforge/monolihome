namespace MoNoLiHome.Network.Client
{
    public class FetchConfig
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string Url
        {
            get
            {
                Host = Host ?? string.Empty;
                Path = Path ?? string.Empty;
                return string.Format($"{Host}/{Path}");
            }
        }
    }
}