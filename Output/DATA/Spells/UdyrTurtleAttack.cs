#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UdyrTurtleAttack : BBSpellScript
    {
        float[] effect0 = {0.1f, 0.12f, 0.14f, 0.16f, 0.18f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float nextBuffVars_DrainPercent;
                    Particle lifestealParticle; // UNUSED
                    float nextBuffVars_ManaDrainPercent;
                    level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_DrainPercent = this.effect0[level];
                    nextBuffVars_ManaDrainPercent = 0.5f * nextBuffVars_DrainPercent;
                    SpellEffectCreate(out lifestealParticle, out _, "ItemLifesteal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out lifestealParticle, out _, "globalhit_physical.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                    AddBuff(attacker, attacker, new Buffs.GlobalDrainMana(nextBuffVars_DrainPercent, nextBuffVars_ManaDrainPercent), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false, attacker);
        }
    }
}