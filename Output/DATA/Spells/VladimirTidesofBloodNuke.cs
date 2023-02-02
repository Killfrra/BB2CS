#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class VladimirTidesofBloodNuke : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {60, 90, 120, 150, 180};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float multiplier;
            float baseDamage;
            float finalDamage;
            TeamId teamID;
            int vladimirSkinID;
            Particle a; // UNUSED
            multiplier = charVars.NumTideStacks * 0.25f;
            multiplier++;
            baseDamage = this.effect0[level];
            finalDamage = baseDamage * multiplier;
            BreakSpellShields(target);
            ApplyDamage((ObjAIBase)owner, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.45f, 1, false, false, attacker);
            teamID = GetTeamID(owner);
            vladimirSkinID = GetSkinID(owner);
            if(vladimirSkinID == 5)
            {
                SpellEffectCreate(out a, out _, "VladTidesofBlood_BloodKing_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out a, out _, "VladTidesofBlood_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class VladimirTidesofBloodNuke : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
            SpellFXOverrideSkins = new[]{ "BloodkingVladimir", },
        };
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if(health >= 0)
            {
                int count;
                float bonusHealPercent;
                float healRatio;
                float effectiveHeal;
                count = GetBuffCountFromAll(owner, nameof(Buffs.VladimirTidesofBloodCost));
                bonusHealPercent = count * 0.08f;
                healRatio = bonusHealPercent + 1;
                effectiveHeal = healRatio * health;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}