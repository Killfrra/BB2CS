#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Fear : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Fear",
            BuffTextureName = "Fiddlesticks_Terrify.dds",
            PopupMessage = new[]{ "game_floatingtext_Feared", },
        };
        Particle a;
        Particle confetti;
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            SetFeared(owner, true);
            SetCanCast(owner, false);
            ApplyAssistMarker(attacker, owner, 10);
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.Fear)) > 0)
            {
                teamID = GetTeamID(attacker);
                fiddlesticksSkinID = GetSkinID(attacker);
                if(fiddlesticksSkinID == 6)
                {
                    SpellEffectCreate(out this.a, out _, "GlobalFear_Surprise.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                    SpellEffectCreate(out this.confetti, out _, "Party_HornConfetti_Instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "BUFFBONE_CSTM_HORN", default, attacker, default, default, false, false, false, false, true);
                }
                else
                {
                    SpellEffectCreate(out this.a, out _, "Global_Fear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
                }
            }
            else
            {
                SpellEffectCreate(out this.a, out _, "Global_Fear.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID; // UNUSED
            int fiddlesticksSkinID;
            SetFeared(owner, false);
            SetCanCast(owner, true);
            SpellEffectRemove(this.a);
            teamID = GetTeamID(attacker);
            fiddlesticksSkinID = GetSkinID(attacker);
            if(fiddlesticksSkinID == 6)
            {
                SpellEffectRemove(this.confetti);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanCast(owner, false);
            IncPercentMultiplicativeMovementSpeedMod(owner, -0.4f);
        }
    }
}