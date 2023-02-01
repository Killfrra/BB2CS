﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HideInShadowsBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "r_hand", "l_hand", "", },
            AutoBuffActivateEffect = new[]{ "twitch_ambush_buf.troy", "twitch_ambush_buf.troy", "", },
            BuffName = "Hide Enrage",
            BuffTextureName = "Twitch_AlterEgo.dds",
        };
        float attackSpeedMod;
        public HideInShadowsBuff(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
        }
    }
}