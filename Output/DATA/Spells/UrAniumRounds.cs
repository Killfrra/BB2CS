#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrAniumRounds : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrAniumRounds",
            BuffTextureName = "Heimerdinger_Level2Turret.dds",
        };
        public override void OnSpellHit()
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    TeamId teamID;
                    teamID = GetTeamID(owner);
                    attacker = GetChampionBySkinName("Heimerdinger", teamID ?? TeamId.TEAM_UNKNOWN);
                    AddBuff(attacker, target, new Buffs.UrAniumRoundsHit(), 25, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    if(GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UpgradeSlow)) > 0)
                    {
                        Particle faas; // UNUSED
                        SpellEffectCreate(out faas, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                    }
                }
            }
        }
    }
}