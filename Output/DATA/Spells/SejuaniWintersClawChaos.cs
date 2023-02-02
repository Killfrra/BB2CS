#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SejuaniWintersClawChaos : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Sejuani_Frost_Arctic.troy", },
            BuffName = "SejuaniFrostArctic",
            BuffTextureName = "Sejuani_Permafrost.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        Particle overhead;
        public SejuaniWintersClawChaos(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            SpellBuffRemove(owner, nameof(Buffs.SejuaniFrostChaos), caster, 0);
            //RequireVar(this.movementSpeedMod);
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
            SpellEffectCreate(out this.overhead, out _, "Sejuani_Frost_Arctic_Overhead.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, true, owner, default, default, attacker, "Bird_head", default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            AttackableUnit caster; // UNITIALIZED
            SpellEffectRemove(this.overhead);
            if(GetBuffCountFromCaster(owner, caster, nameof(Buffs.SejuaniFrostTracker)) > 0)
            {
                float duration;
                float nextBuffVars_MovementSpeedMod;
                duration = GetBuffRemainingDuration(owner, nameof(Buffs.SejuaniFrostTracker));
                SpellBuffRemove(owner, nameof(Buffs.SejuaniFrostTracker), attacker, 0);
                nextBuffVars_MovementSpeedMod = -0.1f;
                AddBuff(attacker, owner, new Buffs.SejuaniFrostChaos(nextBuffVars_MovementSpeedMod), 1, 1, duration, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
        }
    }
}