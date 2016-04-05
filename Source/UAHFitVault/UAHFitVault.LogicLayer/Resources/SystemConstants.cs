namespace UAHFitVault.LogicLayer.Resources
{
    /// <summary>
    /// Integer Constants to be used throughout the solution
    /// </summary>
    public class SystemConstants
    {
        /// <summary>
        /// Maxiumum number of items that can be request from SQL at a time.
        /// This is used to prevent memory exceptions when retreiving data.
        /// </summary>
        public const int MAX_ITEMS_RETURNED = 150000;
    }
}
