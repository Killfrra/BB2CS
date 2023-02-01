#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Wither : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "GLOBAL_SLOW.TROY", "nassus_wither_tar.troy", "", },
            BuffName = "Wither",
            BuffTextureName = "Nasus_Wither.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float bonusSpeedMod;
        float speedMod;
        float duration;
        float timeBetweenTicks;
        float lastTimeExecuted;
        public Wither(float bonusSpeedMod = default, float speedMod = default)
        {
            this.bonusSpeedMod = bonusSpeedMod;
            this.speedMod = speedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusSpeedMod);
            //RequireVar(this.speedMod);
            ApplyAssistMarker(attacker, owner, 10);
            this.duration = GetBuffRemainingDuration(target, nameof(Buffs.Wither));
            this.timeBetweenTicks = this.duration / 5;
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, this.timeBetweenTicks))
            {
                this.speedMod += this.bonusSpeedMod;
            }
            IncPercentMultiplicativeAttackSpeedMod(owner, this.speedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.speedMod);
        }
    }
}
namespace Spells
{
    public class Wither : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {-0.35f, -0.35f, -0.35f, -0.35f, -0.35f};
        float[] effect1 = {-0.03f, -0.06f, -0.09f, -0.12f, -0.15f};
        int[] effect2 = {5, 5, 5, 5, 5};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_SpeedMod;
            float nextBuffVars_BonusSpeedMod;
            nextBuffVars_SpeedMod = this.effect0[level];
            nextBuffVars_BonusSpeedMod = this.effect1[level];
            AddBuff(attacker, target, new Buffs.Wither(nextBuffVars_BonusSpeedMod, nextBuffVars_SpeedMod), 1, 1, this.effect2[level], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}