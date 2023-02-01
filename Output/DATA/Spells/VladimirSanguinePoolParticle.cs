﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class VladimirSanguinePoolParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Rumble_Junkyard Titan2.dds",
        };
        Particle temp;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.temp, out _, "Vlad_SaguinePool_Skin_Swap.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.temp);
        }
    }
}