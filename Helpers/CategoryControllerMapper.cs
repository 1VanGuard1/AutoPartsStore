namespace AutoPartsStore.Helpers
{
    public static class CategoryControllerMapper
    {
        private static readonly Dictionary<string, string> Map = new()
    {
        { "Аккумуляторы", "Battery" },
        { "Шины", "Tire" },
        { "Моторные масла", "MotorOil" },
        { "Тормозные колодки", "BrakePad" },
        { "Дворники", "Wiper" },
        { "Свечи зажигания", "SparkPlug" }
    };

        public static string GetController(string categoryName)
        {
            return Map.TryGetValue(categoryName, out var controller)
                ? controller
                : "Admin"; // fallback
        }
    }
}
