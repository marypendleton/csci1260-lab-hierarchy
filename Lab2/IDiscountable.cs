public interface IDiscountable
{
    bool IsOnSale {get;}
    decimal SalePrice();
}