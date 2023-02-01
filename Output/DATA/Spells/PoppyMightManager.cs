#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoppyMightManager : BBBuffScript
    {
        public override void OnActivate()
        {
            charVars.DamageCount = 1;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float nextBuffVars_DamageCount;
            Particle a; // UNUSED
            nextBuffVars_DamageCount = charVars.DamageCount;
            AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyMightOfDemacia(nextBuffVars_DamageCount), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            charVars.DamageCount++;
            charVars.DamageCount = Math.Min(charVars.DamageCount, 20);
            SpellEffectCreate(out a, out _, "poppydam_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "hammer_b", default, target, default, default, false);
        }
    }
}