using System.Reflection;

public class Block
{
    public string Function;
    public Dictionary<string, object> Params = new();
    public List<Block> SubBlocks = new();

    public string ResolvedName;
    public List<Union<Composite, SubBlocks, Reference>> ResolvedParams = new();
    public Reference? ResolvedReturn;

    public SubBlocks Parent;

    public void Scan(SubBlocks parent)
    {
        Parent = parent;
        ResolvedName = Function.Substring(3, Function.Length - 3 - 1);

        var mInfo = typeof(Functions).GetMethod(
            ResolvedName, BindingFlags.Public | BindingFlags.Static
        );
        if(mInfo == null)
        {
            throw new Exception($"{ResolvedName} is undefiend");
        }

        foreach(var pInfo in mInfo.GetParameters())
        {
            var sAttr = pInfo.GetCustomAttribute<BBSubBlocks>();
            if(sAttr != null)
            {
                var sb = new SubBlocks();
                    sb.Blocks = SubBlocks;
                    sb.Scan(parent);
                foreach(var paramNameName in sAttr.ParamNames)
                {
                    var paramName = (string)Params[paramNameName + "Var"];
                    var arg = new Var(parent: sb);
                        arg.IsArgument = true;
                        arg.Initialized = true; //arg.Write(typeof(object)); //TODO:
                    sb.LocalVars[paramName] = arg;
                }
                ResolvedParams.Add(sb);
            }
            else
            {
                if(pInfo.IsOut || pInfo.ParameterType.IsByRef)
                {
                    var value = Reference.Resolve(pInfo, Params, Parent) ?? new Reference(null, "_");
                        value.IsOut = pInfo.IsOut && pInfo.ParameterType.IsByRef;
                        value.IsRef = !pInfo.IsOut && pInfo.ParameterType.IsByRef;
                    ResolvedParams.Add(value);
                }
                else
                {
                    var value = new Composite(pInfo, Params, Parent);
                    ResolvedParams.Add(value);
                }
            }

            var returnType = mInfo.ReturnType;
            if(returnType != typeof(void))
            {
                var param0 = (ResolvedParams.Count > 0) ? ResolvedParams[0].Item1 : null;
                ResolvedReturn = Reference.Resolve(mInfo, Params, Parent, param0);
            }
        }
    }

    public string ToCSharp()
    {
        return
        ((ResolvedReturn != null) ? (ResolvedReturn.ToCSharp() + " = ") : "") +
        ResolvedName + "(" +
            string.Join(", ", ResolvedParams.Select(p => p.Item1?.ToCSharp() ?? p.Item2?.ToCSharp() ?? p.Item3!.ToCSharp())) +
        ");";
    }
}