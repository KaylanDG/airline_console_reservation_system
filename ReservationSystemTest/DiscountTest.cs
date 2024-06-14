namespace ReservationSystemTest;

[TestClass]
public class DiscountTest
{
    private DiscountLogic logic = new DiscountLogic();

    [TestMethod]
    public void TestCodeExist()
    {
        logic.CreateDiscount("TEST", 50, "01-08-2024 10:00", "30-08-2024 10:00");
        Assert.AreEqual(true, logic.DoesCodeExist("TEST"));
    }

    [TestMethod]
    public void TestDiscountPrice()
    {
        logic.CreateDiscount("TEST2", 50, "01-08-2024 10:00", "30-08-2024 10:00");
        DiscountModel discount = logic.GetDiscount("TEST2");
        double price = logic.GetDiscountPrice(discount, 75.0);
        Assert.AreEqual(price, 37.5);

        price = logic.GetDiscountPrice(discount, 50);
        Assert.AreEqual(price, 25.0);

        price = logic.GetDiscountPrice(discount, 43);
        Assert.AreEqual(price, 21.5);

        price = logic.GetDiscountPrice(discount, 1548);
        Assert.AreEqual(price, 774.0);
    }
}