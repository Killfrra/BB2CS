#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrPhoenixAttack : BBSpellScript
    {
        float count;
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            teamID = GetTeamID(attacker);
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false, attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.count++;
            if(target is ObjAIBase)
            {
                if(charVars.Count >= 3)
                {
                    Particle a; // UNUSED
                    Vector3 targetPos;
                    SpellEffectCreate(out a, out _, "PhoenixBreath_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "goatee", default, target, default, default, true);
                    targetPos = GetPointByUnitFacingOffset(owner, 400, 0);
                    SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    charVars.Count = 0;
                }
            }
        }
    }
}