using static Sherpa.src.sherpa.Assertions.Affirm;

namespace Sherpa.src.sherpa.Assertions;

public static class Equality
{
    public static bool Be<T>(this A<T> x, T y)
    {
        return x.Value.Equals(y);
    }
    public static bool NotBe<T>(this A<T> x, T y)
    {
        return !x.Value.Equals(y);
    }
    public static bool Equal<T>(this A<T> x, T y)
    {
        return x.Value.Equals(y);
    }
    public static bool NotEqual<T>(this A<T> x, T y)
    {
        return !x.Value.Equals(y);
    }
}