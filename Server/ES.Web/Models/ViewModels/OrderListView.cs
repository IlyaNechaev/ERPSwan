namespace ES.Web.Models.ViewModels;

public struct OrderListView
{
    public OrderView[] list { get; set; }
    public struct OrderView
    {
        public Guid id { get; set; }
        public int number { get; set; }
        public DateTime date_reg { get; set; }
        public bool is_completed { get; set; }
    }
}
