#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AstralBlessing : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "soraka_astralBless_buf.troy", },
            BuffName = "Astral Blessing",
            BuffTextureName = "Soraka_Bless.dds",
        };
        float astralArmor;
        public AstralBlessing(float astralArmor = default)
        {
            this.astralArmor = astralArmor;
        }
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            //RequireVar(this.astralArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.astralArmor);
        }
    }
}
namespace Spells
{
    public class AstralBlessing : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {70, 140, 210, 280, 350};
        int[] effect1 = {25, 50, 75, 100, 125};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float tempAbilityPower;
            float healthToRestore;
            float nextBuffVars_AstralArmor;
            float healingBonus;
            tempAbilityPower = GetFlatMagicDamageMod(owner);
            healthToRestore = this.effect0[level];
            nextBuffVars_AstralArmor = this.effect1[level];
            healingBonus = tempAbilityPower * 0.45f;
            healthToRestore += healingBonus;
            AddBuff(attacker, target, new Buffs.AstralBlessing(nextBuffVars_AstralArmor), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            IncHealth(target, healthToRestore, owner);
        }
    }
}