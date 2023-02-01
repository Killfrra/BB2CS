#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class AsheSpiritOfTheHawk : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            Vector3 nextBuffVars_TargetPos;
            nextBuffVars_TargetPos = missileEndPosition;
            AddBuff((ObjAIBase)owner, owner, new Buffs.AsheSpiritOfTheHawkBubble(nextBuffVars_TargetPos), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}