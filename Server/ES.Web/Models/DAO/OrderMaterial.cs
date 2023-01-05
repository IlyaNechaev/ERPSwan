namespace ES.Web.Models.DAO
{
    /// <summary>
    /// Материал, используемый в производственном заказе
    /// </summary>
    public class OrderMaterial
    {
        public int Count { get; set; }

        public Guid PartID { get; set; }
        public OrderPart Part { get; set; }

        public Guid MaterialID { get; set; }
        public Material Material { get; set; }
    }
}
