#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptDragon : BBCharScript
    {
        public override void OnUpdateStats()
        {
            int nextBuffVars_HPPerLevel;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) > 0)
            {
            }
            else
            {
                nextBuffVars_HPPerLevel = 220;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            int nextBuffVars_TickDamage;
            float nextBuffVars_attackSpeedMod;
            nextBuffVars_TickDamage = 15;
            nextBuffVars_attackSpeedMod = -0.2f;
            AddBuff(attacker, target, new Buffs.DragonBurning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
        public override void OnActivate()
        {
            AddBuff(attacker, target, new Buffs.ResistantSkinDragon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}