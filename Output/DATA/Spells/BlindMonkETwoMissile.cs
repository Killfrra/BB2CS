#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkETwoMissile : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "blindMonk_E_tempestFist_cripple.troy", "blindMonk_E_tempestFist_cripple_02.troy", },
            BuffName = "BlindMonkCripple",
            BuffTextureName = "BlindMonkETwo.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float percentReduction;
        float initialPercentReduction;
        int count;
        float lastTimeExecuted;
        public BlindMonkETwoMissile(float percentReduction = default)
        {
            this.percentReduction = percentReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.percentReduction);
            this.initialPercentReduction = this.percentReduction;
            ApplyAssistMarker(attacker, owner, 10);
            this.count = 0;
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                if(this.count == 0)
                {
                    this.percentReduction = this.initialPercentReduction * 0.75f;
                    this.count = 1;
                }
                else if(this.count == 1)
                {
                    this.percentReduction = this.initialPercentReduction * 0.5f;
                    this.count = 2;
                }
                else
                {
                    this.percentReduction = this.initialPercentReduction * 0.25f;
                }
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, this.percentReduction);
            IncPercentMultiplicativeAttackSpeedMod(owner, this.percentReduction);
        }
    }
}
namespace Spells
{
    public class BlindMonkETwoMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = {-0.3f, -0.375f, -0.45f, -0.525f, -0.6f};
        int[] effect1 = {4, 4, 4, 4, 4};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_PercentReduction;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_PercentReduction = this.effect0[level];
            AddBuff(attacker, target, new Buffs.BlindMonkETwoMissile(nextBuffVars_PercentReduction), 1, 1, this.effect1[level], BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
        }
    }
}