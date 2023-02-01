#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FerociousHowl : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "pelvis", },
            AutoBuffActivateEffect = new[]{ "minatuar_unbreakableWill_cas.troy", "feroscioushowl_cas2.troy", },
            BuffName = "Ferocious Howl",
            BuffTextureName = "Minotaur_FerociousHowl.dds",
        };
        float damageReduction;
        float bonusDamage;
        public FerociousHowl(float damageReduction = default, float bonusDamage = default)
        {
            this.damageReduction = damageReduction;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageReduction);
            //RequireVar(this.bonusDamage);
        }
        public override void OnUpdateStats()
        {
            IncPercentMagicReduction(owner, this.damageReduction);
            IncPercentPhysicalReduction(owner, this.damageReduction);
            IncFlatPhysicalDamageMod(owner, this.bonusDamage);
        }
    }
}
namespace Spells
{
    public class FerociousHowl : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 120f, 100f, 80f, 10f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.5f, 0.6f, 0.7f};
        int[] effect1 = {60, 75, 90};
        int[] effect2 = {7, 7, 7};
        public override void SelfExecute()
        {
            float nextBuffVars_DamageReduction;
            float nextBuffVars_bonusDamage;
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            nextBuffVars_DamageReduction = this.effect0[level];
            nextBuffVars_bonusDamage = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.FerociousHowl(nextBuffVars_DamageReduction, nextBuffVars_bonusDamage), 1, 1, this.effect2[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.AlistarTrample(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}