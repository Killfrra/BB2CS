#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SeismicShardBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SeismicShard",
            BuffTextureName = "Malphite_SeismicShard.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        int level;
        Particle sSSlow;
        float value;
        float[] effect0 = {0.14f, 0.17f, 0.2f, 0.23f, 0.26f};
        public SeismicShardBuff(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            SpellEffectCreate(out this.sSSlow, out _, "Global_Slow.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            this.value = GetMovementSpeed(owner);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.sSSlow);
        }
        public override void OnUpdateStats()
        {
            int level;
            float modifier;
            float result;
            level = this.level;
            modifier = this.effect0[level];
            result = this.value * modifier;
            IncFlatMovementSpeedMod(attacker, result);
            result *= -1;
            IncFlatMovementSpeedMod(owner, result);
        }
    }
}