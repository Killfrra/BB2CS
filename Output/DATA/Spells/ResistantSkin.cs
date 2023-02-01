#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ResistantSkin : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Resistant Skin",
            BuffTextureName = "GreenTerror_ChitinousExoplates.dds",
        };
        float lastTimeExecuted;
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            float distance;
            if(owner.Team != attacker.Team)
            {
                if(scriptName == nameof(Buffs.GlobalWallPush))
                {
                    returnValue = false;
                }
                else if(type == BuffType.FEAR)
                {
                    returnValue = false;
                }
                else if(type == BuffType.CHARM)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SILENCE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLEEP)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SLOW)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SNARE)
                {
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    returnValue = false;
                }
                else if(type == BuffType.TAUNT)
                {
                    returnValue = false;
                }
                else if(type == BuffType.BLIND)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SUPPRESSION)
                {
                    returnValue = false;
                }
                else if(type == BuffType.SHRED)
                {
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WrathTimer)) == 0)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.SweepTimer)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PropelTimer)) == 0)
                        {
                            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer2)) == 0)
                            {
                                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ActionTimer)) == 0)
                                {
                                    distance = DistanceBetweenObjects("Attacker", "Owner");
                                    if(distance > 950)
                                    {
                                        returnValue = false;
                                    }
                                }
                            }
                        }
                    }
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
            float gameTime;
            float bonusHealth;
            float bonusRegen;
            SetFeared(owner, false);
            SetNearSight(owner, false);
            SetSilenced(owner, false);
            SetSleep(owner, false);
            SetStunned(owner, false);
            SetNetted(owner, false);
            SetFeared(owner, false);
            SetDisarmed(owner, false);
            SetTaunted(owner, false);
            SetCharmed(owner, false);
            SetFeared(owner, false);
            gameTime = GetGameTime();
            bonusHealth = gameTime * 2.083f;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            bonusRegen = gameTime * 0.00625f;
            IncPermanentFlatHPRegenMod(owner, bonusRegen);
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectMinions | SpellDataFlags.AffectWards, nameof(Buffs.SharedWardBuff), true))
            {
                MoveAway(unit, owner.Position, 1000, 50, 300, 300, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 300, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            }
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(60, ref this.lastTimeExecuted, false))
            {
                IncPermanentFlatHPPoolMod(owner, 125);
                IncPermanentFlatHPRegenMod(owner, 0.375f);
            }
        }
        public override void OnDeath()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            if(teamID == TeamId.TEAM_BLUE)
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_BLUE, default, true))
                {
                    if(!unit.IsDead)
                    {
                        AddBuff(unit, unit, new Buffs.ExaltedWithBaronNashor(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
            else
            {
                foreach(Champion unit in GetChampions(TeamId.TEAM_PURPLE, default, true))
                {
                    if(!unit.IsDead)
                    {
                        AddBuff(unit, unit, new Buffs.ExaltedWithBaronNashor(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}