using System;
using System.Collections.Generic;

namespace UAHFitVault.LogicLayer.Models
{
    /// <summary>
    /// This class is used to model the data used to produce a line graph
    /// </summary>
    public class LineGraphModel
    {
        #region Public Properties
        /// <summary>
        /// Name to give the x axis of the graph.
        /// </summary>
        public string XAxisName { get; set; }

        /// <summary>
        /// Name to give the y axis of the graph
        /// </summary>
        public string YAxisName { get; set; }

        /// <summary>
        /// List of all the x axis data points for the line.
        /// </summary>
        public List<DateTime> XAxisData { get; set; }

        /// <summary>
        /// List of all the y axis data points for the line.
        /// </summary>
        public List<double> YAxisData { get; set; }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public LineGraphModel() {

        }

        /// <summary>
        /// Constructor used to initialize object properties at construction.
        /// </summary>
        /// <param name="xAxisName">Name to give the x axis of the graph.</param>
        /// <param name="yAxisName">Name to give the y axis of the graph</param>
        /// <param name="xAxisdata">List of all the x axis data points for the line.</param>
        /// <param name="yAxisData">List of all the y axis data points for the line.</param>
        public LineGraphModel(string xAxisName, string yAxisName, List<DateTime> xAxisdata, List<double> yAxisData) {
            if(IsValid(xAxisName, yAxisName, xAxisdata, yAxisData)) {
                XAxisName = xAxisName;
                YAxisName = yAxisName;
                XAxisData = xAxisdata;
                YAxisData = yAxisData;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Verify the current object properties have valid values.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return IsValid(XAxisName, YAxisName, XAxisData, YAxisData);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Verify the data has valid data values.
        /// </summary>
        /// <param name="xAxisName">Name to give the x axis of the graph.</param>
        /// <param name="yAxisName">Name to give the y axis of the graph</param>
        /// <param name="xAxisdata">List of all the x axis data points for the line.</param>
        /// <param name="yAxisData">List of all the y axis data points for the line.</param>
        /// <returns></returns>
        protected bool IsValid(string xAxisName, string yAxisName, List<DateTime> xAxisdata, List<double> yAxisData) {
            bool valid = false;

            if(!string.IsNullOrEmpty(xAxisName) && !string.IsNullOrEmpty(yAxisName) 
                && xAxisdata != null && xAxisdata.Count > 0 && yAxisData != null && yAxisData.Count > 0){
                valid = true;
            }

            return valid;
        }

        #endregion
    }


}
