using Microsoft.EntityFrameworkCore;
public class CoffeeDbContext : DbContext{
    public DbSet<Products> Products => Set<Products>();
    public DbSet<Orders> Orders => Set<Orders>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION") 
        ?? "Host=localhost;Database=CoffeeShopDb;Username=postgres;Password=mysecretpassword";
    
    options.UseNpgsql(connectionString);
}
    public class Products{
        public int ID {get; set;}
        public string Name {get; set;} = string.Empty;
        public decimal Price {get; set;}
        public string Category {get; set;} = string.Empty;
    }
    public class Orders{
        public int Id {get; set;}
        public string CustomerName {get; set;} = string.Empty;
        public string ItemOrdered {get; set;} = string.Empty;
        public int Quantity {get; set;}
       
    }