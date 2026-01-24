public static class CategoryControllerMapper
{
    private static readonly Dictionary<string, string> MapRuToController = new()
    {
        { "Аккумуляторы", "Battery" },
        { "Шины", "Tire" },
        { "Моторные масла", "MotorOil" },
        { "Тормозные колодки", "BrakePad" },
        { "Дворники", "Wiper" },
        { "Свечи зажигания", "SparkPlug" }
    };

    private static readonly Dictionary<string, string> MapEnToRu = new()
    {
        { "Batteries", "Аккумуляторы" },
        { "Tires", "Шины" },
        { "Motor Oils", "Моторные масла" },
        { "Brake Pads", "Тормозные колодки" },
        { "Wipers", "Дворники" },
        { "Spark Plugs", "Свечи зажигания" }
    };

    public static string GetControllerByRu(string categoryNameRu)
        => MapRuToController.TryGetValue(categoryNameRu, out var controller)
            ? controller
            : "Admin";

    public static string GetRussianName(string categoryNameEn)
        => MapEnToRu.TryGetValue(categoryNameEn, out var ru)
            ? ru
            : categoryNameEn;
}
