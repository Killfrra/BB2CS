﻿#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class YorickDecayedDiseaseCloud : BBBuffScript
    {
        float moveSpeedMod;
        Particle diseaseCloud;
        float lastTimeExecuted;
        public YorickDecayedDiseaseCloud(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            SpellEffectCreate(out this.diseaseCloud, out _, "yorick_necroCloud.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.diseaseCloud);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                float nextBuffVars_MoveSpeedMod;
                nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(attacker, unit, new Buffs.YorickDecayedAuraSlow(default, nextBuffVars_MoveSpeedMod), 100, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}