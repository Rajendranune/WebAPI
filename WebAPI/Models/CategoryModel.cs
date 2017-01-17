using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CategoryModel
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }

    }
    public class SubCategoryModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Categoryid { get; set; }
    }
    public class ProductModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string SubCategoryid { get; set; }
    }
}