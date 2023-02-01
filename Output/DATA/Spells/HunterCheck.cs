#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HunterCheck : BBBuffScript
    {
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.COMBAT_DEHANCER)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
                else if(type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                }
            }
            return returnValue;
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.AdrenalineRushDebuff(), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}