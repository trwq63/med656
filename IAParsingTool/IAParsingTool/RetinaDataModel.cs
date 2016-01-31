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
    public partial class scanJob
    {

        private scanJobHosts hostsField;

        private scanJobMetrics metricsField;

        /// <remarks/>
        public scanJobHosts hosts
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

        /// <remarks/>
        public scanJobMetrics metrics
        {
            get
            {
                return this.metricsField;
            }
            set
            {
                this.metricsField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class scanJobHosts
    {

        private scanJobHostsHost hostField;

        /// <remarks/>
        public scanJobHostsHost host
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class scanJobHostsHost
    {

        private string ipField;

        private string netBIOSNameField;

        private string netBIOSDomainField;

        private string dnsNameField;

        private string macField;

        private string osField;

        private string cpeField;

        private scanJobHostsHostAudit[] auditField;

        /// <remarks/>
        public string ip
        {
            get
            {
                return this.ipField;
            }
            set
            {
                this.ipField = value;
            }
        }

        /// <remarks/>
        public string netBIOSName
        {
            get
            {
                return this.netBIOSNameField;
            }
            set
            {
                this.netBIOSNameField = value;
            }
        }

        /// <remarks/>
        public string netBIOSDomain
        {
            get
            {
                return this.netBIOSDomainField;
            }
            set
            {
                this.netBIOSDomainField = value;
            }
        }

        /// <remarks/>
        public string dnsName
        {
            get
            {
                return this.dnsNameField;
            }
            set
            {
                this.dnsNameField = value;
            }
        }

        /// <remarks/>
        public string mac
        {
            get
            {
                return this.macField;
            }
            set
            {
                this.macField = value;
            }
        }

        /// <remarks/>
        public string os
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
        public string cpe
        {
            get
            {
                return this.cpeField;
            }
            set
            {
                this.cpeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("audit")]
        public scanJobHostsHostAudit[] audit
        {
            get
            {
                return this.auditField;
            }
            set
            {
                this.auditField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class scanJobHostsHostAudit
    {

        private ushort rthIDField;

        private string cveField;

        private string cceField;

        private string iavField;

        private string nameField;

        private string descriptionField;

        private string dateField;

        private string sevCodeField;

        private string riskField;

        private string pciLevelField;

        private string pciReasonField;

        private string pciPassFailField;

        private string cvssScoreField;

        private string fixInformationField;

        private string exploitField;

        private string contextField;

        /// <remarks/>
        public ushort rthID
        {
            get
            {
                return this.rthIDField;
            }
            set
            {
                this.rthIDField = value;
            }
        }

        /// <remarks/>
        public string cve
        {
            get
            {
                return this.cveField;
            }
            set
            {
                this.cveField = value;
            }
        }

        /// <remarks/>
        public string cce
        {
            get
            {
                return this.cceField;
            }
            set
            {
                this.cceField = value;
            }
        }

        /// <remarks/>
        public string iav
        {
            get
            {
                return this.iavField;
            }
            set
            {
                this.iavField = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        public string sevCode
        {
            get
            {
                return this.sevCodeField;
            }
            set
            {
                this.sevCodeField = value;
            }
        }

        /// <remarks/>
        public string risk
        {
            get
            {
                return this.riskField;
            }
            set
            {
                this.riskField = value;
            }
        }

        /// <remarks/>
        public string pciLevel
        {
            get
            {
                return this.pciLevelField;
            }
            set
            {
                this.pciLevelField = value;
            }
        }

        /// <remarks/>
        public string pciReason
        {
            get
            {
                return this.pciReasonField;
            }
            set
            {
                this.pciReasonField = value;
            }
        }

        /// <remarks/>
        public string pciPassFail
        {
            get
            {
                return this.pciPassFailField;
            }
            set
            {
                this.pciPassFailField = value;
            }
        }

        /// <remarks/>
        public string cvssScore
        {
            get
            {
                return this.cvssScoreField;
            }
            set
            {
                this.cvssScoreField = value;
            }
        }

        /// <remarks/>
        public string fixInformation
        {
            get
            {
                return this.fixInformationField;
            }
            set
            {
                this.fixInformationField = value;
            }
        }

        /// <remarks/>
        public string exploit
        {
            get
            {
                return this.exploitField;
            }
            set
            {
                this.exploitField = value;
            }
        }

        /// <remarks/>
        public string context
        {
            get
            {
                return this.contextField;
            }
            set
            {
                this.contextField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class scanJobMetrics
    {

        private string jobNameField;

        private string fileNameField;

        private string scannerVersionField;

        private string auditsRevisionField;

        private string credentialsField;

        private string auditGroupsField;

        private string addressGroupsField;

        private string ipRangesField;

        private byte attemptedField;

        private byte scannedField;

        private byte noAdminField;

        private byte noResponseField;

        private string startField;

        private string durationField;

        /// <remarks/>
        public string jobName
        {
            get
            {
                return this.jobNameField;
            }
            set
            {
                this.jobNameField = value;
            }
        }

        /// <remarks/>
        public string fileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        public string scannerVersion
        {
            get
            {
                return this.scannerVersionField;
            }
            set
            {
                this.scannerVersionField = value;
            }
        }

        /// <remarks/>
        public string auditsRevision
        {
            get
            {
                return this.auditsRevisionField;
            }
            set
            {
                this.auditsRevisionField = value;
            }
        }

        /// <remarks/>
        public string credentials
        {
            get
            {
                return this.credentialsField;
            }
            set
            {
                this.credentialsField = value;
            }
        }

        /// <remarks/>
        public string auditGroups
        {
            get
            {
                return this.auditGroupsField;
            }
            set
            {
                this.auditGroupsField = value;
            }
        }

        /// <remarks/>
        public string addressGroups
        {
            get
            {
                return this.addressGroupsField;
            }
            set
            {
                this.addressGroupsField = value;
            }
        }

        /// <remarks/>
        public string ipRanges
        {
            get
            {
                return this.ipRangesField;
            }
            set
            {
                this.ipRangesField = value;
            }
        }

        /// <remarks/>
        public byte attempted
        {
            get
            {
                return this.attemptedField;
            }
            set
            {
                this.attemptedField = value;
            }
        }

        /// <remarks/>
        public byte scanned
        {
            get
            {
                return this.scannedField;
            }
            set
            {
                this.scannedField = value;
            }
        }

        /// <remarks/>
        public byte noAdmin
        {
            get
            {
                return this.noAdminField;
            }
            set
            {
                this.noAdminField = value;
            }
        }

        /// <remarks/>
        public byte noResponse
        {
            get
            {
                return this.noResponseField;
            }
            set
            {
                this.noResponseField = value;
            }
        }

        /// <remarks/>
        public string start
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
        public string duration
        {
            get
            {
                return this.durationField;
            }
            set
            {
                this.durationField = value;
            }
        }
    }





}
