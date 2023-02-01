#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PotionOfElusiveness : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "PotionofElusiveness_itm.troy", },
            BuffName = "Elixer of Elusiveness",
            BuffTextureName = "2038_Potion_of_Elusiveness.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusAttackSpeed;
        public override void OnActivate()
        {
            int level;
            float bonusAttackSpeed;
            level = GetLevel(owner);
            bonusAttackSpeed = level * 0.0059f;
            this.bonusAttackSpeed = bonusAttackSpeed + 0.114f;
            IncPermanentPercentAttackSpeedMod(owner, this.bonusAttackSpeed);
            IncPermanentFlatCritChanceMod(owner, 0.08f);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusAttackSpeed;
            bonusAttackSpeed = -1 * this.bonusAttackSpeed;
            IncPermanentPercentAttackSpeedMod(owner, bonusAttackSpeed);
            IncPermanentFlatCritChanceMod(owner, -0.08f);
        }
    }
}
namespace Spells
{
    public class PotionOfElusiveness : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PotionOfElusiveness(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}