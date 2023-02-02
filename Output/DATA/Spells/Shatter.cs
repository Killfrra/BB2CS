#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Shatter : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 105, 150, 195, 240};
        int[] effect1 = {-10, -15, -20, -25, -30};
        public override void SelfExecute()
        {
            TeamId teamID;
            Particle hi; // UNUSED
            teamID = GetTeamID(owner);
            SpellEffectCreate(out hi, out _, "Shatter_nova.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.ShatterSelfBonus), (ObjAIBase)owner, 0);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_ArmorReduction;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
            nextBuffVars_ArmorReduction = this.effect1[level];
            AddBuff(attacker, target, new Buffs.Shatter(nextBuffVars_ArmorReduction), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Shatter : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shatter",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float armorReduction;
        public Shatter(float armorReduction = default)
        {
            this.armorReduction = armorReduction;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Particle shatterz; // UNUSED
            teamID = GetTeamID(owner);
            //RequireVar(this.armorReduction);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out shatterz, out _, "Shatter_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out shatterz, out _, "BloodSlash.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorReduction);
        }
    }
}