﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AlZaharMaleficVisions : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "AlzaharMaleficVisions_tar.troy", },
            BuffName = "AlZaharMaleficVisions",
            BuffTextureName = "AlZahar_MaleficVisions.dds",
        };
        TeamId teamID;
        float lastTimeExecuted;
        float[] effect0 = {10, 17.5f, 25, 32.5f, 40};
        int[] effect1 = {10, 14, 18, 22, 26};
        public override void OnActivate()
        {
            this.teamID = GetTeamID(attacker);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.AlZaharVoidling), true))
            {
                if(owner.Team != unit.Team)
                {
                    ApplyTaunt(owner, unit, 1.5f);
                }
            }
        }
        public override void OnUpdateActions()
        {
            int level;
            float damagePerTick;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damagePerTick = this.effect0[level];
            if(ExecutePeriodically(0.45f, ref this.lastTimeExecuted, false))
            {
                ApplyDamage(attacker, owner, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.1f, 1, false, false, attacker);
                if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.AlZaharRecentVis)) > 0)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                    {
                        if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.AlZaharVoidling)) > 0)
                        {
                            if(owner.Team != unit.Team)
                            {
                                ApplyTaunt(owner, unit, 1.5f);
                            }
                        }
                    }
                }
            }
        }
        public override void OnDeath()
        {
            int level;
            float manaRestore;
            attacker = GetChampionBySkinName("Malzahar", this.teamID);
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            manaRestore = this.effect1[level];
            IncPAR(attacker, manaRestore, PrimaryAbilityResourceType.MANA);
            foreach(AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.AlZaharRecentVis), false))
            {
                BreakSpellShields(unit);
                AddBuff(attacker, unit, new Buffs.AlZaharRecentVis(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
                AddBuff(attacker, unit, new Buffs.AlZaharMaleficVisions(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false);
            }
        }
    }
}
namespace Spells
{
    public class AlZaharMaleficVisions : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
            {
                AddBuff(attacker, attacker, new Buffs.AlZaharVoidlingCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.AlZaharRecentVis(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, target, new Buffs.AlZaharMaleficVisions(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false);
        }
    }
}