#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class LeonaSolarBarrier2 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", },
            BuffName = "LeonaSolarBarrier",
            BuffTextureName = "LeonaSolarBarrier.dds",
            SpellToggleSlot = 2,
        };
        float defenseBonus;
        Particle particle;
        public LeonaSolarBarrier2(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            //RequireVar(this.defenseBonus);
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
            SpellEffectCreate(out this.particle, out _, "Leona_SolarBarrier2_buf.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            OverrideAnimation("Idle1", "Spell2_idle", owner);
            OverrideAnimation("Idle2", "Spell2_idle", owner);
            OverrideAnimation("Idle3", "Spell2_idle", owner);
            OverrideAnimation("Idle4", "Spell2_idle", owner);
            OverrideAnimation("Attack1", "Spell2_attack", owner);
            OverrideAnimation("Attack2", "Spell2_attack", owner);
            OverrideAnimation("Attack3", "Spell2_attack", owner);
            OverrideAnimation("Crit", "Spell2_attack", owner);
            OverrideAnimation("Run", "Spell2_run", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            ClearOverrideAnimation("Idle1", owner);
            ClearOverrideAnimation("Idle2", owner);
            ClearOverrideAnimation("Idle3", owner);
            ClearOverrideAnimation("Idle4", owner);
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Attack3", owner);
            ClearOverrideAnimation("Crit", owner);
            ClearOverrideAnimation("Run", owner);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseBonus);
            IncFlatSpellBlockMod(owner, this.defenseBonus);
        }
    }
}