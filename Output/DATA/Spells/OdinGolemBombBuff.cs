#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGolemBombBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.SNARE)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.SLOW)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.FEAR)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.CHARM)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.SLEEP)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.STUN)
                {
                    duration *= 0.7f;
                }
                if(type == BuffType.TAUNT)
                {
                    duration *= 0.7f;
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            float nextBuffVars_HPPerLevel;
            TeamId orderTeam;
            TeamId chaosTeam;
            SetGhosted(owner, true);
            nextBuffVars_HPPerLevel = 315;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            orderTeam = TeamId.TEAM_BLUE;
            chaosTeam = TeamId.TEAM_PURPLE;
            this.bubbleID = AddUnitPerceptionBubble(orderTeam, 650, owner, 25000, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(chaosTeam, 650, owner, 25000, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                bool run;
                run = false;
                if(run)
                {
                    if(!owner.IsDead)
                    {
                        bool killedGuardian;
                        killedGuardian = false;
                        foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 450, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, nameof(Buffs.OdinGuardianBuff), true))
                        {
                            killedGuardian = true;
                            ApplyDamage((ObjAIBase)owner, unit, 1000000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
                        }
                        if(killedGuardian)
                        {
                            ApplyDamage(attacker, owner, 1000000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}