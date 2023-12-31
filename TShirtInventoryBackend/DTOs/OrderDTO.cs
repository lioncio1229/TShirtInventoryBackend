﻿using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string PaymentMethod { get; set; }
        public CustomerDTO Customer { get; set; }
    }
}
