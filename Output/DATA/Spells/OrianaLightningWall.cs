#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaLightningWall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "KogMaw_VoidOoze.dds",
        };
        Particle particle;
        float tickDamage;
        float lastTimeExecuted;
        float[] effect0 = {20, 27.5f, 35};
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            ObjAIBase caster;
            int level;
            float baseDamage;
            float selfAP;
            float bonusDamage;
            float totalDamage;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle, out _, "ManaLeach_tar2.troy", default, teamOfOwner, 240, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetTargetable(owner, false);
            caster = SetBuffCasterUnit();
            level = GetSlotSpellLevel(caster, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            selfAP = GetFlatMagicDamageMod(caster);
            bonusDamage = selfAP * 0.25f;
            totalDamage = baseDamage + bonusDamage;
            this.tickDamage = totalDamage;
        }
        public override void OnDeactivate(bool expired)
        {
            SetTargetable(owner, true);
            SpellEffectRemove(this.particle);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetTargetable(owner, false);
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_TickDamage;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 120, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    nextBuffVars_TickDamage = this.tickDamage;
                    AddBuff(attacker, unit, new Buffs.OrianaDoT(nextBuffVars_TickDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                }
            }
        }
    }
}