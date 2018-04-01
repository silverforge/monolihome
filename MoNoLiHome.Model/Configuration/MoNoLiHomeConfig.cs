namespace MoNoLiHome.Model.Configuration
{
    public class MoNoLiHomeConfig
    {
        public int DefaultPort { get; set; }
        public string BaseUrl { get; set; }
        public string EndpointPath { get; set; }
        public string Function { get; set; }
        public RedisConfig Redis { get; set; }
    }
}
