#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaSunlight : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "LeonaSunlight",
            BuffTextureName = "LeonaSunlight.dds",
        };
        Particle particle1;
        ObjAIBase attacker1; // UNUSED
        TeamId teamIDAttacker; // UNUSED
        int[] effect0 = {20, 20, 35, 35, 50, 50, 65, 65, 80, 80, 95, 95, 110, 110, 125, 125, 140, 140};
        public override void OnActivate()
        {
            Particle particle2; // UNUSED
            if(owner is Champion)
            {
                SpellEffectCreate(out this.particle1, out particle2, "Leona_Sunlight_Champion.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle1, out particle2, "Leona_Sunlight.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle1);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster;
            TeamId teamIDAttacker;
            TeamId teamIDCaster;
            int level;
            float sunlightDamage;
            bool sunglasses;
            Particle motaExplosion; // UNUSED
            if(attacker is Champion)
            {
                caster = SetBuffCasterUnit();
                if(caster != attacker)
                {
                    teamIDAttacker = GetTeamID(attacker);
                    teamIDCaster = GetTeamID(caster);
                    if(teamIDAttacker == teamIDCaster)
                    {
                        level = GetLevel(caster);
                        sunlightDamage = this.effect0[level];
                        sunglasses = TestUnitAttributeFlag(owner, ExtraAttributeFlag.HAS_SUNGLASSES);
                        if(sunglasses)
                        {
                            sunlightDamage--;
                        }
                        this.attacker1 = attacker;
                        this.teamIDAttacker = teamIDAttacker;
                        SpellEffectCreate(out motaExplosion, out _, "LeonaPassive_tar.troy", default, teamIDAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
                        SpellBuffClear(owner, nameof(Buffs.LeonaSunlight));
                        ApplyDamage(attacker, owner, sunlightDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
    }
}