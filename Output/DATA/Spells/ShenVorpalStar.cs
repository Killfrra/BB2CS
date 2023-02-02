#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShenVorpalStar : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {70, 115, 140, 175, 210};
        float[] effect1 = {6, 8.66f, 11.33f, 14, 16.66f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float nextBuffVars_LifeTapMod;
            Particle hit; // UNUSED
            teamID = GetTeamID(attacker);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.75f, 0, false, false, attacker);
            nextBuffVars_LifeTapMod = this.effect1[level];
            AddBuff(attacker, target, new Buffs.ShenVorpalStar(nextBuffVars_LifeTapMod), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            SpellEffectCreate(out hit, out _, "shen_vorpalStar_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class ShenVorpalStar : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Vorpal Star",
            BuffTextureName = "Shen_VorpalBlade.dds",
        };
        float lifeTapMod;
        Particle slow;
        public ShenVorpalStar(float lifeTapMod = default)
        {
            this.lifeTapMod = lifeTapMod;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            //RequireVar(this.lifeTapMod);
            SpellEffectCreate(out this.slow, out _, "shen_life_tap_tar_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.slow);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(attacker is Champion)
            {
                ObjAIBase caster;
                float nextBuffVars_LifeTapMod;
                caster = SetBuffCasterUnit();
                nextBuffVars_LifeTapMod = this.lifeTapMod;
                AddBuff(caster, attacker, new Buffs.ShenVorpalStarHeal(nextBuffVars_LifeTapMod), 1, 1, 2.9f, BuffAddType.REPLACE_EXISTING, BuffType.HEAL, 0, true, false, false);
            }
        }
    }
}