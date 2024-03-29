﻿using System.ComponentModel;

namespace Project1.WebApp.Models
{
    public class Order
    {
        [DisplayName("Order ID")]
        public string Id { get; set; }

        [DisplayName("Location")]
        public string Location { get; set; }

        [DisplayName("Customer")]
        public string Customer { get; set; }

        [DisplayName("Order Time")]
        public string OrderTime { get; set; }
    }
}
