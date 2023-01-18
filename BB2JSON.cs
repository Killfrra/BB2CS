using System.Text.RegularExpressions;

public static class BB2JSON
{
    public static (string metadata, string functions) Convert(string text)
    {
        text = Regex.Replace(text, @"(\{|\},?)\s*", "$1\n");
        text = Regex.Replace(text, @"\s*(\})", "\n$1");
        int level = 0;
        text = string.Join("\n", text.Split("\n").Select(line =>
        {
            level -= System.Convert.ToInt32(line.StartsWith("}"));
            var ret = "".PadRight(level * 4) + line.TrimStart();
            level += System.Convert.ToInt32(line.EndsWith("{"));
            return ret;
        }));

        var ms = RegexOptions.Multiline | RegexOptions.Singleline;

        var functions = "";
        var metadata = Regex.Replace(text, @"^(\w+)BuildingBlocks = \{(.*?)^\}", (m) =>
        {
            if(functions != "")
                functions += ",\n";
            var (v1, v2) = (m.Groups[1].Value, m.Groups[2].Value);
            functions += v1 + " = [" + v2 + "]";
            return "";
        }, 
        ms);

        metadata = Regex.Replace(metadata, @"^\w+ = (?:\{.*^\}|.*?$)", "$1,");

        metadata  = Regex.Replace(metadata,  @"(\w+) =", "\"$1\":");
        functions = Regex.Replace(functions, @"(\w+) =", "\"$1\":");

        functions = Regex.Replace(functions, """(:\s*)(?!-?\d(?:\.\d+)?|true|false)([^\s"].*?[^\s"])(\s*[,}])""", "$1\"$$$2$$\"$3");

        bool replaced;
        do
        {
            replaced = false;
            functions = Regex.Replace(functions, """^(\s*)("SubBlocks": )\{(.*?)^\1\}""", (m) => {
                replaced = true;
                var (v1, v2, v3) = (m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value);
                return v1 + v2 + "[" + v3 + v1 + "]";
            }, ms);
        }
        while(replaced);
        
        var number = @"(?:-?\d+(?:\.\d+(?:E-\d+)?)?)";
        var str = "\"" + """(?:\\"|[^"])*""" + "\"";
        void ReplacePrimitiveArrays(ref string text)
        {
            text = Regex.Replace(text, @"\{((?:\s*" + number + @"\s*,)*\s*" + number + @"\s*)\}", "[$1]");
            text = Regex.Replace(text, @"\{((?:\s*" +    str + @"\s*,)*\s*" +    str + @"\s*)\}", "[$1]");
        }
        
        ReplacePrimitiveArrays(ref metadata);
        ReplacePrimitiveArrays(ref functions);

        return
        (
            "{\n" + metadata.Indent() + "\n}",
            "{\n" + functions.Indent() + "\n}"
        );
    }
}