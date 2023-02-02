#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaRedactDamage : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        float totalDamage;
        public OrianaRedactDamage(float totalDamage = default)
        {
            this.totalDamage = totalDamage;
        }
        public override void OnActivate()
        {
            TeamId casterTeam;
            Particle temp; // UNUSED
            //RequireVar(this.totalDamage);
            casterTeam = GetTeamID(attacker);
            BreakSpellShields(owner);
            SpellEffectCreate(out temp, out _, "OrianaRedact_tar.troy", default, casterTeam ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            ApplyDamage(attacker, owner, this.totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}