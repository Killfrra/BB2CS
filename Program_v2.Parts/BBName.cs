public class BBSpellName: BBName
{
    public BBSpellName(string str): base(str){}
}

public class BBBuffName: BBName
{
    public BBBuffName(string str): base(str){}
}

public class BBName
{
    public string Value;
    public BBName(string str)
    {
        Value = str;
    }
    public static implicit operator BBName(string str){ return new(str); }
    public static string? Resolve(object? buffNameParam)
    {
        return (buffNameParam as string) ?? (buffNameParam as BBBuffName)?.Value;
    }
}