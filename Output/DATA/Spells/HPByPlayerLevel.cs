#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HPByPlayerLevel : BBBuffScript
    {
        float hPPerLevel;
        float maxPlayerLevel;
        float lastTimeExecuted;
        public HPByPlayerLevel(float hPPerLevel = default)
        {
            this.hPPerLevel = hPPerLevel;
        }
        public override void OnActivate()
        {
            int playerLevel;
            //RequireVar(this.hPPerLevel);
            this.maxPlayerLevel = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
            {
                playerLevel = GetLevel(unit);
                if(playerLevel > this.maxPlayerLevel)
                {
                    this.maxPlayerLevel = playerLevel;
                }
            }
        }
        public override void OnUpdateStats()
        {
            float myHealth;
            int playerLevel;
            float hPIncrease;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, false))
            {
                myHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if(myHealth >= 0.99f)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes))
                    {
                        playerLevel = GetLevel(unit);
                        if(playerLevel > this.maxPlayerLevel)
                        {
                            this.maxPlayerLevel = playerLevel;
                        }
                    }
                }
            }
            hPIncrease = this.hPPerLevel * this.maxPlayerLevel;
            IncFlatHPPoolMod(owner, hPIncrease);
        }
    }
}