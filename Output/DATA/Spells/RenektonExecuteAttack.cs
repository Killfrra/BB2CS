#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class RenektonExecuteAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {35, 70, 105, 140, 175};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_BonusDamage; // UNUSED
            float damageAmount;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_BonusDamage = this.effect0[level];
            SpellBuffRemove(owner, nameof(Buffs.RenektonPreExecute), (ObjAIBase)owner);
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    float ragePercent;
                    ragePercent = GetPARPercent(owner, PrimaryAbilityResourceType.Other);
                    if(ragePercent >= 0.5f)
                    {
                        SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                        charVars.Swung = true;
                    }
                    else
                    {
                        SpellCast(attacker, target, default, default, 0, SpellSlotType.ExtraSlots, level, true, false, false, true, true, false);
                        charVars.Swung = true;
                    }
                }
            }
            damageAmount *= 0;
        }
    }
}