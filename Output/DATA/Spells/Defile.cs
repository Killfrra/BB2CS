#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Defile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Defile)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Defile), (ObjAIBase)owner, 0);
            }
            else
            {
                AddBuff(attacker, target, new Buffs.Defile(), 1, 1, 30000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Defile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "L_weapon", "", },
            AutoBuffActivateEffect = new[]{ "Defile_glow.troy", "", },
            BuffName = "Defile",
            BuffTextureName = "Lich_Defile.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        Particle particle;
        Particle particle2;
        float lastTimeExecuted;
        int[] effect0 = {30, 50, 70, 90, 110};
        int[] effect1 = {30, 42, 54, 66, 78};
        int[] effect2 = {30, 50, 70, 90, 110};
        public override void OnActivate()
        {
            int level;
            float damageToDeal;
            TeamId teamOfOwner;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageToDeal = this.effect0[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
            }
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out this.particle2, "Defile_green_cas.troy", "Defile_red_cas.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                int level;
                float damageToDeal;
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DeathDefiedBuff)) == 0)
                {
                    float manaCost;
                    float ownerMana;
                    manaCost = this.effect1[level];
                    ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if(ownerMana < manaCost)
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                    else
                    {
                        float negManaCost;
                        negManaCost = -1 * manaCost;
                        IncPAR(owner, negManaCost, PrimaryAbilityResourceType.MANA);
                    }
                }
                damageToDeal = this.effect2[level];
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
                }
            }
        }
    }
}