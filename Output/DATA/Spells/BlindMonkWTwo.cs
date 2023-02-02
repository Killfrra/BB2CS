#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class BlindMonkWTwo : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 4, 4, 4, 4, 4, },
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {10, 15, 20, 25, 30};
        float[] effect1 = {0.05f, 0.1f, 0.15f, 0.2f, 0.25f};
        public override void SelfExecute()
        {
            float nextBuffVars_TotalArmor;
            float nextBuffVars_LifestealPercent;
            nextBuffVars_TotalArmor = this.effect0[level];
            nextBuffVars_LifestealPercent = this.effect1[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.BlindMonkWTwo(nextBuffVars_TotalArmor, nextBuffVars_LifestealPercent), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.BlindMonkWManager)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.BlindMonkWManager), (ObjAIBase)owner);
            }
        }
    }
}
namespace Buffs
{
    public class BlindMonkWTwo : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "BlindMonkIronWill",
            BuffTextureName = "BlindMonKWTwo.dds",
        };
        float totalArmor;
        float lifestealPercent;
        Particle turntostone;
        public BlindMonkWTwo(float totalArmor = default, float lifestealPercent = default)
        {
            this.totalArmor = totalArmor;
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalArmor);
            //RequireVar(this.lifestealPercent);
            SpellEffectCreate(out this.turntostone, out _, "blindMonk_W_ironWill_armor.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            IncFlatArmorMod(owner, this.totalArmor);
            IncPercentLifeStealMod(owner, this.lifestealPercent);
            IncPercentSpellVampMod(owner, this.lifestealPercent);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.turntostone);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.totalArmor);
            IncPercentLifeStealMod(owner, this.lifestealPercent);
            IncPercentSpellVampMod(owner, this.lifestealPercent);
        }
    }
}