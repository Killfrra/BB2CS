#nullable enable

using System.Numerics;

public static partial class Functions
{
    [BBFunc]
    public static void ExecutePeriodically(
        float timeBetweenExecutions,
        ref float trackTime,
        bool executeImmediately = false,
        //TODO: ref float tickTime?

        [BBSubBlocks]
        Action? subBlocks = null
    ){}

    [BBFunc(Dest = "Dest")]
    public static T SetVarInTable<T>(
        [BBParam("Var", "VarTable", "Value", "ValueByLevel")]
        T src
    ){
        return src;
    }
}