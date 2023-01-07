namespace ES.Web.Models.EditModels;

public struct CreateOrderEditModel
{
    public string name { get; set; }
    public OrderPart[] parts { get; set; }
    
    public struct OrderPart
    {
        public int order_num { get; set; }
        public DateTime? date_end { get; set; }
        public OrderMaterial[] storelist { get; set; }
        public struct OrderMaterial
        {
            public Guid id { get; set; }
            public int count { get; set; }
        }
    }
}
