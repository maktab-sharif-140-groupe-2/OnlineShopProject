
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Excepitons;

namespace OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;

public class Product : BaseEntity
{
    public Product(string name, string brandName, decimal price, int stock,Guid? createrId=null)
    {
        Name = name;
        BrandName = brandName;
        Price = price;
        Stock = stock;
        CreaterId= createrId;
    }

    public string Name { get; private set; }=string.Empty;

    public string BrandName { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Stock { get; private set; }

    public void ChangeStock(int request,Guid modifierId)
    {
        if (Stock + request < 0)
            throw new ProcessingStockException("request count is higher than totla stock in wearhouse");

        if(request==0)
            throw new ProcessingStockException("request can't be zero");
        Stock=+request;
        ModifiederId = modifierId;
    }
    public void ChangePrice(decimal newPrice, Guid modifierId)
    {
        if (newPrice <= 0)
            throw new InputPriceException("price can't be zero or negative");
        Price = newPrice;
        ModifiederId = modifierId;
    }
}
