#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class JudicatorDivineBlessing : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ null, "root", },
            AutoBuffActivateEffect = new[]{ "InterventionHeal_buf.troy", "Interventionspeed_buf.troy", },
            BuffName = "JudicatorDivineBlessing",
            BuffTextureName = "Judicator_AngelicEmbrace.dds",
        };
        float moveSpeedMod;
        public JudicatorDivineBlessing(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedMod);
        }
    }
}
namespace Spells
{
    public class JudicatorDivineBlessing : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = {0.15f, 0.17f, 0.19f, 0.21f, 0.23f};
        int[] effect1 = {45, 85, 125, 165, 205};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float abilityPower;
            float healLevel;
            float healAmount;
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            AddBuff(attacker, target, new Buffs.JudicatorDivineBlessing(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            abilityPower = GetFlatMagicDamageMod(owner);
            healLevel = this.effect1[level];
            abilityPower *= 0.35f;
            healAmount = healLevel + abilityPower;
            IncHealth(target, healAmount, owner);
            ApplyAssistMarker(attacker, target, 10);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KayleDivineBlessingAnim(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}