#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class TalonShadowAssaultToggle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultMisOne), (ObjAIBase)owner, 0);
        }
    }
}