using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArStore.API.Models.API
{
    public class ProductsApiResponce
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "model")]
        public string Model { get; set; }

        [JsonProperty(PropertyName = "cost")]
        public string Cost { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "isRotated")]
        public bool IsRotated { get; set; }

        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "imageForTarget")]
        public string ImageForTarget { get; set; }

        [JsonProperty(PropertyName = "height")]
        public float Height { get; set; }

        [JsonProperty(PropertyName = "width")]
        public float Width { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public float Distance { get; set; }

        [JsonProperty(PropertyName = "textures")]
        public string texturesObj { get; set; }
        
        [JsonProperty(PropertyName = "scene")]
        public  string Scene { get; set; }

    }
}
