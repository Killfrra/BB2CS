#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class KogMawBasicAttack : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            int kMSkinID;
            baseAttackDamage = GetBaseAttackDamage(owner);
            kMSkinID = GetSkinID(attacker);
            if(target is ObjAIBase)
            {
                Particle a; // UNUSED
                if(kMSkinID == 5)
                {
                    SpellEffectCreate(out a, out _, "KogMawChineseBasicAttack_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "KogMawSpatter.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
                }
            }
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}