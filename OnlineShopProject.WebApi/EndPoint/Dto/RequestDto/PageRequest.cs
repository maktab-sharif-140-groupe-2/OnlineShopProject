using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class PageRequest
    {
        [Range(0, int.MaxValue)]
        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [Range(1,50,ErrorMessage ="Max page size is 50")]
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}

