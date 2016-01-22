using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAParsingTool
{
    class Results
    {
        public string machine { get; set; }
        public string tool { get; set; }
        public string version { get; set; }
        public string status { get; set; }
        public string iaControls { get; set; }
        public string vId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string checkContent { get; set; }
        public string findingDetails { get; set; }
        public string fix { get; set; }
        public string cve { get; set; }
        public string iavm { get; set; }
        public string comments { get; set; }
    }

    class Nmap
    {
        public string portNum { get; set; }
        public string protocol { get; set; }
        public string name { get; set; }
        public string machineName { get; set; }
    }
}
