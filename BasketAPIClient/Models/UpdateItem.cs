// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class UpdateItem
    {
        /// <summary>
        /// Initializes a new instance of the UpdateItem class.
        /// </summary>
        public UpdateItem()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the UpdateItem class.
        /// </summary>
        public UpdateItem(int? quantity = default(int?))
        {
            Quantity = quantity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int? Quantity { get; set; }

    }
}