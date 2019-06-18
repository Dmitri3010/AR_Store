using System;
using System.Collections.Generic;
using ArStore.API.Tools;

namespace ArStore.API.Models.API
{
    public class OrderApiRequest
    {
        public string Name { get; set; }

        //[ValidationPattern(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
        public string Phone { get; set; }

        public string Adress { get; set; }

        [ValidationPattern(@"\w+@\w+[.]\w+")]
        public string Email { get; set; }

        public bool IsDelivery { get; set; }

        public string MainImage { get; set; }

        public string SecondImage { get; set; }

        public DateTime Date { get; set; }
        
        public string Comment { get; set; }
        
        public int ProductId { get; set; }
        
        public List<Product> Products { get; set; }

        
    }
}