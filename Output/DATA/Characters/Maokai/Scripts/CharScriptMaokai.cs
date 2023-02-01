#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptMaokai : BBCharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            TeamId teamID;
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    teamID = GetTeamID(owner);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
                    {
                        if(teamID == TeamId.TEAM_BLUE)
                        {
                            AddBuff(attacker, unit, new Buffs.MaokaiSapMagic(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        else
                        {
                            AddBuff(attacker, unit, new Buffs.MaokaiSapMagicChaos(), 1, 1, 0.75f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnPreAttack()
        {
            float healthPercent;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MaokaiSapMagicMelee)) > 0)
                    {
                        healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                        if(healthPercent < 1)
                        {
                            OverrideAnimation("Attack", "Passive", owner);
                            OverrideAnimation("Attack2", "Passive", owner);
                            OverrideAnimation("Crit", "Passive", owner);
                        }
                        else
                        {
                            ClearOverrideAnimation("Attack", owner);
                            ClearOverrideAnimation("Attack2", owner);
                            ClearOverrideAnimation("Crit", owner);
                        }
                    }
                    else
                    {
                        ClearOverrideAnimation("Attack", owner);
                        ClearOverrideAnimation("Attack2", owner);
                        ClearOverrideAnimation("Crit", owner);
                    }
                }
                else
                {
                    ClearOverrideAnimation("Attack", owner);
                    ClearOverrideAnimation("Attack2", owner);
                    ClearOverrideAnimation("Crit", owner);
                }
            }
            else
            {
                ClearOverrideAnimation("Attack", owner);
                ClearOverrideAnimation("Attack2", owner);
                ClearOverrideAnimation("Crit", owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiSapMagicPass(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.Tally = 0;
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}