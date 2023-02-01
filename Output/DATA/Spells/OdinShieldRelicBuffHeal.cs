#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShieldRelicBuffHeal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinShieldRelic",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
        };
        int[] effect0 = {90, 99, 108, 117, 126, 135, 144, 153, 162, 171, 180, 189, 198, 207, 216, 225, 234, 243};
        public override void OnActivate()
        {
            int level;
            float healAmount;
            float manaAmount;
            TeamId teamID;
            Particle asdf; // UNUSED
            level = GetLevel(owner);
            healAmount = this.effect0[level];
            manaAmount = healAmount * 0.6f;
            IncPAR(owner, manaAmount, PrimaryAbilityResourceType.MANA);
            IncPAR(owner, 20, PrimaryAbilityResourceType.Energy);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinPlayerBuff)) > 0)
            {
                healAmount *= 1.25f;
            }
            IncHealth(owner, healAmount, owner);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out asdf, out _, "Odin_HealthPackHeal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out asdf, out _, "Summoner_Mana.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, false, false, false, false, false);
            SpellBuffClear(owner, nameof(Buffs.OdinShieldRelicBuffHeal));
        }
    }
}