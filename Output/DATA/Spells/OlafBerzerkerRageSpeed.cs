﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafBerzerkerRageSpeed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OlafBerzerkerRageSpeed",
            BuffTextureName = "DarkChampion_Fury.dds",
            NonDispellable = true,
        };
        float attackSpeed;
        public OlafBerzerkerRageSpeed(float attackSpeed = default)
        {
            this.attackSpeed = attackSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeed);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeed);
        }
    }
}