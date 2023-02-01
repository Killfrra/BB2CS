#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SwainDampeningFieldMana : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "SwainDampeningFieldMana",
            BuffTextureName = "SwainCarrionRenewal.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float manaRegen;
        int[] effect0 = {10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30};
        public override void OnActivate()
        {
            this.manaRegen = 10;
            SetBuffToolTipVar(1, this.manaRegen);
        }
        public override void OnKill()
        {
            Particle particle; // UNUSED
            SpellEffectCreate(out particle, out _, "NeutralMonster_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            IncPAR(owner, this.manaRegen, PrimaryAbilityResourceType.MANA);
        }
        public override void OnLevelUp()
        {
            int level;
            level = GetLevel(owner);
            this.manaRegen = this.effect0[level];
            SetBuffToolTipVar(1, this.manaRegen);
        }
    }
}