#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinGuardianStatsByLevel : BBBuffScript
    {
        float dmgPerLevel;
        float maxPlayerLevel;
        float lastTimeExecuted;
        public OdinGuardianStatsByLevel(float dmgPerLevel = default)
        {
            this.dmgPerLevel = dmgPerLevel;
        }
        public override void OnActivate()
        {
            //RequireVar(this.hPPerLevel);
            //RequireVar(this.dmgPerLevel);
            //RequireVar(this.armorPerLevel);
            //RequireVar(this.mR_per_level);
            this.maxPlayerLevel = 0;
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                int playerLevel;
                playerLevel = GetLevel(unit);
                if(playerLevel > this.maxPlayerLevel)
                {
                    this.maxPlayerLevel = playerLevel;
                }
            }
        }
        public override void OnUpdateStats()
        {
            float dmgIncrease;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, false))
            {
                float myHealth;
                myHealth = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if(myHealth >= 0.99f)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 9999, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                    {
                        int playerLevel;
                        playerLevel = GetLevel(unit);
                        if(playerLevel > this.maxPlayerLevel)
                        {
                            this.maxPlayerLevel = playerLevel;
                        }
                    }
                }
            }
            dmgIncrease = this.dmgPerLevel * this.maxPlayerLevel;
            IncFlatPhysicalDamageMod(owner, dmgIncrease);
        }
    }
}