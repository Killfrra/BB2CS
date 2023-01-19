public class Union<T1, T2>
{
    private T1? item1;
    private T2? item2;
    public T1? Item1 => item1;
    public T2? Item2 => item2;
    protected Union(){}
    public Union(T1 item){ item1 = item; }
    public Union(T2 item){ item2 = item; }
    public static implicit operator Union<T1, T2>(T1 item){ return new(item); }
    public static implicit operator Union<T1, T2>(T2 item){ return new(item); }
    public override string? ToString()
    {
        return item1?.ToString() ?? item2?.ToString();
    }
}

public class Union<T1, T2, T3>: Union<T1, T2>
{
    private T3? item3;
    public T3? Item3 => item3;
    public Union(T3 item){ item3 = item; }
    public static implicit operator Union<T1, T2, T3>(T3 item){ return new(item); }
    public override string? ToString()
    {
        return base.ToString() ?? item3?.ToString();
    }
}