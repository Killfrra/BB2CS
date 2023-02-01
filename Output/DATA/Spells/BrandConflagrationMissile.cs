#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BrandConflagrationMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {70, 105, 140, 175, 210};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int brandSkinID;
            TeamId teamID;
            Particle varA; // UNUSED
            brandSkinID = GetSkinID(attacker);
            teamID = GetTeamID(attacker);
            if(brandSkinID == 3)
            {
                SpellEffectCreate(out varA, out _, "BrandConflagration_tar_frost.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out varA, out _, "BrandConflagration_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.55f, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.BrandAblaze(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
        }
    }
}