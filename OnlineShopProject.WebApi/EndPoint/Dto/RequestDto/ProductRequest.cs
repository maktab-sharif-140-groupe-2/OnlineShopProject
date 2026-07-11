using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class ProductRequest
    {
        [Required(ErrorMessage ="Product Name Is Required")]
        [MaxLength(100,ErrorMessage = "Max Length Product  Name Is 100 ")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand Name Of Product Is Required")]
        [MaxLength(100, ErrorMessage = "Max Length Brand Name Of Product Is 100 ")]
        public string BrandName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product Stock Amount Is Required")]
        [Display(Name = "Stock Of Product")]
        [Range(1,1000000,ErrorMessage = "Min Initialization Stock Is 1 and max is 1_000_000_000")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Product Price Amount Is Required")]
        [Range(1,Int32.MaxValue,ErrorMessage ="Price of product can't be negative")]
        public decimal Price { get; set; } 


        public ProductCommand ToCommand()
        {
           return new ProductCommand()
            {
                Stock=Stock,
                BrandName=BrandName, 
                Price=Price,
                Name=Name
            };
        }

    }
}
