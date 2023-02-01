#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinSpeedShrineAura : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShamanAura",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        Particle particleOrder;
        Particle particleChaos;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.FEAR)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.CHARM)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SILENCE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SLEEP)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.TAUNT)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.BLIND)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SUPPRESSION)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId orderTeam;
            TeamId chaosTeam;
            Region myBubble; // UNUSED
            Region myBubble2; // UNUSED
            orderTeam = TeamId.TEAM_BLUE;
            chaosTeam = TeamId.TEAM_PURPLE;
            SpellEffectCreate(out this.particleOrder, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_BLUE, default, owner, false, default, default, owner.Position, owner, default, default, false, true, false, false, false);
            SpellEffectCreate(out this.particleChaos, out _, "odin_shrine_time.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_PURPLE, default, owner, false, default, default, owner.Position, owner, default, default, false, true, false, false, false);
            myBubble = AddPosPerceptionBubble(orderTeam, 250, owner.Position, 1, default, false);
            myBubble2 = AddPosPerceptionBubble(chaosTeam, 250, owner.Position, 1, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particleOrder);
            SpellEffectRemove(this.particleChaos);
        }
        public override void OnUpdateActions()
        {
            int count;
            float newDuration;
            float nextBuffVars_SpeedMod;
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 365, SpellDataFlags.AffectHeroes, default, true))
                {
                    count = GetBuffCountFromAll(unit, nameof(Buffs.OdinShrineBombBuff));
                    if(count < 1)
                    {
                        newDuration = 10;
                        if(GetBuffCountFromCaster(unit, unit, nameof(Buffs.MonsterBuffs)) > 0)
                        {
                            newDuration *= 1.2f;
                        }
                        nextBuffVars_SpeedMod = 0.3f;
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinSpeedShrineBuff(nextBuffVars_SpeedMod), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}