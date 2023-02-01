#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3152 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            int nextBuffVars_AP_Buff;
            float nextBuffVars_SpellVamp_Buff;
            ObjAIBase caster;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                nextBuffVars_AP_Buff = 30;
                nextBuffVars_SpellVamp_Buff = 0.25f;
                if(owner is Champion)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(unit == owner)
                        {
                            AddBuff((ObjAIBase)owner, unit, new Buffs.WillOfTheAncientsSelf(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                        else
                        {
                            if(!owner.IsDead)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.WillOfTheAncientsFriendly(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
                else
                {
                    if(!owner.IsDead)
                    {
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1200, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                        {
                            caster = GetPetOwner((Pet)owner);
                            if(unit == owner)
                            {
                                if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.WillOfTheAncientsFriendly)) == 0)
                                {
                                    AddBuff((ObjAIBase)owner, unit, new Buffs.WillOfTheAncientsSelf(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                                }
                            }
                            if(unit == owner)
                            {
                            }
                            else if(unit != caster)
                            {
                                AddBuff((ObjAIBase)owner, unit, new Buffs.WillOfTheAncientsFriendly(nextBuffVars_AP_Buff, nextBuffVars_SpellVamp_Buff), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class _3152 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "WillOftheAncients",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        Particle starkSelfParticle;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false);
            if(owner.Team == TeamId.TEAM_BLUE.Team)
            {
                SpellEffectCreate(out this.starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_BLUE, default, owner, true, owner, default, default, owner, default, default, false, default, default, false);
            }
            else
            {
                SpellEffectCreate(out this.starkSelfParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_PURPLE, default, default, true, owner, default, default, owner, default, default, false, default, default, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.starkSelfParticle);
        }
    }
}