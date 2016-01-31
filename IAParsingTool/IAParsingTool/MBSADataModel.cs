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
    public partial class SecScan
    {

        private SecScanIPList iPListField;

        private SecScanAdditCabs additCabsField;

        private SecScanCheck[] checkField;

        private byte compositeField;

        private byte idField;

        private string displayNameField;

        private string machineField;

        private string dateField;

        private string lDateField;

        private string domainField;

        private string ipField;

        private byte gradeField;

        private /*System.DateTime*/string hotfixDataVersionField;

        private string mbsaToolVersionField;

        private string isWorkgroupField;

        private string sUSServerField;

        private byte hFFlagsField;

        private string securityUpdatesScanDoneField;

        private string wUSSourceField;

        private bool isCSAModeField;

        /// <remarks/>
        public SecScanIPList IPList
        {
            get
            {
                return this.iPListField;
            }
            set
            {
                this.iPListField = value;
            }
        }

        /// <remarks/>
        public SecScanAdditCabs AdditCabs
        {
            get
            {
                return this.additCabsField;
            }
            set
            {
                this.additCabsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Check")]
        public SecScanCheck[] Check
        {
            get
            {
                return this.checkField;
            }
            set
            {
                this.checkField = value;
            }
        }

        /// <remarks/>
        public byte Composite
        {
            get
            {
                return this.compositeField;
            }
            set
            {
                this.compositeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Machine
        {
            get
            {
                return this.machineField;
            }
            set
            {
                this.machineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Date
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LDate
        {
            get
            {
                return this.lDateField;
            }
            set
            {
                this.lDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Domain
        {
            get
            {
                return this.domainField;
            }
            set
            {
                this.domainField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IP
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Grade
        {
            get
            {
                return this.gradeField;
            }
            set
            {
                this.gradeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public /*System.DateTime*/string HotfixDataVersion
        {
            get
            {
                return this.hotfixDataVersionField;
            }
            set
            {
                this.hotfixDataVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MbsaToolVersion
        {
            get
            {
                return this.mbsaToolVersionField;
            }
            set
            {
                this.mbsaToolVersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string IsWorkgroup
        {
            get
            {
                return this.isWorkgroupField;
            }
            set
            {
                this.isWorkgroupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SUSServer
        {
            get
            {
                return this.sUSServerField;
            }
            set
            {
                this.sUSServerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte HFFlags
        {
            get
            {
                return this.hFFlagsField;
            }
            set
            {
                this.hFFlagsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SecurityUpdatesScanDone
        {
            get
            {
                return this.securityUpdatesScanDoneField;
            }
            set
            {
                this.securityUpdatesScanDoneField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WUSSource
        {
            get
            {
                return this.wUSSourceField;
            }
            set
            {
                this.wUSSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsCSAMode
        {
            get
            {
                return this.isCSAModeField;
            }
            set
            {
                this.isCSAModeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanIPList
    {

        private SecScanIPListIP ipField;

        /// <remarks/>
        public SecScanIPListIP IP
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanIPListIP
    {

        private string addrField;

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
    public partial class SecScanAdditCabs
    {

        private SecScanAdditCabsCab cabField;

        /// <remarks/>
        public SecScanAdditCabsCab Cab
        {
            get
            {
                return this.cabField;
            }
            set
            {
                this.cabField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanAdditCabsCab
    {

        private string propField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Prop
        {
            get
            {
                return this.propField;
            }
            set
            {
                this.propField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanCheck
    {

        private string adviceField;

        private SecScanCheckUpdateData[] detailField;

        private ushort idField;

        private byte gradeField;

        private byte typeField;

        private byte catField;

        private byte rankField;

        private string nameField;

        private string uRL1Field;

        private string uRL2Field;

        private string groupIDField;

        private string groupNameField;

        /// <remarks/>
        public string Advice
        {
            get
            {
                return this.adviceField;
            }
            set
            {
                this.adviceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("UpdateData", IsNullable = false)]
        public SecScanCheckUpdateData[] Detail
        {
            get
            {
                return this.detailField;
            }
            set
            {
                this.detailField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Grade
        {
            get
            {
                return this.gradeField;
            }
            set
            {
                this.gradeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Type
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
        public byte Cat
        {
            get
            {
                return this.catField;
            }
            set
            {
                this.catField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string URL1
        {
            get
            {
                return this.uRL1Field;
            }
            set
            {
                this.uRL1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string URL2
        {
            get
            {
                return this.uRL2Field;
            }
            set
            {
                this.uRL2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GroupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GroupName
        {
            get
            {
                return this.groupNameField;
            }
            set
            {
                this.groupNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanCheckUpdateData
    {

        private string titleField;

        private SecScanCheckUpdateDataReferences referencesField;

        private SecScanCheckUpdateDataOtherID[] otherIDsField;

        private string idField;

        private string gUIDField;

        private string bulletinIDField;

        private uint kBIDField;

        private byte typeField;

        private bool isInstalledField;

        private byte severityField;

        private bool restartRequiredField;

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public SecScanCheckUpdateDataReferences References
        {
            get
            {
                return this.referencesField;
            }
            set
            {
                this.referencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("OtherID", IsNullable = false)]
        public SecScanCheckUpdateDataOtherID[] OtherIDs
        {
            get
            {
                return this.otherIDsField;
            }
            set
            {
                this.otherIDsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string GUID
        {
            get
            {
                return this.gUIDField;
            }
            set
            {
                this.gUIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BulletinID
        {
            get
            {
                return this.bulletinIDField;
            }
            set
            {
                this.bulletinIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint KBID
        {
            get
            {
                return this.kBIDField;
            }
            set
            {
                this.kBIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Type
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
        public bool IsInstalled
        {
            get
            {
                return this.isInstalledField;
            }
            set
            {
                this.isInstalledField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte Severity
        {
            get
            {
                return this.severityField;
            }
            set
            {
                this.severityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool RestartRequired
        {
            get
            {
                return this.restartRequiredField;
            }
            set
            {
                this.restartRequiredField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanCheckUpdateDataReferences
    {

        private string bulletinURLField;

        private string informationURLField;

        private string downloadURLField;

        /// <remarks/>
        public string BulletinURL
        {
            get
            {
                return this.bulletinURLField;
            }
            set
            {
                this.bulletinURLField = value;
            }
        }

        /// <remarks/>
        public string InformationURL
        {
            get
            {
                return this.informationURLField;
            }
            set
            {
                this.informationURLField = value;
            }
        }

        /// <remarks/>
        public string DownloadURL
        {
            get
            {
                return this.downloadURLField;
            }
            set
            {
                this.downloadURLField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class SecScanCheckUpdateDataOtherID
    {

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
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


}
