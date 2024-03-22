using static Sherpa.src.sherpa.Assertions.Affirm;

namespace Sherpa.src.sherpa.Assertions;

public static class Collection
{
    public static bool Contain<T>(this A<T> a, T b) where T : ICollection<T>
    {
        foreach (var i in a.Value)
            if (Equals(i, b))
                return true;

        return false;
    }
    public static bool NotContain<T>(this A<T> a, T b) where T : ICollection<T>
    {
        foreach (var i in a.Value)
            if (Equals(i, b))
                return false;

        return true;
    }
    public static bool NotBeEmpty<T>(this A<T> a) where T : ICollection<T>
    {
        return a.Value.Count > 0;
    }
    public static bool BeEmpty<T>(this A<T> a) where T : ICollection<T>
    {
        return a.Value.Count == 0;
    }
}