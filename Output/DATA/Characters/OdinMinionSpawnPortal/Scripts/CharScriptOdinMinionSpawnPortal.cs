﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptOdinMinionSpawnPortal : BBCharScript
    {
        public override void OnActivate()
        {
            SetMagicImmune(owner, true);
            SetPhysicalImmune(owner, true);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.OdinMinionPortal(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}