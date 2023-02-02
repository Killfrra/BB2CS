#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JudicatorReckoning : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {-0.35f, -0.35f, -0.35f, -0.35f, -0.35f};
        int[] effect1 = {60, 110, 160, 210, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float aP;
            float baseAD;
            float totalAD;
            float bonusAD;
            float finalDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            aP = GetFlatMagicDamageMod(owner);
            baseAD = GetBaseAttackDamage(owner);
            totalAD = GetTotalAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            bonusAD *= 1;
            aP *= 1;
            finalDamage = aP + bonusAD;
            ApplyDamage(attacker, target, finalDamage + this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, target, new Buffs.JudicatorReckoning(nextBuffVars_MoveSpeedMod), 100, 1, 4, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class JudicatorReckoning : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Global_Slow.troy", },
            BuffName = "JudicatorReckoning",
            BuffTextureName = "Judicator_Reckoning.dds",
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public JudicatorReckoning(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}