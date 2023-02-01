#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaRedactShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", },
            AutoBuffActivateEffect = new[]{ "OrianaProtectShield.troy", },
            BuffName = "OrianaRedactShield",
            BuffTextureName = "OriannaCommandRedact.dds",
            OnPreDamagePriority = 3,
            DoOnPreDamageInExpirationOrder = true,
        };
        float damageBlock;
        bool willRemove;
        float oldArmorAmount;
        public OrianaRedactShield(float damageBlock = default, bool willRemove = default)
        {
            this.damageBlock = damageBlock;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBlock);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, this.damageBlock, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            Particle ar; // UNUSED
            if(!this.willRemove)
            {
                SpellEffectCreate(out ar, out _, "OrianaProtectShield.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
            }
            if(this.damageBlock > 0)
            {
                RemoveShield(owner, this.damageBlock, true, true);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            TeamId teamID; // UNUSED
            this.oldArmorAmount = this.damageBlock;
            if(this.damageBlock >= damageAmount)
            {
                this.damageBlock -= damageAmount;
                damageAmount = 0;
                this.oldArmorAmount -= this.damageBlock;
                ReduceShield(owner, this.oldArmorAmount, true, true);
            }
            else
            {
                teamID = GetTeamID(owner);
                damageAmount -= this.damageBlock;
                this.damageBlock = 0;
                ReduceShield(owner, this.oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}