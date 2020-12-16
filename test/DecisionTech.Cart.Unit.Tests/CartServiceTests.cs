using System.Linq;
using Xunit;

namespace DecisionTech.Cart.Unit.Tests
{
    public class CartServiceTests
    {
        [Fact]
        public void WhenGettingShouldReturnCart()
        {
            var sut = new CartService();
            var cart = sut.Get();
            Assert.NotNull(cart);
        }

        [Fact]
        public void WhenGettingShouldReturnCartItems()
        {
            var sut = new CartService();
            var cart = sut.Get();
            Assert.Empty(cart.Items);
        }

        [Fact]
        public void WhenGettingShouldReturnCartSubTotal()
        {
            var sut = new CartService();
            var cart = sut.Get();
            Assert.Equal(0, cart.SubTotal);
        }

        [Fact]
        public void WhenGettingShouldReturnCartDiscount()
        {
            var sut = new CartService();
            var cart = sut.Get();
            Assert.Equal(0, cart.Discount);
        }

        [Fact]
        public void WhenGettingShouldReturnCartTotal()
        {
            var sut = new CartService();
            var cart = sut.Get();
            Assert.Equal(0, cart.Total);
        }

        [Fact]
        public void WhenAddingGivenNoRequestShouldReturnFailure()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, null);
            Assert.False(result.Success);
        }

        [Fact]
        public void WhenAddingGivenNoRequestShouldReturnError()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, null);
            Assert.Contains("The ProductId field is required.", result.Errors);
        }

        [Fact]
        public void WhenAddingGivenEmptyRequestShouldReturnFailure()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest());
            Assert.False(result.Success);
        }

        [Fact]
        public void WhenAddingGivenEmptyRequestShouldReturnError()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest());
            Assert.Contains("The ProductId field is required.", result.Errors);
        }

        [Fact]
        public void WhenAddingGivenRequestShouldReturnSuccess()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            Assert.True(result.Success);
        }

        [Fact]
        public void WhenAddingGivenRequestShouldReturnCartWithItem()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            var dto = result.Model;
            Assert.NotEmpty(dto.Items);
        }

        [Fact]
        public void WhenAddingGivenRequestShouldReturnCartWithItemName()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            var dto = result.Model;
            var items = dto.Items.Select(x => x.ProductName);
            Assert.Contains("Butter", items);
        }

        [Fact]
        public void WhenAddingGivenRequestShouldReturnCartWithItemCost()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            var dto = result.Model;
            var item = dto.Items.FirstOrDefault();
            Assert.Equal(0.8M, item.Cost);
        }

        [Fact]
        public void WhenAddingGivenRequestShouldReturnCartWithItemQuantity()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            var dto = result.Model;
            var item = dto.Items.FirstOrDefault();
            Assert.Equal(1, item.Quantity);
        }

        [Fact]
        public void WhenAddingGivenButterAndMilkAndBreadRequestShouldReturnSuccess()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var butterResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            var milkResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2 });
            var breadResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            Assert.True(butterResult.Success && milkResult.Success && breadResult.Success);
        }

        [Fact]
        public void WhenAddingGivenButterAndMilkAndBreadRequestShouldReturnCartWithItemSubTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(2.95M, dto.SubTotal);
        }

        [Fact]
        public void WhenAddingGivenButterAndMilkAndBreadRequestShouldReturnCartWithItemDiscount()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(0M, dto.Discount);
        }

        [Fact]
        public void WhenAddingGivenButterAndMilkAndBreadRequestShouldReturnCartWithItemTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(2.95M, dto.Total);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndBreadRequestShouldReturnSuccess()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var butterResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            var breadResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            Assert.True(butterResult.Success && breadResult.Success);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndBreadRequestShouldReturnCartWithItemSubTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(2.6M, dto.SubTotal);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndBreadRequestShouldReturnCartWithItemDiscount()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(.5M, dto.Discount);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndBreadRequestShouldReturnCartWithItemTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(2.1M, dto.Total);
        }

        [Fact]
        public void WhenAddingGivenFourMilkRequestShouldReturnSuccess()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 4 });
            Assert.True(result.Success);
        }

        [Fact]
        public void WhenAddingGivenFourMilkRequestShouldReturnCartWithItemSubTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 4 });
            var dto = result.Model;
            Assert.Equal(4.6M, dto.SubTotal);
        }

        [Fact]
        public void WhenAddingGivenFourMilkRequestShouldReturnCartWithItemDiscount()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 4 });
            var dto = result.Model;
            Assert.Equal(1.15M, dto.Discount);
        }

        [Fact]
        public void WhenAddingGivenFourMilkRequestShouldReturnCartWithItemTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 4 });
            var dto = result.Model;
            Assert.Equal(3.45M, dto.Total);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndEightMilkAndBreadRequestShouldReturnSuccess()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            var butterResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            var milkResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 8 });
            var breadResult = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            Assert.True(butterResult.Success && milkResult.Success && breadResult.Success);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndEightMilkAndBreadRequestShouldReturnCartWithItemSubTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 8 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(11.8M, dto.SubTotal);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndEightMilkAndBreadRequestShouldReturnCartWithItemDiscount()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 8 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(2.8M, dto.Discount);
        }

        [Fact]
        public void WhenAddingGivenTwoButterAndEightMilkAndBreadRequestShouldReturnCartWithItemTotal()
        {
            var sut = new CartService();
            var cart = sut.GetModel();

            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 1, Quantity = 2 });
            _ = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 2, Quantity = 8 });
            var result = sut.AddItem(cart, new Dtos.CartRequest { ProductId = 3 });
            var dto = result.Model;
            Assert.Equal(9M, dto.Total);
        }
    }
}