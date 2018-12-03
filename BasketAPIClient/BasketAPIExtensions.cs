// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Client
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for BasketAPI.
    /// </summary>
    public static partial class BasketAPIExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static void Authenticate(this IBasketAPI operations)
            {
                operations.AuthenticateAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task AuthenticateAsync(this IBasketAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.AuthenticateWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static Basket GetBasketById(this IBasketAPI operations, System.Guid id)
            {
                return operations.GetBasketByIdAsync(id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Basket> GetBasketByIdAsync(this IBasketAPI operations, System.Guid id, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetBasketByIdWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            public static void DeleteBasket(this IBasketAPI operations, System.Guid id)
            {
                operations.DeleteBasketAsync(id).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteBasketAsync(this IBasketAPI operations, System.Guid id, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteBasketWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static Basket PostBasket(this IBasketAPI operations)
            {
                return operations.PostBasketAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Basket> PostBasketAsync(this IBasketAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostBasketWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            public static Item GetItemById(this IBasketAPI operations, System.Guid basketId, System.Guid itemId)
            {
                return operations.GetItemByIdAsync(basketId, itemId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Item> GetItemByIdAsync(this IBasketAPI operations, System.Guid basketId, System.Guid itemId, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetItemByIdWithHttpMessagesAsync(basketId, itemId, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            public static void DeleteItem(this IBasketAPI operations, System.Guid basketId, System.Guid itemId)
            {
                operations.DeleteItemAsync(basketId, itemId).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteItemAsync(this IBasketAPI operations, System.Guid basketId, System.Guid itemId, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteItemWithHttpMessagesAsync(basketId, itemId, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            /// <param name='updateItem'>
            /// </param>
            public static Item PatchItem(this IBasketAPI operations, System.Guid basketId, System.Guid itemId, UpdateItem updateItem = default(UpdateItem))
            {
                return operations.PatchItemAsync(basketId, itemId, updateItem).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='itemId'>
            /// </param>
            /// <param name='updateItem'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Item> PatchItemAsync(this IBasketAPI operations, System.Guid basketId, System.Guid itemId, UpdateItem updateItem = default(UpdateItem), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PatchItemWithHttpMessagesAsync(basketId, itemId, updateItem, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='request'>
            /// </param>
            public static Item PostItem(this IBasketAPI operations, System.Guid basketId, AddItem request = default(AddItem))
            {
                return operations.PostItemAsync(basketId, request).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='basketId'>
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<Item> PostItemAsync(this IBasketAPI operations, System.Guid basketId, AddItem request = default(AddItem), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.PostItemWithHttpMessagesAsync(basketId, request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
