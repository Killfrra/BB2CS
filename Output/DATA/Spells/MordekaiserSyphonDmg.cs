#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserSyphonDmg : BBBuffScript
    {
        int count;
        int[] effect0 = {70, 115, 160, 205, 250};
        float[] effect1 = {0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f};
        float[] effect2 = {0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f};
        public override void OnActivate()
        {
            int level;
            //RequireVar(this.baseDamage);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.count = 0;
            ApplyDamage((ObjAIBase)owner, attacker, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(this.count == 0)
            {
                int level;
                float percentLeech;
                float shieldAmount;
                level = GetLevel(owner);
                if(target is Champion)
                {
                    percentLeech = this.effect1[level];
                }
                else
                {
                    percentLeech = this.effect2[level];
                }
                shieldAmount = percentLeech * damageAmount;
                IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                this.count = 1;
            }
        }
    }
}