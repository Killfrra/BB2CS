﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class GateFix2 : BBBuffScript
    {
        float lastTimeExecuted;
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.GateFix(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(3, ref this.lastTimeExecuted, true))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.GateFix(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            }
        }
    }
}