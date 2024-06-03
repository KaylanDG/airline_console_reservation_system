public class DiscountAccess : JsonHandler<DiscountModel>, IJsonHandler<DiscountModel>
{
    public DiscountAccess()
    {
        path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/discount.json"));
    }
}
