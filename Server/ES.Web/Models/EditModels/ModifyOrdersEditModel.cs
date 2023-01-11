namespace ES.Web.Models.EditModels;

public struct ModifyOrdersEditModel
{
    public Order[] orders { get; set; }
    public OrderPart[] parts { get; set; }
    public struct Order
    {
        public Guid id { get; set; }
        public string? name { get; set; }
    }
    public struct OrderPart
    {
        public Guid id { get; set; }
        public int? order_num { get; set; }
        public DateTime? date_end { get; set; }
        public Material[]? storelist { get; set; }
        public struct Material
        {
            public Guid id { get; set; }
            public int? count { get; set; }
        }
    }
}
