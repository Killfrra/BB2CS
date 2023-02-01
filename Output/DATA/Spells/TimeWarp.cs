#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TimeWarp : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ChronoClockFast_tar.troy", "Global_Haste.troy", },
            BuffName = "Time Warp",
            BuffTextureName = "Chronokeeper_Haste.dds",
        };
        float speedMod;
        public TimeWarp(float speedMod = default)
        {
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.speedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.speedMod);
        }
    }
}
namespace Spells
{
    public class TimeWarp : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {2.5f, 3.25f, 4, 4.75f, 5.5f};
        float[] effect1 = {2.5f, 3.25f, 4, 4.75f, 5.5f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_SpeedMod;
            int nextBuffVars_AttackSpeedMod;
            if(target.Team == owner.Team)
            {
                nextBuffVars_SpeedMod = 0.55f;
                AddBuff(attacker, target, new Buffs.TimeWarp(nextBuffVars_SpeedMod), 1, 1, this.effect0[level], BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true);
            }
            else
            {
                nextBuffVars_AttackSpeedMod = 0;
                nextBuffVars_SpeedMod = -0.55f;
                AddBuff(attacker, target, new Buffs.TimeWarpSlow(nextBuffVars_SpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, this.effect1[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
            }
        }
    }
}