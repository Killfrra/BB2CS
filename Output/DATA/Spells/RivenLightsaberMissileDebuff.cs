#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RivenLightsaberMissileDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.troy", },
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
        public override void OnActivate()
        {
            TeamId teamID;
            Particle temp; // UNUSED
            Particle temp2; // UNUSED
            teamID = GetTeamID(attacker);
            if(owner is Champion)
            {
                SpellEffectCreate(out temp, out temp2, "exile_ult_mis_tar.troy ", "exile_ult_mis_tar.troy ", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            SpellEffectCreate(out temp, out temp2, "exile_ult_mis_tar_minion.troy ", "exile_ult_mis_tar_minion.troy ", teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}