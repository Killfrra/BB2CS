#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SonaSongofDiscordAuraB : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "SonaSongofDiscordAuraB",
            BuffTextureName = "Sona_SongofDiscord.dds",
        };
        float mSBoost;
        public SonaSongofDiscordAuraB(float mSBoost = default)
        {
            this.mSBoost = mSBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.mSBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, this.mSBoost);
        }
    }
}