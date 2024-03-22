using static Sherpa.src.sherpa.Assertions.Affirm;

namespace Sherpa.src.sherpa.Assertions;

public static class Boolean
{
    public static bool BeFalse<T>(this A<T> r) where T : IEquatable<bool>
    {
        return r.Value.Equals(false);
    }
    public static bool BeTrue<T>(this A<T> r) where T : IEquatable<bool>
    {
        return r.Value.Equals(true);
    }
}