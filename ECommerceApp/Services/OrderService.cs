using ECommerceApp.DTOs.Order;
using ECommerceApp.Models;
using ECommerceApp.Repositories.Interfaces;
using ECommerceApp.Services.Interfaces;

namespace ECommerceApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(PlaceOrderDto dto)
        {
            // 1. User ka cart lao
            var cart = await _cartRepository.GetCartByUserIdAsync(dto.UserId);
            if (cart == null || cart.CartItem.Count == 0)
                throw new ArgumentException("Cart is empty");

            // 2. Stock check karo har product ka
            foreach (var item in cart.CartItem)
            {
                if (item.Product!.Stock < item.Quantity)
                    throw new ArgumentException($"Not enough stock for {item.Product.Name}");
            }

            // 3. Order banao
            var order = new Order
            {
                UserId = dto.UserId,
                FullName = dto.FullName,
                Phone = dto.Phone,
                AddressLine = dto.AddressLine,
                PaymentMethod = dto.PaymentMethod,
                Status = "Pending",
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartItem.Sum(ci => ci.UnitPrice * ci.Quantity)
            };

            // 4. Cart items ko OrderItems mein convert karo
            foreach (var item in cart.CartItem)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product!.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity
                });

                // 5. Product stock kam karo
                item.Product.Stock -= item.Quantity;
            }

            await _orderRepository.AddOrderAsync(order);

            // 6. Cart clear karo (saare cart items remove)
            foreach (var item in cart.CartItem.ToList())
            {
                await _cartRepository.RemoveCartItemAsync(item);
            }

            await _orderRepository.SaveChangesAsync();

            return MapToOrderResponseDto(order);
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapToOrderResponseDto).ToList();
        }

        private OrderResponseDto MapToOrderResponseDto(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.Id,
                UserId = order.UserId,
                FullName = order.FullName,
                Phone = order.Phone,
                AddressLine = order.AddressLine,
                PaymentMethod = order.PaymentMethod,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    ProductId = oi.ProductId,
                    ProductName = oi.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    Subtotal = oi.UnitPrice * oi.Quantity
                }).ToList()
            };
        }
    }
}