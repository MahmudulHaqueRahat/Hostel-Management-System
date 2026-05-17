using System;

namespace BLL.DTOs
{
    public class BillDTO
    {
        public int BillId { get; set; }

        public int ResidentId { get; set; }

        public string BillingMonth { get; set; }

        public decimal RoomRent { get; set; }

        public decimal MealCharge { get; set; }

        public decimal UtilityCharge { get; set; }

        public decimal PreviousDue { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DueAmount { get; set; }

        public DateTime? GeneratedDate { get; set; }

        public string Status { get; set; }

        public string ResidentName { get; set; }
    }
}