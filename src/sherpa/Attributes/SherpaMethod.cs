namespace Sherpa.src.sherpa.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class SherpaMethod : Attribute
{
    public object[] Data { get; private set; }

    public SherpaMethod()
    {
        Data = [];
    }

    public SherpaMethod(object data)
    {
        Data = [data];
    }
}
