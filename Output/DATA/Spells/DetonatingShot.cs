﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class DetonatingShot : BBSpellScript
    {
        int[] effect0 = {22, 28, 34, 40, 46};
        int[] effect1 = {5, 5, 5, 5, 5};
        int[] effect2 = {5, 5, 5, 5, 5};
        int[] effect3 = {5, 5, 5, 5, 5};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_dotdmg;
            IssueOrder(owner, OrderType.AttackTo, default, target);
            SpellBuffRemove(owner, nameof(Buffs.DetonatingShot), (ObjAIBase)owner, 0);
            nextBuffVars_dotdmg = this.effect0[level];
            AddBuff(attacker, target, new Buffs.ExplosiveShotDebuff(nextBuffVars_dotdmg), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 1, true, false, false);
            AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, this.effect2[level], BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, this.effect3[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class DetonatingShot : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Detonating Shot",
            BuffTextureName = "Tristana_ExplosiveShot.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellFXOverrideSkins = new[]{ "RocketTristana", },
        };
        int[] effect0 = {50, 75, 100, 125, 150};
        public override void OnKill()
        {
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.DetonatingShot_Target)) > 0)
            {
                Particle e; // UNUSED
                int level;
                SpellEffectCreate(out e, out _, "DetonatingShot_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    Particle b; // UNUSED
                    BreakSpellShields(unit);
                    SpellEffectCreate(out b, out _, "tristana_explosiveShot_unit_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage((ObjAIBase)owner, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
                }
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(owner.Team != target.Team)
                {
                    AddBuff((ObjAIBase)owner, target, new Buffs.DetonatingShot_Target(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}