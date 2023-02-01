#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorPowerTransferReturn : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Viktor_Reverb_shield.troy", },
            BuffName = "ViktorShield",
            BuffTextureName = "ViktorPowerTransfer.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            if(charVars.IsChampTarget)
            {
                charVars.TotalDamage *= 0.4f;
                IncreaseShield(owner, charVars.TotalDamage, true, true);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, 10000, true, true);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageAmount > charVars.TotalDamage)
            {
                damageAmount -= charVars.TotalDamage;
                ReduceShield(owner, damageAmount, true, true);
                RemoveShield(owner, 0, true, true);
                SpellBuffRemove(owner, default, (ObjAIBase)owner, 0);
            }
            else
            {
                ReduceShield(owner, damageAmount, true, true);
                charVars.TotalDamage -= damageAmount;
                damageAmount = 0;
            }
        }
    }
}
namespace Spells
{
    public class ViktorPowerTransferReturn : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {40, 65, 90, 115, 140};
        float[] effect1 = {0.5f, 0.5f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float pAR;
            float baseDamage;
            float aoEDamage;
            float bonusDamage;
            float totalDamage;
            TeamId ownerTeam; // UNUSED
            TeamId targetTeam; // UNUSED
            teamID = GetTeamID(attacker);
            pAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            baseDamage = this.effect0[level];
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            aoEDamage = this.effect1[level];
            bonusDamage = pAR * 0.08f;
            totalDamage = bonusDamage + baseDamage;
            aoEDamage *= totalDamage;
            ownerTeam = GetTeamID(owner);
            targetTeam = GetTeamID(target);
            if(owner.Team != target.Team)
            {
                ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.2f, 1, false, false, attacker);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.ViktorPowerTransferReturn(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}