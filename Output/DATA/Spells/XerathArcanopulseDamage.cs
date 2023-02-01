#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class XerathArcanopulseDamage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        Particle particleID; // UNUSED
        Particle particleID2; // UNUSED
        int[] effect0 = {75, 115, 155, 195, 235};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 beam1;
            Vector3 beam3;
            Minion other1;
            Minion other3;
            Vector3 damagePoint;
            Particle asdf; // UNUSED
            Particle asdf2; // UNUSED
            teamOfOwner = GetTeamID(owner);
            beam1 = GetPointByUnitFacingOffset(owner, 145, 0);
            beam3 = GetPointByUnitFacingOffset(owner, 1100, 0);
            other1 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam1, teamOfOwner, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            other3 = SpawnMinion("hiu", "TestCubeRender10Vision", "idle.lua", beam3, teamOfOwner, false, true, false, false, false, true, 1, false, false, (Champion)owner);
            FaceDirection(other1, other3.Position);
            LinkVisibility(other1, other3);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(other3, other1, new Buffs.XerathArcanopulsePartFix2(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.XerathArcanopulseDeath(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetForceRenderParticles(other1, true);
            SetForceRenderParticles(other3, true);
            SpellEffectCreate(out this.particleID, out this.particleID2, "XerathR_beam.troy", "XerathR_beam.troy", teamOfOwner, 550, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other3, "top", default, other1, "top", default, true, false, false, false, false);
            damagePoint = GetPointByUnitFacingOffset(owner, 500, 0);
            foreach(AttackableUnit unit in GetUnitsInRectangle(owner, damagePoint, 95, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
            {
                BreakSpellShields(unit);
                SpellEffectCreate(out asdf, out _, "Xerath_beam_hit.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                ApplyDamage(attacker, unit, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
                if(GetBuffCountFromCaster(unit, owner, nameof(Buffs.XerathMageChains)) > 0)
                {
                    SpellEffectCreate(out asdf2, out _, "Xerath_MageChains_consume.troy", default, teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.XerathMageChainsRoot(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, true, false);
                    SpellBuffRemove(unit, nameof(Buffs.XerathMageChains), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}