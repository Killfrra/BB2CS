#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandBasicAttack2 : BBSpellScript
    {
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            int brandSkinID;
            TeamId teamID;
            Particle a; // UNUSED
            baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if(target is ObjAIBase)
            {
                brandSkinID = GetSkinID(owner);
                teamID = GetTeamID(owner);
                if(brandSkinID == 3)
                {
                    SpellEffectCreate(out a, out _, "BrandBasicAttack_Frost_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                }
                else
                {
                    SpellEffectCreate(out a, out _, "BrandBasicAttack_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "Spine", default, target, default, default, true, false, false, false, false);
                }
            }
        }
    }
}