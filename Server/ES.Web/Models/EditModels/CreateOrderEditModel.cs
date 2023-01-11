namespace ES.Web.Models.EditModels;

public struct CreateOrderEditModel
{
    /// <summary>
    /// Номер ПЗ
    /// </summary>
    public int number { get; set; }

    /// <summary>
    /// Идентификатор бригадира
    /// </summary>
    public Guid foremanId { get; set; }

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
