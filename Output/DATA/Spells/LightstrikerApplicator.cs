#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LightstrikerApplicator : BBBuffScript
    {
        float attackCounter;
        public override void OnActivate()
        {
            this.attackCounter = 0;
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            ObjAIBase caster;
            Particle part; // UNUSED
            this.attackCounter++;
            if(this.attackCounter == 4)
            {
                caster = SetBuffCasterUnit();
                if(attacker is not Champion)
                {
                    caster = GetPetOwner((Pet)attacker);
                }
                ApplyDamage(caster, target, 100, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
                this.attackCounter = 0;
                if(target is ObjAIBase)
                {
                    SpellEffectCreate(out part, out _, "sword_of_the_divine_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                }
            }
        }
    }
}