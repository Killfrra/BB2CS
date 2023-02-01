#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ManaBarrier : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "ManaBarrier",
            BuffTextureName = "Blitzcrank_ManaBarrier.dds",
            NonDispellable = true,
            DoOnPreDamageInExpirationOrder = true,
        };
        float manaShield;
        float amountToSubtract;
        Particle asdf1;
        float oldArmorAmount;
        public ManaBarrier(float manaShield = default, float amountToSubtract = default)
        {
            this.manaShield = manaShield;
            this.amountToSubtract = amountToSubtract;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            //RequireVar(this.manaShield);
            //RequireVar(this.amountToSubtract);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.asdf1, out _, "SteamGolemShield.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            IncreaseShield(owner, this.manaShield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.asdf1);
            if(this.manaShield > 0)
            {
                RemoveShield(owner, this.manaShield, true, true);
            }
        }
        public override void OnUpdateActions()
        {
            ReduceShield(owner, this.amountToSubtract, true, true);
            this.manaShield -= this.amountToSubtract;
            this.amountToSubtract = 0;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID;
            this.oldArmorAmount = this.manaShield;
            teamID = GetTeamID(owner);
            if(this.manaShield >= damageAmount)
            {
                this.manaShield -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.manaShield;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            }
            else
            {
                damageAmount -= this.manaShield;
                this.manaShield = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellEffectCreate(out _, out _, "SteamGolemShield_hit.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}