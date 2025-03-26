using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace _22806012222_PhamNgocHuy_S3_B3.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
