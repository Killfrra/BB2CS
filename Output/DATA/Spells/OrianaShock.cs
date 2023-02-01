#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaShock : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        object level;
        public OrianaShock(object level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            object level; // UNUSED
            TeamId casterTeam;
            Particle temp; // UNUSED
            object nextBuffVars_Level;
            //RequireVar(this.level);
            level = this.level;
            casterTeam = GetTeamID(attacker);
            SpellEffectCreate(out temp, out _, "Oriana_ts_tar.troy", default, casterTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            nextBuffVars_Level = this.level;
            AddBuff(attacker, target, new Buffs.OrianaShred(nextBuffVars_Level), 5, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
        }
    }
}