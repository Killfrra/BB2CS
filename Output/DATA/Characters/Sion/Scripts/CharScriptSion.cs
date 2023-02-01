#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptSion : BBCharScript
    {
        float blockAmount;
        float finalDamage;
        float[] effect0 = {0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f};
        int[] effect1 = {30, 30, 30, 30, 30, 30, 40, 40, 40, 40, 40, 40, 50, 50, 50, 50, 50, 50};
        public override void SetVarsByLevel()
        {
            charVars.BlockChance = this.effect0[level];
            charVars.BaseBlockAmount = this.effect1[level];
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(damageSource == default)
            {
                if(RandomChance() < charVars.BlockChance)
                {
                    this.blockAmount = Math.Min(charVars.BaseBlockAmount, damageAmount);
                    this.finalDamage = damageAmount - this.blockAmount;
                    SpellEffectCreate(out _, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    damageAmount = this.finalDamage;
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.FeelNoPain(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, default, false);
        }
    }
}