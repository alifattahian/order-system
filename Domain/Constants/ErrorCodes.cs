﻿namespace Domain.Constants
{
    public record ErrorCode(string Code, string Description);
    public static class ErrorCodes
    {
        public static readonly ErrorCode CustomerNotEmpty = new ErrorCode("Domain-1", "customer is not set!");
        public static readonly ErrorCode NoItemIsAddedToOrder = new ErrorCode("Domain-2", "no item is added to order!");
        public static readonly ErrorCode ProductShouldHavePriceForSales = new ErrorCode("Domain-3", "product should have a price in order to sales!");
        public static readonly ErrorCode InvalidRange = new ErrorCode("Domain-4", "Invalid range is provided!");
    }
}
