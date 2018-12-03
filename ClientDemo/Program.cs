using System;
using Client;
using Client.Models;
using Microsoft.Rest;

namespace ClientDemo
{
    class Program
    {
        static readonly Uri ApiUri = new Uri("https://basketapi-dev-as.azurewebsites.net");

        static void Main(string[] args)
        {
            Console.WriteLine($"Environment: {ApiUri}");
            var client = CreateAuthenticatedClient();

            var basket = client.PostBasket();
            Console.WriteLine($"Created basket: {basket.Id}");

            var item = client.PostItem(basket.Id.Value, new AddItem {ItemId = Guid.NewGuid(), Quantity = 5});
            Console.WriteLine($"Added {item.ItemId} to basket {item.Quantity} times");

            client.PatchItem(basket.Id.Value, item.ItemId.Value, new UpdateItem(10));
            Console.WriteLine($"Updated {item.ItemId} quantity to {item.Quantity}");

            client.DeleteItem(basket.Id.Value, item.ItemId.Value);
            Console.WriteLine($"Deleted {item.ItemId} from basket");

            client.DeleteBasket(basket.Id.Value);
            Console.WriteLine($"Deleted basket {basket.Id.Value}");

            Console.ReadKey();
        }

        private static BasketAPI CreateAuthenticatedClient()
        {
            // A bit hacky as it seems Autorest is difficult to use with JWT? Not happy with this.
            var unauthenticatedClient = new BasketAPI(ApiUri, new BasicAuthenticationCredentials());
            var authenticationResponse = unauthenticatedClient.Authenticate();
            Console.WriteLine($"Authentication token: {authenticationResponse.Token}");
            return new BasketAPI(ApiUri, new TokenCredentials(authenticationResponse.Token));
        }
    }
}
