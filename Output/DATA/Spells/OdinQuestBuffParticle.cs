﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinQuestBuffParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterShrineBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "odin_quest_complete.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
    }
}