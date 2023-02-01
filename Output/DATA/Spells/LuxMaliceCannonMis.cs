#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class LuxMaliceCannonMis : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {300, 400, 500};
        public override void SelfExecute()
        {
            Vector3 damagePoint;
            TeamId teamID;
            Particle motaExplosion; // UNUSED
            Particle asdf; // UNUSED
            damagePoint = GetPointByUnitFacingOffset(owner, 1650, 0);
            teamID = GetTeamID(attacker);
            foreach(AttackableUnit unit in GetUnitsInRectangle(owner, damagePoint, 100, 1700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                BreakSpellShields(unit);
                if(GetBuffCountFromCaster(unit, attacker, nameof(Buffs.LuxIlluminatingFraulein)) > 0)
                {
                    teamID = GetTeamID(unit);
                    ApplyDamage(attacker, unit, charVars.IlluminateDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    SpellEffectCreate(out motaExplosion, out _, "LuxPassive_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false);
                    SpellBuffRemove(unit, nameof(Buffs.LuxIlluminatingFraulein), attacker);
                }
                SpellEffectCreate(out asdf, out _, "LuxMaliceCannon_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                if(unit is not BaseTurret)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.LuxIlluminatingFraulein(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 damagePoint; // UNUSED
            damagePoint = GetPointByUnitFacingOffset(owner, 1650, 0);
        }
    }
}