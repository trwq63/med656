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
    public partial class XMLOut
    {

        private XMLOutCatalogInfo catalogInfoField;

        private XMLOutCheck[] checkField;

        /// <remarks/>
        public XMLOutCatalogInfo CatalogInfo
        {
            get
            {
                return this.catalogInfoField;
            }
            set
            {
                this.catalogInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Check")]
        public XMLOutCheck[] Check
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XMLOutCatalogInfo
    {

        private /*System.DateTime*/ string creationDateField;

        /// <remarks/>
        public /*System.DateTime*/ string CreationDate
        {
            get
            {
                return this.creationDateField;
            }
            set
            {
                this.creationDateField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XMLOutCheck
    {

        private string adviceField;

        private XMLOutCheckUpdateData[] detailField;

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
        public XMLOutCheckUpdateData[] Detail
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
    public partial class XMLOutCheckUpdateData
    {

        private string titleField;

        private XMLOutCheckUpdateDataReferences referencesField;

        private XMLOutCheckUpdateDataOtherID[] otherIDsField;

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
        public XMLOutCheckUpdateDataReferences References
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
        public XMLOutCheckUpdateDataOtherID[] OtherIDs
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
    public partial class XMLOutCheckUpdateDataReferences
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
    public partial class XMLOutCheckUpdateDataOtherID
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
