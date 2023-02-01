#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JaxLeapStrikeAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        Particle particle; // UNUSED
        int[] effect0 = {70, 110, 150, 190, 230};
        int[] effect1 = {40, 85, 130, 175, 220};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float bonusDamage;
            float totalAD;
            float baseAD;
            float bonusAD;
            float attackDamage; // UNUSED
            float damageToDeal;
            float baseAP;
            float aPDamage;
            TeamId teamID; // UNITIALIZED
            BreakSpellShields(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            bonusDamage = this.effect0[level];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            attackDamage = bonusAD * 1;
            damageToDeal = bonusDamage + bonusAD;
            baseAP = GetFlatMagicDamageMod(owner);
            aPDamage = baseAP * 0.6f;
            damageToDeal += aPDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.JaxEmpowerTwo)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
                SpellEffectCreate(out this.particle, out _, "EmpowerTwoHit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, target, default, default, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.JaxEmpowerTwo), (ObjAIBase)owner, 0);
            }
            if(target is Champion)
            {
                if(owner.Team != target.Team)
                {
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
            }
        }
    }
}