using System;

namespace ArStore.API.Models
{
    public class Order:Base
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public string Adress { get; set; }
        
        public DateTime Date { get; set; }
        
        public int ProductId { get; set; }
        
    }
}