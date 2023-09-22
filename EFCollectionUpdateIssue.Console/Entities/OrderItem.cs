namespace EFCollectionUpdateIssue.Console.Entities;

// this class represents OrderItem value object
public sealed class OrderItem
{
    // empty constructor for EF purposes
    private OrderItem() { }
    // artificial Id - for EF purposes
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public int Quantity { get; private set; }

    private OrderItem(string name, int quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Quantity = quantity;
    }

    public static OrderItem Create(string name, int quantity)
    {
        return new OrderItem(name, quantity);
    }
}