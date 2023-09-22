namespace EFCollectionUpdateIssue.Console.Entities;

// this class represents Order entity, which has Order Items collection
public sealed class Order
{
    public Guid Id { get; private set; }

    // empty constructor for EF purposes
    private Order() { }

    // original order items list - encapsulated
    private readonly List<OrderItem> _orderItems = new();

    // getter returning copy of order items so that we encapsulate the original collection
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList();
    
    // private constructor - order creation only via CreateOrder factory method
    private Order(Guid id, IEnumerable<OrderItem> initialOrderItems)
    {
        this.Id = id;

        // initialize order items collection
        SetOrderItemsCollection(initialOrderItems);
    }

    public void SetOrderItemsCollection(IEnumerable<OrderItem> orderItems)
    {
        // add combined order items
        _orderItems.Clear();
        _orderItems.AddRange(orderItems);
    }

    public static Order Create(Guid id, IEnumerable<OrderItem> initialOrderItems)
    {
        return new Order(id, initialOrderItems);
    }
}