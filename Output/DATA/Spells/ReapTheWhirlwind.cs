#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ReapTheWhirlwind : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "Reap The Whirlwind",
            BuffTextureName = "Janna_ReapTheWhirlwind.dds",
        };
        float tickAmount;
        Particle particle2;
        Particle particle;
        float friendlyTimeExecuted;
        public ReapTheWhirlwind(float tickAmount = default)
        {
            this.tickAmount = tickAmount;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            float temp1;
            Vector3 center;
            Vector3 nextBuffVars_Center;
            float nextBuffVars_Distance;
            int nextBuffVars_IdealDistance;
            float nextBuffVars_Gravity;
            float nextBuffVars_Speed;
            //RequireVar(this.tickAmount);
            teamOfOwner = GetTeamID(owner);
            charVars.Ticks = 0;
            SpellEffectCreate(out this.particle2, out this.particle, "ReapTheWhirlwind_green_cas.troy", "ReapTheWhirlwind_red_cas.troy", teamOfOwner, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if(owner.Team == unit.Team)
                {
                    temp1 = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                    if(temp1 < 1)
                    {
                        ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                        IncHealth(unit, this.tickAmount, owner);
                    }
                }
                else
                {
                    BreakSpellShields(unit);
                    center = GetUnitPosition(owner);
                    nextBuffVars_Center = center;
                    nextBuffVars_Distance = 1000;
                    nextBuffVars_IdealDistance = 1000;
                    nextBuffVars_Gravity = 10;
                    nextBuffVars_Speed = 1200;
                    AddBuff(attacker, unit, new Buffs.MoveAway(nextBuffVars_Distance, nextBuffVars_Gravity, nextBuffVars_Speed, nextBuffVars_Center), 1, 1, 0.75f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
        }
        public override void OnUpdateStats()
        {
            float temp1;
            if(ExecutePeriodically(0.5f, ref this.friendlyTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 700, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    temp1 = GetHealthPercent(unit, PrimaryAbilityResourceType.MANA);
                    if(temp1 < 1)
                    {
                        ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                        IncHealth(unit, this.tickAmount, owner);
                    }
                }
            }
        }
    }
}
namespace Spells
{
    public class ReapTheWhirlwind : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            ChannelDuration = 4f,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {35, 55, 75};
        public override void ChannelingStart()
        {
            float baseTickAmount;
            float aPAmount;
            float aPTickBonus;
            float tickAmount;
            float nextBuffVars_TickAmount;
            baseTickAmount = this.effect0[level];
            aPAmount = GetFlatMagicDamageMod(owner);
            aPTickBonus = aPAmount * 0.175f;
            tickAmount = baseTickAmount + aPTickBonus;
            nextBuffVars_TickAmount = tickAmount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.ReapTheWhirlwind(nextBuffVars_TickAmount), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.25f, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.ReapTheWhirlwind), (ObjAIBase)owner, 0);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.ReapTheWhirlwind), (ObjAIBase)owner, 0);
        }
    }
}