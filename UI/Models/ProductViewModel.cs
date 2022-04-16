using BusinessObjects.Model;

namespace UI.Models
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public IEnumerable<Product> CurrentPageProducts { get; set; }

    }
}