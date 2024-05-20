using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.DTO.Purchase
{
    public class PurchaseInfoDto
    {
        public int Id { get; set; }

        public DateTime Purchase_Date { get; set;}

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductType { get; set; } = string.Empty;

        public int ProductPrice { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set;}        
    }
}