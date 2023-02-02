#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LeonaShieldOfDaybreakAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        int[] effect1 = {40, 70, 100, 130, 160};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float slowPercent; // UNUSED
            float bonusDamage;
            float supremeDmg;
            float dealtDamage;
            teamID = GetTeamID(attacker);
            slowPercent = this.effect0[level];
            bonusDamage = this.effect1[level];
            supremeDmg = GetTotalAttackDamage(owner);
            dealtDamage = supremeDmg * 1;
            hitResult = false;
            if(target is ObjAIBase)
            {
                Vector3 targetPos;
                Particle temp; // UNUSED
                targetPos = GetUnitPosition(target);
                SpellEffectCreate(out temp, out _, "Leona_ShieldOfDaybreak_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, targetPos, target, default, default, true, default, default, false, false);
                SpellEffectCreate(out temp, out _, "Leona_ShieldOfDaybreak_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 0, false, true, attacker);
                if(target is not BaseTurret)
                {
                    AddBuff(attacker, target, new Buffs.LeonaSunlight(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    ApplyStun(attacker, target, 1);
                }
            }
            else
            {
                ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 0, false, true, attacker);
                ApplyDamage(attacker, target, dealtDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            }
        }
    }
}