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
    public partial class NessusClientData_v2
    {

        private NessusClientData_v2Policy policyField;

        private NessusClientData_v2Report reportField;

        /// <remarks/>
        public NessusClientData_v2Policy Policy
        {
            get
            {
                return this.policyField;
            }
            set
            {
                this.policyField = value;
            }
        }

        /// <remarks/>
        public NessusClientData_v2Report Report
        {
            get
            {
                return this.reportField;
            }
            set
            {
                this.reportField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2Policy
    {

        private string policyNameField;

        private object policyCommentsField;

        private NessusClientData_v2PolicyPreferences preferencesField;

        private NessusClientData_v2PolicyFamilyItem[] familySelectionField;

        private NessusClientData_v2PolicyPluginItem[] individualPluginSelectionField;

        /// <remarks/>
        public string policyName
        {
            get
            {
                return this.policyNameField;
            }
            set
            {
                this.policyNameField = value;
            }
        }

        /// <remarks/>
        public object policyComments
        {
            get
            {
                return this.policyCommentsField;
            }
            set
            {
                this.policyCommentsField = value;
            }
        }

        /// <remarks/>
        public NessusClientData_v2PolicyPreferences Preferences
        {
            get
            {
                return this.preferencesField;
            }
            set
            {
                this.preferencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FamilyItem", IsNullable = false)]
        public NessusClientData_v2PolicyFamilyItem[] FamilySelection
        {
            get
            {
                return this.familySelectionField;
            }
            set
            {
                this.familySelectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("PluginItem", IsNullable = false)]
        public NessusClientData_v2PolicyPluginItem[] IndividualPluginSelection
        {
            get
            {
                return this.individualPluginSelectionField;
            }
            set
            {
                this.individualPluginSelectionField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2PolicyPreferences
    {

        private NessusClientData_v2PolicyPreferencesPreference[] serverPreferencesField;

        private NessusClientData_v2PolicyPreferencesItem[] pluginsPreferencesField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("preference", IsNullable = false)]
        public NessusClientData_v2PolicyPreferencesPreference[] ServerPreferences
        {
            get
            {
                return this.serverPreferencesField;
            }
            set
            {
                this.serverPreferencesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
        public NessusClientData_v2PolicyPreferencesItem[] PluginsPreferences
        {
            get
            {
                return this.pluginsPreferencesField;
            }
            set
            {
                this.pluginsPreferencesField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2PolicyPreferencesPreference
    {

        private string nameField;

        private string valueField;

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
        public string value
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
    public partial class NessusClientData_v2PolicyPreferencesItem
    {

        private string pluginNameField;

        private uint pluginIdField;

        private string fullNameField;

        private string preferenceNameField;

        private string preferenceTypeField;

        private string preferenceValuesField;

        private string selectedValueField;

        /// <remarks/>
        public string pluginName
        {
            get
            {
                return this.pluginNameField;
            }
            set
            {
                this.pluginNameField = value;
            }
        }

        /// <remarks/>
        public uint pluginId
        {
            get
            {
                return this.pluginIdField;
            }
            set
            {
                this.pluginIdField = value;
            }
        }

        /// <remarks/>
        public string fullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }

        /// <remarks/>
        public string preferenceName
        {
            get
            {
                return this.preferenceNameField;
            }
            set
            {
                this.preferenceNameField = value;
            }
        }

        /// <remarks/>
        public string preferenceType
        {
            get
            {
                return this.preferenceTypeField;
            }
            set
            {
                this.preferenceTypeField = value;
            }
        }

        /// <remarks/>
        public string preferenceValues
        {
            get
            {
                return this.preferenceValuesField;
            }
            set
            {
                this.preferenceValuesField = value;
            }
        }

        /// <remarks/>
        public string selectedValue
        {
            get
            {
                return this.selectedValueField;
            }
            set
            {
                this.selectedValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2PolicyFamilyItem
    {

        private string familyNameField;

        private string statusField;

        /// <remarks/>
        public string FamilyName
        {
            get
            {
                return this.familyNameField;
            }
            set
            {
                this.familyNameField = value;
            }
        }

        /// <remarks/>
        public string Status
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2PolicyPluginItem
    {

        private ushort pluginIdField;

        private string pluginNameField;

        private string familyField;

        private string statusField;

        /// <remarks/>
        public ushort PluginId
        {
            get
            {
                return this.pluginIdField;
            }
            set
            {
                this.pluginIdField = value;
            }
        }

        /// <remarks/>
        public string PluginName
        {
            get
            {
                return this.pluginNameField;
            }
            set
            {
                this.pluginNameField = value;
            }
        }

        /// <remarks/>
        public string Family
        {
            get
            {
                return this.familyField;
            }
            set
            {
                this.familyField = value;
            }
        }

        /// <remarks/>
        public string Status
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
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2Report
    {

        private NessusClientData_v2ReportReportHost reportHostField;

        private string nameField;

        /// <remarks/>
        public NessusClientData_v2ReportReportHost ReportHost
        {
            get
            {
                return this.reportHostField;
            }
            set
            {
                this.reportHostField = value;
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
    public partial class NessusClientData_v2ReportReportHost
    {

        private NessusClientData_v2ReportReportHostTag[] hostPropertiesField;

        private NessusClientData_v2ReportReportHostReportItem[] reportItemField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("tag", IsNullable = false)]
        public NessusClientData_v2ReportReportHostTag[] HostProperties
        {
            get
            {
                return this.hostPropertiesField;
            }
            set
            {
                this.hostPropertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReportItem")]
        public NessusClientData_v2ReportReportHostReportItem[] ReportItem
        {
            get
            {
                return this.reportItemField;
            }
            set
            {
                this.reportItemField = value;
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
    public partial class NessusClientData_v2ReportReportHostTag
    {

        private string nameField;

        private string valueField;

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
    public partial class NessusClientData_v2ReportReportHostReportItem
    {

        private uint[] bidField;

        private string canvas_packageField;

        private uint certField;

        private bool certFieldSpecified;

        private string cpeField;

        private string[] cveField;

        private byte cweField;

        private bool cweFieldSpecified;

        private decimal cvss_base_scoreField;

        private bool cvss_base_scoreFieldSpecified;

        private decimal cvss_temporal_scoreField;

        private bool cvss_temporal_scoreFieldSpecified;

        private string cvss_temporal_vectorField;

        private string cvss_vectorField;

        private string descriptionField;

        private ushort[] edbidField;

        private bool exploit_availableField;

        private bool exploit_availableFieldSpecified;

        private bool exploit_framework_canvasField;

        private bool exploit_framework_canvasFieldSpecified;

        private bool exploit_framework_coreField;

        private bool exploit_framework_coreFieldSpecified;

        private bool exploit_framework_metasploitField;

        private bool exploit_framework_metasploitFieldSpecified;

        private string exploitability_easeField;

        private bool exploited_by_malwareField;

        private bool exploited_by_malwareFieldSpecified;

        private string fnameField;

        private string iavbField;

        private string iavaField;

        private string icsaField;

        private string metasploit_nameField;

        private string msftField;

        private uint[] osvdbField;

        private string patch_publication_dateField;

        private string plugin_modification_dateField;

        private string plugin_nameField;

        private string plugin_publication_dateField;

        private string plugin_typeField;

        private string risk_factorField;

        private string script_versionField;

        private string see_alsoField;

        private string solutionField;

        private string stig_severityField;

        private string synopsisField;

        private string vuln_publication_dateField;

        private string[] xrefField;

        private string plugin_outputField;

        private NessusClientData_v2ReportReportHostReportItemAttachment attachmentField;

        private ushort portField;

        private string svc_nameField;

        private string protocolField;

        private byte severityField;

        private uint pluginIDField;

        private string pluginNameField;

        private string pluginFamilyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("bid")]
        public uint[] bid
        {
            get
            {
                return this.bidField;
            }
            set
            {
                this.bidField = value;
            }
        }

        /// <remarks/>
        public string canvas_package
        {
            get
            {
                return this.canvas_packageField;
            }
            set
            {
                this.canvas_packageField = value;
            }
        }

        /// <remarks/>
        public uint cert
        {
            get
            {
                return this.certField;
            }
            set
            {
                this.certField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool certSpecified
        {
            get
            {
                return this.certFieldSpecified;
            }
            set
            {
                this.certFieldSpecified = value;
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
        [System.Xml.Serialization.XmlElementAttribute("cve")]
        public string[] cve
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
        public byte cwe
        {
            get
            {
                return this.cweField;
            }
            set
            {
                this.cweField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cweSpecified
        {
            get
            {
                return this.cweFieldSpecified;
            }
            set
            {
                this.cweFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal cvss_base_score
        {
            get
            {
                return this.cvss_base_scoreField;
            }
            set
            {
                this.cvss_base_scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cvss_base_scoreSpecified
        {
            get
            {
                return this.cvss_base_scoreFieldSpecified;
            }
            set
            {
                this.cvss_base_scoreFieldSpecified = value;
            }
        }

        /// <remarks/>
        public decimal cvss_temporal_score
        {
            get
            {
                return this.cvss_temporal_scoreField;
            }
            set
            {
                this.cvss_temporal_scoreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cvss_temporal_scoreSpecified
        {
            get
            {
                return this.cvss_temporal_scoreFieldSpecified;
            }
            set
            {
                this.cvss_temporal_scoreFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string cvss_temporal_vector
        {
            get
            {
                return this.cvss_temporal_vectorField;
            }
            set
            {
                this.cvss_temporal_vectorField = value;
            }
        }

        /// <remarks/>
        public string cvss_vector
        {
            get
            {
                return this.cvss_vectorField;
            }
            set
            {
                this.cvss_vectorField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("edb-id")]
        public ushort[] edbid
        {
            get
            {
                return this.edbidField;
            }
            set
            {
                this.edbidField = value;
            }
        }

        /// <remarks/>
        public bool exploit_available
        {
            get
            {
                return this.exploit_availableField;
            }
            set
            {
                this.exploit_availableField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool exploit_availableSpecified
        {
            get
            {
                return this.exploit_availableFieldSpecified;
            }
            set
            {
                this.exploit_availableFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool exploit_framework_canvas
        {
            get
            {
                return this.exploit_framework_canvasField;
            }
            set
            {
                this.exploit_framework_canvasField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool exploit_framework_canvasSpecified
        {
            get
            {
                return this.exploit_framework_canvasFieldSpecified;
            }
            set
            {
                this.exploit_framework_canvasFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool exploit_framework_core
        {
            get
            {
                return this.exploit_framework_coreField;
            }
            set
            {
                this.exploit_framework_coreField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool exploit_framework_coreSpecified
        {
            get
            {
                return this.exploit_framework_coreFieldSpecified;
            }
            set
            {
                this.exploit_framework_coreFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool exploit_framework_metasploit
        {
            get
            {
                return this.exploit_framework_metasploitField;
            }
            set
            {
                this.exploit_framework_metasploitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool exploit_framework_metasploitSpecified
        {
            get
            {
                return this.exploit_framework_metasploitFieldSpecified;
            }
            set
            {
                this.exploit_framework_metasploitFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string exploitability_ease
        {
            get
            {
                return this.exploitability_easeField;
            }
            set
            {
                this.exploitability_easeField = value;
            }
        }

        /// <remarks/>
        public bool exploited_by_malware
        {
            get
            {
                return this.exploited_by_malwareField;
            }
            set
            {
                this.exploited_by_malwareField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool exploited_by_malwareSpecified
        {
            get
            {
                return this.exploited_by_malwareFieldSpecified;
            }
            set
            {
                this.exploited_by_malwareFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string fname
        {
            get
            {
                return this.fnameField;
            }
            set
            {
                this.fnameField = value;
            }
        }

        /// <remarks/>
        public string iavb
        {
            get
            {
                return this.iavbField;
            }
            set
            {
                this.iavbField = value;
            }
        }

        /// <remarks/>
        public string iava
        {
            get
            {
                return this.iavaField;
            }
            set
            {
                this.iavaField = value;
            }
        }

        /// <remarks/>
        public string icsa
        {
            get
            {
                return this.icsaField;
            }
            set
            {
                this.icsaField = value;
            }
        }

        /// <remarks/>
        public string metasploit_name
        {
            get
            {
                return this.metasploit_nameField;
            }
            set
            {
                this.metasploit_nameField = value;
            }
        }

        /// <remarks/>
        public string msft
        {
            get
            {
                return this.msftField;
            }
            set
            {
                this.msftField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("osvdb")]
        public uint[] osvdb
        {
            get
            {
                return this.osvdbField;
            }
            set
            {
                this.osvdbField = value;
            }
        }

        /// <remarks/>
        public string patch_publication_date
        {
            get
            {
                return this.patch_publication_dateField;
            }
            set
            {
                this.patch_publication_dateField = value;
            }
        }

        /// <remarks/>
        public string plugin_modification_date
        {
            get
            {
                return this.plugin_modification_dateField;
            }
            set
            {
                this.plugin_modification_dateField = value;
            }
        }

        /// <remarks/>
        public string plugin_name
        {
            get
            {
                return this.plugin_nameField;
            }
            set
            {
                this.plugin_nameField = value;
            }
        }

        /// <remarks/>
        public string plugin_publication_date
        {
            get
            {
                return this.plugin_publication_dateField;
            }
            set
            {
                this.plugin_publication_dateField = value;
            }
        }

        /// <remarks/>
        public string plugin_type
        {
            get
            {
                return this.plugin_typeField;
            }
            set
            {
                this.plugin_typeField = value;
            }
        }

        /// <remarks/>
        public string risk_factor
        {
            get
            {
                return this.risk_factorField;
            }
            set
            {
                this.risk_factorField = value;
            }
        }

        /// <remarks/>
        public string script_version
        {
            get
            {
                return this.script_versionField;
            }
            set
            {
                this.script_versionField = value;
            }
        }

        /// <remarks/>
        public string see_also
        {
            get
            {
                return this.see_alsoField;
            }
            set
            {
                this.see_alsoField = value;
            }
        }

        /// <remarks/>
        public string solution
        {
            get
            {
                return this.solutionField;
            }
            set
            {
                this.solutionField = value;
            }
        }

        /// <remarks/>
        public string stig_severity
        {
            get
            {
                return this.stig_severityField;
            }
            set
            {
                this.stig_severityField = value;
            }
        }

        /// <remarks/>
        public string synopsis
        {
            get
            {
                return this.synopsisField;
            }
            set
            {
                this.synopsisField = value;
            }
        }

        /// <remarks/>
        public string vuln_publication_date
        {
            get
            {
                return this.vuln_publication_dateField;
            }
            set
            {
                this.vuln_publication_dateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("xref")]
        public string[] xref
        {
            get
            {
                return this.xrefField;
            }
            set
            {
                this.xrefField = value;
            }
        }

        /// <remarks/>
        public string plugin_output
        {
            get
            {
                return this.plugin_outputField;
            }
            set
            {
                this.plugin_outputField = value;
            }
        }

        /// <remarks/>
        public NessusClientData_v2ReportReportHostReportItemAttachment attachment
        {
            get
            {
                return this.attachmentField;
            }
            set
            {
                this.attachmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort port
        {
            get
            {
                return this.portField;
            }
            set
            {
                this.portField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string svc_name
        {
            get
            {
                return this.svc_nameField;
            }
            set
            {
                this.svc_nameField = value;
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
        public byte severity
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
        public uint pluginID
        {
            get
            {
                return this.pluginIDField;
            }
            set
            {
                this.pluginIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pluginName
        {
            get
            {
                return this.pluginNameField;
            }
            set
            {
                this.pluginNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pluginFamily
        {
            get
            {
                return this.pluginFamilyField;
            }
            set
            {
                this.pluginFamilyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusClientData_v2ReportReportHostReportItemAttachment
    {

        private string nameField;

        private string typeField;

        private string valueField;

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


}
  