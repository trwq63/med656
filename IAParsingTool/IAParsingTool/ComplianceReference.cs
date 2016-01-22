using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAParsingTool
{
    class ComplianceReference
    {
        public string vulnId { get; set; }
        public string ruleId { get; set; }
        public string cat { get; set; }
        public string stigId { get; set; }
        public List<string> iaControls { get; set; }
        
        //public ComplianceReference(string complianceRef)
        //{
        //    try
        //    {
        //        string[] splitRef;
        //        splitRef = complianceRef.Split(',');

        //        vulnId = splitRef.Where(r => r.Contains("Vuln-ID")).FirstOrDefault().Split('|')[1];
        //        ruleId = splitRef.Where(r => r.Contains("Rule-ID")).FirstOrDefault().Split('|')[1];
        //        cat = splitRef.Where(r => r.Contains("CAT")).FirstOrDefault().Split('|')[1];
        //        stigId = splitRef.Where(r => r.Contains("STIG-ID")).FirstOrDefault().Split('|')[1];


        //        IEnumerable<string> temp = splitRef.Where(r => r.Contains("8500.2"));
        //        iaControls = temp.Select(r => r.Split('|')[1]).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        iaControls = null;
               
        //    }


        //}

        public static ComplianceReference MakeComplianceReference(string complianceRef)
        {
            ComplianceReference compRef = new ComplianceReference();
            try
            {
                string[] splitRef;
                splitRef = complianceRef.Split(',');

                compRef.vulnId = splitRef.Where(r => r.Contains("Vuln-ID")).FirstOrDefault().Split('|')[1];
                compRef.ruleId = splitRef.Where(r => r.Contains("Rule-ID")).FirstOrDefault().Split('|')[1];
                compRef.cat = splitRef.Where(r => r.Contains("CAT")).FirstOrDefault().Split('|')[1];
                compRef.stigId = splitRef.Where(r => r.Contains("STIG-ID")).FirstOrDefault().Split('|')[1];
                IEnumerable<string> temp = splitRef.Where(r => r.Contains("8500.2"));
                compRef.iaControls = temp.Select(r => r.Split('|')[1]).ToList();

                return compRef;
            }
            catch (Exception ex)
            {
                compRef = null;
                return compRef;
            }
                
        }


    }
}
