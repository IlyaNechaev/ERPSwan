namespace ES.Web.Models.ViewModels;

public struct OrderView
{
    public Guid id { get; set; }
    public int number { get; set; }
    public DateTime date_reg { get; set; }
    public bool is_approved { get; set; }
    public bool is_completed { get; set; }
    public bool is_checked { get; set; }
    public float sum { get; set; }
    public ForemanView foreman { get; set; }
    public OrderPartView[] parts { get; set; }
}

public struct ForemanView
{
    public Guid id { get; set; }
    public string fullname { get; set; }
}

public struct OrderPartView
{
    public Guid id { get; set; }
    public int order_num { get; set; }
    public bool is_completed { get; set; }
    public DateTime? date_end { get; set; }
    public OrderStoreView[] storelist { get; set; }
}

public struct OrderStoreView
{
    public Guid id { get; set; }
    public string name { get; set; }
    public int count { get; set; }
}