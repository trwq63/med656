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
    public partial class CHECKLIST
    {

        private string sV_VERSIONField;

        private CHECKLISTASSET aSSETField;

        private CHECKLISTSTIG_INFO sTIG_INFOField;

        private CHECKLISTVULN[] vULNField;

        /// <remarks/>
        public string SV_VERSION
        {
            get
            {
                return this.sV_VERSIONField;
            }
            set
            {
                this.sV_VERSIONField = value;
            }
        }

        /// <remarks/>
        public CHECKLISTASSET ASSET
        {
            get
            {
                return this.aSSETField;
            }
            set
            {
                this.aSSETField = value;
            }
        }

        /// <remarks/>
        public CHECKLISTSTIG_INFO STIG_INFO
        {
            get
            {
                return this.sTIG_INFOField;
            }
            set
            {
                this.sTIG_INFOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("VULN")]
        public CHECKLISTVULN[] VULN
        {
            get
            {
                return this.vULNField;
            }
            set
            {
                this.vULNField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CHECKLISTASSET
    {

        private string aSSET_TYPEField;

        private string hOST_NAMEField;

        private string hOST_IPField;

        private string hOST_MACField;

        private object hOST_GUIDField;

        private object tARGET_KEYField;

        private CHECKLISTASSETASSET_VAL aSSET_VALField;

        /// <remarks/>
        public string ASSET_TYPE
        {
            get
            {
                return this.aSSET_TYPEField;
            }
            set
            {
                this.aSSET_TYPEField = value;
            }
        }

        /// <remarks/>
        public string HOST_NAME
        {
            get
            {
                return this.hOST_NAMEField;
            }
            set
            {
                this.hOST_NAMEField = value;
            }
        }

        /// <remarks/>
        public string HOST_IP
        {
            get
            {
                return this.hOST_IPField;
            }
            set
            {
                this.hOST_IPField = value;
            }
        }

        /// <remarks/>
        public string HOST_MAC
        {
            get
            {
                return this.hOST_MACField;
            }
            set
            {
                this.hOST_MACField = value;
            }
        }

        /// <remarks/>
        public object HOST_GUID
        {
            get
            {
                return this.hOST_GUIDField;
            }
            set
            {
                this.hOST_GUIDField = value;
            }
        }

        /// <remarks/>
        public object TARGET_KEY
        {
            get
            {
                return this.tARGET_KEYField;
            }
            set
            {
                this.tARGET_KEYField = value;
            }
        }

        /// <remarks/>
        public CHECKLISTASSETASSET_VAL ASSET_VAL
        {
            get
            {
                return this.aSSET_VALField;
            }
            set
            {
                this.aSSET_VALField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CHECKLISTASSETASSET_VAL
    {

        private string aV_NAMEField;

        private string aV_DATAField;

        /// <remarks/>
        public string AV_NAME
        {
            get
            {
                return this.aV_NAMEField;
            }
            set
            {
                this.aV_NAMEField = value;
            }
        }

        /// <remarks/>
        public string AV_DATA
        {
            get
            {
                return this.aV_DATAField;
            }
            set
            {
                this.aV_DATAField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CHECKLISTSTIG_INFO
    {

        private string sTIG_TITLEField;

        /// <remarks/>
        public string STIG_TITLE
        {
            get
            {
                return this.sTIG_TITLEField;
            }
            set
            {
                this.sTIG_TITLEField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CHECKLISTVULN
    {

        private CHECKLISTVULNSTIG_DATA[] sTIG_DATAField;

        private string sTATUSField;

        private string fINDING_DETAILSField;

        private string cOMMENTSField;

        private object sEVERITY_OVERRIDEField;

        private object sEVERITY_JUSTIFICATIONField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("STIG_DATA")]
        public CHECKLISTVULNSTIG_DATA[] STIG_DATA
        {
            get
            {
                return this.sTIG_DATAField;
            }
            set
            {
                this.sTIG_DATAField = value;
            }
        }

        /// <remarks/>
        public string STATUS
        {
            get
            {
                return this.sTATUSField;
            }
            set
            {
                this.sTATUSField = value;
            }
        }

        /// <remarks/>
        public string FINDING_DETAILS
        {
            get
            {
                return this.fINDING_DETAILSField;
            }
            set
            {
                this.fINDING_DETAILSField = value;
            }
        }

        /// <remarks/>
        public string COMMENTS
        {
            get
            {
                return this.cOMMENTSField;
            }
            set
            {
                this.cOMMENTSField = value;
            }
        }

        /// <remarks/>
        public object SEVERITY_OVERRIDE
        {
            get
            {
                return this.sEVERITY_OVERRIDEField;
            }
            set
            {
                this.sEVERITY_OVERRIDEField = value;
            }
        }

        /// <remarks/>
        public object SEVERITY_JUSTIFICATION
        {
            get
            {
                return this.sEVERITY_JUSTIFICATIONField;
            }
            set
            {
                this.sEVERITY_JUSTIFICATIONField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class CHECKLISTVULNSTIG_DATA
    {

        private string vULN_ATTRIBUTEField;

        private string aTTRIBUTE_DATAField;

        /// <remarks/>
        public string VULN_ATTRIBUTE
        {
            get
            {
                return this.vULN_ATTRIBUTEField;
            }
            set
            {
                this.vULN_ATTRIBUTEField = value;
            }
        }

        /// <remarks/>
        public string ATTRIBUTE_DATA
        {
            get
            {
                return this.aTTRIBUTE_DATAField;
            }
            set
            {
                this.aTTRIBUTE_DATAField = value;
            }
        }
    }


}
