using System.Text.RegularExpressions;
using Newtonsoft.Json;

public static class BB2JSON
{
    public static (Dictionary<string, object> metadata, Dictionary<string, List<Block>> functions)
    Parse(string text)
    {
        var (metadataJson, functionsJson) = ConvertToJSON_Regex(text);

        Dictionary<string, object> metadata = new();
        Dictionary<string, List<Block>> functions = new();
        try
        {
            //metadata = JsonConvert.DeserializeObject<Dictionary<string, object>>(metadataJson)!;
            functions = JsonConvert.DeserializeObject<Dictionary<string, List<Block>>>(functionsJson)!; 
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            Console.WriteLine(metadataJson);
            Console.WriteLine(functionsJson);
        }
        return (metadata, functions);
    }

    private static (string, string) ConvertToJSON_Regex(string text)
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

        metadata = string.Join(",\n", Regex.Matches(metadata, @"^\w+ = (?:\{.*?^\}|.*?$)", ms).Select(m => m.Value));
        //metadata = Regex.Replace(metadata, @",(\s*(\]|\}|$))", "$1");

        metadata  = Regex.Replace(metadata,  @"(\w+) =", "\"$1\":");
        functions = Regex.Replace(functions, @"(\w+) =", "\"$1\":");

        metadata = Regex.Replace(metadata, @"\bTrue\b", "true");
        metadata = Regex.Replace(metadata, @"\bFalse\b", "false");

        void Escape(ref string text)
        {
            text = Regex.Replace(text, """(:\s*)(?!-?\d(?:\.\d+)?|true|false)([^\s"].*?[^\s"])(\s*(?:,|\}|$))""", "$1\"$$$2$$\"$3");
        }
        Escape(ref metadata);
        Escape(ref functions);

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

        metadata = "{\n" + metadata.Indent() + "\n}";
        functions = "{\n" + functions.Indent() + "\n}";

        return (metadata, functions);
    }
}