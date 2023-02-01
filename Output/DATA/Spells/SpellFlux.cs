#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SpellFlux : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spell Flux",
            BuffTextureName = "Ryze_LightningFlux.dds",
        };
        float resistanceMod;
        public SpellFlux(float resistanceMod = default)
        {
            this.resistanceMod = resistanceMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.resistanceMod);
            IncFlatSpellBlockMod(owner, this.resistanceMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, this.resistanceMod);
        }
    }
}
namespace Spells
{
    public class SpellFlux : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 150f, 120f, 90f, },
            CastingBreaksStealth = true,
            ChainMissileParameters = new()
            {
                CanHitCaster = 1,
                CanHitEnemies = 1,
                CanHitFriends = 0,
                CanHitSameTarget = 1,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 5, 5, 5, 5, 5, },
            },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {-12, -15, -18, -21, -24};
        int[] effect1 = {50, 70, 90, 110, 130};
        float[] effect2 = {0.5f, 0.5f, 0.5f, 0.5f, 0.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_ResistanceMod;
            float damage;
            float aoEDamage;
            float ultDamage;
            Particle asdf; // UNUSED
            Particle part; // UNUSED
            Particle part2; // UNUSED
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(attacker);
            nextBuffVars_ResistanceMod = this.effect0[level];
            damage = this.effect1[level];
            aoEDamage = this.effect2[level];
            ultDamage = damage * aoEDamage;
            SpellEffectCreate(out asdf, out _, "SpellFlux_tar2.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            if(target != owner)
            {
                AddBuff(attacker, target, new Buffs.SpellFlux(nextBuffVars_ResistanceMod), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DesperatePower)) > 0)
                {
                    SpellEffectCreate(out part, out _, "DesperatePower_aoe.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, target.Position, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        if(target != unit)
                        {
                            SpellEffectCreate(out part2, out _, "ManaLeach_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, ultDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.175f, 1, false, false, attacker);
                        }
                    }
                }
            }
        }
    }
}