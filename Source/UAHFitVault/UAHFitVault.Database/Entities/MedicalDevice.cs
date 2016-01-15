using System.ComponentModel.DataAnnotations;

namespace UAHFitVault..Database.Entities
{
    public class MedicalDevice
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public MedicalDevice() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        #endregion

    }
}
