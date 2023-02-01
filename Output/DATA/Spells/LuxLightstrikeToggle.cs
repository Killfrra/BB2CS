#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxLightstrikeToggle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.LuxLightstrikeKugel), (ObjAIBase)owner);
        }
    }
}