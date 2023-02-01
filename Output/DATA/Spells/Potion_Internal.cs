#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Potion_Internal : BBBuffScript
    {
        float lastTimeExecuted;
        public override void OnActivate()
        {
            charVars.CountHealthPotion = GetBuffCountFromAll(owner, nameof(Buffs.RegenerationPotion));
            charVars.CountManaPotion = GetBuffCountFromAll(owner, nameof(Buffs.FlaskOfCrystalWater));
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                charVars.CountHealthPotion = GetBuffCountFromAll(owner, nameof(Buffs.RegenerationPotion));
                charVars.CountManaPotion = GetBuffCountFromAll(owner, nameof(Buffs.FlaskOfCrystalWater));
                if(charVars.CountHealthPotion == 0)
                {
                    if(charVars.CountManaPotion == 0)
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
                if(charVars.CountHealthPotion >= 1)
                {
                    IncHealth(owner, 10, owner);
                }
                if(charVars.CountManaPotion >= 1)
                {
                    IncPAR(owner, 5, PrimaryAbilityResourceType.MANA);
                }
            }
        }
    }
}