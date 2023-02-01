#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class SeismicShard : BBSpellScript
    {
        int[] effect0 = {70, 120, 170, 220, 270};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_Level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_Level = level;
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0.6f, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.SeismicShardBuff(nextBuffVars_Level), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false);
        }
    }
}