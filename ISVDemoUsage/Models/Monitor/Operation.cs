
using ISVDemoUsage.Models.Monitor;

namespace ISVDemoUsage.Models.Monitor
{
    public class Operation
    {
        public string Id { get; set; }
        public string name { get; set; }
        public bool isDataAction { get; set; }
        public Display display { get; set; }
        //public Properties properties { get; set; }
    }
}
