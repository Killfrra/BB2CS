#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PotionOfGiantStrength : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofGiantStrength_itm.troy", },
            BuffName = "Elixer of Fortitude",
            BuffTextureName = "2037_Potion_of_Giant_Strength.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusHealth;
        public override void OnActivate()
        {
            int level;
            float bonusHealth;
            level = GetLevel(owner);
            bonusHealth = level * 5.59f;
            this.bonusHealth = bonusHealth + 134.41f;
            IncPermanentFlatHPPoolMod(owner, this.bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusHealth;
            bonusHealth = -1 * this.bonusHealth;
            IncPermanentFlatHPPoolMod(owner, bonusHealth);
            IncPermanentFlatPhysicalDamageMod(owner, -10);
        }
    }
}
namespace Spells
{
    public class PotionOfGiantStrength : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.PotionOfGiantStrength(), 1, 1, 240, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}