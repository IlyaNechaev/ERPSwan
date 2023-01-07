namespace ES.Web.Contracts.V1;

public static class ApiRoutes
{
    public const string Root = "api";
    public const string Version = "v1";
    public const string Base = $"{Root}/{Version}";

    public static class Home
    {
        public const string Login = $"{Base}/login";
        public const string Register = $"{Base}/register";
        public const string Test = $"{Base}/test";
    }

    public static class Store
    {
        public const string GetStore = $"{Base}/store/get";
        public const string GetStoreList = $"{Base}/store/get-list";
    }

    public static class Order
    {
        public const string GetOrder = $"{Base}/order/get";
        public const string GetOrderList = $"{Base}/order/get-list";
        public const string CreateOrder = $"{Base}/order/create";
        public const string ModifyOrder = $"{Base}/order/modify";
        public const string DeleteOrder = $"{Base}/order/delete";

        public const string ApproveOrder = $"{Base}/order/approve";
        public const string CompleteOrder = $"{Base}/order/complete";
        public const string CheckOrder = $"{Base}/order/check";
    }
}
