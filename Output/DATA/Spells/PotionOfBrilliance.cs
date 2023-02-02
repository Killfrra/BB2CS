#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PotionOfBrilliance : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PotionOfBrilliance(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class PotionOfBrilliance : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofBrilliance_itm.troy", },
            BuffName = "Elixer of Brilliance",
            BuffTextureName = "2039_Potion_of_Brilliance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusAP;
        public override void OnActivate()
        {
            int level;
            float bonusAP;
            level = GetLevel(owner);
            bonusAP = level * 1.18f;
            this.bonusAP = bonusAP + 18.82f;
            IncPermanentFlatMagicDamageMod(owner, this.bonusAP);
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusAP;
            bonusAP = -1 * this.bonusAP;
            IncPermanentFlatMagicDamageMod(owner, bonusAP);
            IncPermanentPercentCooldownMod(owner, 0.1f);
        }
    }
}