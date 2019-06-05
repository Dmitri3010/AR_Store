using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArStore.API.Models
{
    public class Product : Base
    {
        public string Name { get; set; }

        public string Model { get; set; }

        public string Cost { get; set; }

        public string Description { get; set; }

        public bool IsRotated { get; set; }

        public int CategoryId { get; set; }

        public string ImageForTarget { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public float Distance { get; set; }

        [Column("Textures")]
        public string JServices
        {
            get => texturesObj != null ? JsonConvert.SerializeObject(texturesObj) : string.Empty;
            set => texturesObj = JsonConvert.DeserializeObject<List<string>>(value ?? string.Empty);
        }

        [NotMapped]
        public List<string> texturesObj { get; set; }

        public virtual Category Category { get; set; }
        
        public string Scene { get; set; }

        

    }
}
