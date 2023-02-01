#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlindMonkWOneShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "blindMonk_W_shield_self.troy", "", },
            BuffName = "BlindMonkSafeguard",
            BuffTextureName = "BlindMonkWOne.dds",
            OnPreDamagePriority = 3,
        };
        float shieldAbsorb;
        bool willRemove;
        float oldArmorAmount;
        public BlindMonkWOneShield(float shieldAbsorb = default, bool willRemove = default)
        {
            this.shieldAbsorb = shieldAbsorb;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldAbsorb);
            SetBuffToolTipVar(1, this.shieldAbsorb);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, this.shieldAbsorb, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle ar; // UNUSED
            if(!this.willRemove)
            {
                SpellEffectCreate(out ar, out _, "blindMonk_W_shield_self_deactivate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            if(this.shieldAbsorb > 0)
            {
                RemoveShield(owner, this.shieldAbsorb, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster; // UNUSED
            TeamId teamID;
            Particle ar; // UNUSED
            this.oldArmorAmount = this.shieldAbsorb;
            if(this.shieldAbsorb >= damageAmount)
            {
                this.shieldAbsorb -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.shieldAbsorb;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                caster = SetBuffCasterUnit();
                teamID = GetTeamID(owner);
                damageAmount -= this.shieldAbsorb;
                this.shieldAbsorb = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellEffectCreate(out ar, out _, "blindMonk_W_shield_block.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}