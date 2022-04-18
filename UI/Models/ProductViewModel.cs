using BusinessObjects.Model;

namespace UI.Models
{
    /// <summary>
    /// Product view model, which is used in views
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// List of products
        /// </summary>
        public IEnumerable<Product> Products { get; set; }
        /// <summary>
        /// PageViewModel object to enable pagination
        /// </summary>
        public PageViewModel PageViewModel { get; set; }
        /// <summary>
        /// List of products on current page
        /// </summary>
        public IEnumerable<Product> CurrentPageProducts { get; set; }

    }
}