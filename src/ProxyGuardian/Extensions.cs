namespace ProxyGuardian
{
    public static class Extensions
    {
        public static int ToInt32(this string source, int defaultValue = 0) =>
            string.IsNullOrWhiteSpace(source) || !int.TryParse(source, out var value)
                ? defaultValue
                : value;
    }
}