using System.Data;

namespace OnlineShopProject.WebApi.Domain.Common.Paginations;

public class Pagination<T>
{
    public List<T> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPageCount { get; set; }


    public static Pagination<T> GetPagination(List<T> data, int pageNumber, int pageSize,int totalcountData)
    {
        totalcountData= pageSize>totalcountData ? pageSize : totalcountData+pageSize;
        return new Pagination<T>()
        {
            Items = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPageCount = totalcountData / pageSize
        };
    }

}
