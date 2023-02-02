#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrBearAttack : BBSpellScript
    {
        int[] effect0 = {5, 5, 5, 5, 5};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.UdyrBearStunCheck)) > 0)
                    {
                    }
                    else
                    {
                        Particle c; // UNUSED
                        AddBuff(attacker, target, new Buffs.UdyrBearStunCheck(), 1, 1, this.effect0[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
                        BreakSpellShields(target);
                        ApplyStun(attacker, target, 1);
                        level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        SpellEffectCreate(out c, out _, "udyr_bear_slam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                    }
                }
            }
        }
    }
}