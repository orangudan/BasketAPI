// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace BasketAPIClient.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class AddItem
    {
        /// <summary>
        /// Initializes a new instance of the AddItem class.
        /// </summary>
        public AddItem()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AddItem class.
        /// </summary>
        public AddItem(System.Guid? itemId = default(System.Guid?), int? quantity = default(int?))
        {
            ItemId = itemId;
            Quantity = quantity;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "itemId")]
        public System.Guid? ItemId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int? Quantity { get; set; }

    }
}
