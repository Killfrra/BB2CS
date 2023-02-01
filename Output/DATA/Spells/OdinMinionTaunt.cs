#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinMinionTaunt : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "odin_minion_tower_buf.troy", "", "", },
            BuffName = "OdinMinionTaunt",
            BuffTextureName = "Soraka_Bless.dds",
        };
        float magicResistBuff;
        float armorBuff;
        float damageTakenFromGuardian;
        float moveSpeedBuff;
        public OdinMinionTaunt(float magicResistBuff = default, float armorBuff = default)
        {
            this.magicResistBuff = magicResistBuff;
            this.armorBuff = armorBuff;
        }
        public override void OnActivate()
        {
            this.damageTakenFromGuardian = 0.8f;
            this.moveSpeedBuff = 0.25f;
            ApplyTaunt(attacker, owner, 1);
            //RequireVar(this.armorBuff);
            //RequireVar(this.magicResistBuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, this.moveSpeedBuff);
            IncFlatSpellBlockMod(owner, this.magicResistBuff);
            IncFlatArmorMod(owner, this.armorBuff);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            string attackerSkinName; // UNUSED
            float damageMultiplier;
            TeamId attackerTeam;
            attackerSkinName = GetUnitSkinName(attacker);
            damageMultiplier = 1;
            if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.OdinGuardianBuff)) > 0)
            {
                attackerTeam = GetTeamID(attacker);
                if(attackerTeam != TeamId.TEAM_NEUTRAL)
                {
                    damageMultiplier = this.damageTakenFromGuardian;
                }
            }
            damageAmount *= damageMultiplier;
        }
    }
}