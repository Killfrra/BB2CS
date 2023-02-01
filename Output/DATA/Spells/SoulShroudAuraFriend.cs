#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SoulShroudAuraFriend : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Soul Shroud Aura Friend",
            BuffTextureName = "3063_Soul_Shroud.dds",
        };
        float manaRegenMod;
        float cooldownReduction;
        Particle apocalypseParticle;
        public SoulShroudAuraFriend(float manaRegenMod = default, float cooldownReduction = default)
        {
            this.manaRegenMod = manaRegenMod;
            this.cooldownReduction = cooldownReduction;
        }
        public override void OnActivate()
        {
            //RequireVar(this.cooldownReduction);
            //RequireVar(this.manaRegenMod);
            SpellEffectCreate(out this.apocalypseParticle, out _, "ZettasManaManipulator_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.apocalypseParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, this.cooldownReduction);
            IncFlatPARRegenMod(owner, this.manaRegenMod, PrimaryAbilityResourceType.MANA);
        }
    }
}