using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAParsingTool
{


    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class nmaprun
    {

        private nmaprunScaninfo[] scaninfoField;

        private nmaprunVerbose verboseField;

        private nmaprunDebugging debuggingField;

        private nmaprunOutput outputField;

        private nmaprunHost hostField;

        private nmaprunRunstats runstatsField;

        private uint startField;

        private string profile_nameField;

        private decimal xmloutputversionField;

        private string scannerField;

        private decimal versionField;

        private string startstrField;

        private string argsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("scaninfo")]
        public nmaprunScaninfo[] scaninfo
        {
            get
            {
                return this.scaninfoField;
            }
            set
            {
                this.scaninfoField = value;
            }
        }

        /// <remarks/>
        public nmaprunVerbose verbose
        {
            get
            {
                return this.verboseField;
            }
            set
            {
                this.verboseField = value;
            }
        }

        /// <remarks/>
        public nmaprunDebugging debugging
        {
            get
            {
                return this.debuggingField;
            }
            set
            {
                this.debuggingField = value;
            }
        }

        /// <remarks/>
        public nmaprunOutput output
        {
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
            }
        }

        /// <remarks/>
        public nmaprunHost host
        {
            get
            {
                return this.hostField;
            }
            set
            {
                this.hostField = value;
            }
        }

        /// <remarks/>
        public nmaprunRunstats runstats
        {
            get
            {
                return this.runstatsField;
            }
            set
            {
                this.runstatsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string profile_name
        {
            get
            {
                return this.profile_nameField;
            }
            set
            {
                this.profile_nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal xmloutputversion
        {
            get
            {
                return this.xmloutputversionField;
            }
            set
            {
                this.xmloutputversionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string scanner
        {
            get
            {
                return this.scannerField;
            }
            set
            {
                this.scannerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string startstr
        {
            get
            {
                return this.startstrField;
            }
            set
            {
                this.startstrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string args
        {
            get
            {
                return this.argsField;
            }
            set
            {
                this.argsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunScaninfo
    {

        private string servicesField;

        private string protocolField;

        private ushort numservicesField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string services
        {
            get
            {
                return this.servicesField;
            }
            set
            {
                this.servicesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort numservices
        {
            get
            {
                return this.numservicesField;
            }
            set
            {
                this.numservicesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunVerbose
    {

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunDebugging
    {

        private byte levelField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunOutput
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHost
    {

        private nmaprunHostStatus statusField;

        private nmaprunHostAddress addressField;

        private nmaprunHostHostnames hostnamesField;

        private nmaprunHostPort[] portsField;

        private object osField;

        private nmaprunHostUptime uptimeField;

        private nmaprunHostTcpsequence tcpsequenceField;

        private nmaprunHostIpidsequence ipidsequenceField;

        private nmaprunHostTcptssequence tcptssequenceField;

        private string commentField;

        /// <remarks/>
        public nmaprunHostStatus status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostAddress address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostHostnames hostnames
        {
            get
            {
                return this.hostnamesField;
            }
            set
            {
                this.hostnamesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("port", IsNullable = false)]
        public nmaprunHostPort[] ports
        {
            get
            {
                return this.portsField;
            }
            set
            {
                this.portsField = value;
            }
        }

        /// <remarks/>
        public object os
        {
            get
            {
                return this.osField;
            }
            set
            {
                this.osField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostUptime uptime
        {
            get
            {
                return this.uptimeField;
            }
            set
            {
                this.uptimeField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostTcpsequence tcpsequence
        {
            get
            {
                return this.tcpsequenceField;
            }
            set
            {
                this.tcpsequenceField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostIpidsequence ipidsequence
        {
            get
            {
                return this.ipidsequenceField;
            }
            set
            {
                this.ipidsequenceField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostTcptssequence tcptssequence
        {
            get
            {
                return this.tcptssequenceField;
            }
            set
            {
                this.tcptssequenceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string comment
        {
            get
            {
                return this.commentField;
            }
            set
            {
                this.commentField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostStatus
    {

        private string stateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostAddress
    {

        private string addrtypeField;

        private string vendorField;

        private string addrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string addrtype
        {
            get
            {
                return this.addrtypeField;
            }
            set
            {
                this.addrtypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vendor
        {
            get
            {
                return this.vendorField;
            }
            set
            {
                this.vendorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string addr
        {
            get
            {
                return this.addrField;
            }
            set
            {
                this.addrField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostHostnames
    {

        private nmaprunHostHostnamesHostname hostnameField;

        /// <remarks/>
        public nmaprunHostHostnamesHostname hostname
        {
            get
            {
                return this.hostnameField;
            }
            set
            {
                this.hostnameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostHostnamesHostname
    {

        private string typeField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostPort
    {

        private nmaprunHostPortState stateField;

        private nmaprunHostPortService serviceField;

        private string protocolField;

        private ushort portidField;

        /// <remarks/>
        public nmaprunHostPortState state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public nmaprunHostPortService service
        {
            get
            {
                return this.serviceField;
            }
            set
            {
                this.serviceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort portid
        {
            get
            {
                return this.portidField;
            }
            set
            {
                this.portidField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostPortState
    {

        private string reasonField;

        private string stateField;

        private byte reason_ttlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte reason_ttl
        {
            get
            {
                return this.reason_ttlField;
            }
            set
            {
                this.reason_ttlField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostPortService
    {

        private string methodField;

        private byte confField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string method
        {
            get
            {
                return this.methodField;
            }
            set
            {
                this.methodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte conf
        {
            get
            {
                return this.confField;
            }
            set
            {
                this.confField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostUptime
    {

        private string lastbootField;

        private string secondsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lastboot
        {
            get
            {
                return this.lastbootField;
            }
            set
            {
                this.lastbootField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string seconds
        {
            get
            {
                return this.secondsField;
            }
            set
            {
                this.secondsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostTcpsequence
    {

        private string indexField;

        private string valuesField;

        private string difficultyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string values
        {
            get
            {
                return this.valuesField;
            }
            set
            {
                this.valuesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string difficulty
        {
            get
            {
                return this.difficultyField;
            }
            set
            {
                this.difficultyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostIpidsequence
    {

        private string valuesField;

        private string classField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string values
        {
            get
            {
                return this.valuesField;
            }
            set
            {
                this.valuesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunHostTcptssequence
    {

        private string valuesField;

        private string classField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string values
        {
            get
            {
                return this.valuesField;
            }
            set
            {
                this.valuesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                this.classField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunRunstats
    {

        private nmaprunRunstatsFinished finishedField;

        private nmaprunRunstatsHosts hostsField;

        /// <remarks/>
        public nmaprunRunstatsFinished finished
        {
            get
            {
                return this.finishedField;
            }
            set
            {
                this.finishedField = value;
            }
        }

        /// <remarks/>
        public nmaprunRunstatsHosts hosts
        {
            get
            {
                return this.hostsField;
            }
            set
            {
                this.hostsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunRunstatsFinished
    {

        private string timestrField;

        private uint timeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string timestr
        {
            get
            {
                return this.timestrField;
            }
            set
            {
                this.timestrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint time
        {
            get
            {
                return this.timeField;
            }
            set
            {
                this.timeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nmaprunRunstatsHosts
    {

        private byte downField;

        private byte totalField;

        private byte upField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte down
        {
            get
            {
                return this.downField;
            }
            set
            {
                this.downField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte up
        {
            get
            {
                return this.upField;
            }
            set
            {
                this.upField = value;
            }
        }
    }


}
