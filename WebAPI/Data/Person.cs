using Newtonsoft.Json;

namespace WebAPI.Data
{
	public class response
	{
        [JsonProperty(PropertyName = "id")] // <-- need to add this mapping to avoid issues
        public string Id { get; set; }  
        public string FirstName { get; set; }
        public string LastName { get; set; }

	}
}