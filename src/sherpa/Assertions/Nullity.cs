using static Sherpa.src.sherpa.Assertions.Affirm;

namespace Sherpa.src.sherpa.Assertions;

public static class Nullity
{
    public static bool BeNull<T>(this A<T> a)
    {
        return a.Value == null;
    }
    public static bool NotBeNull<T>(this A<T> a)
    {
        return a.Value != null;
    }
}