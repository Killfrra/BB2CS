#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinShamanBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[]{ "bloodboil_buf.troy", "bloodboil_buf.troy", },
            BuffName = "OdinShamanBuff",
            BuffTextureName = "Sona_SongofDiscordGold.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetScaleSkinCoef(1.15f, owner);
        }
        public override void OnUpdateStats()
        {
            SetScaleSkinCoef(1.15f, owner);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            string targetName;
            float damageMultiplier;
            targetName = GetUnitSkinName(target);
            damageMultiplier = 1.5f;
            if(targetName == "OdinChaosGuardian")
            {
                damageMultiplier = 1;
            }
            if(targetName == "OdinOrderGuardian")
            {
                damageMultiplier = 1;
            }
            if(targetName == "OdinNeutralGuardian")
            {
                damageMultiplier = 1;
            }
            damageAmount *= damageMultiplier;
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            damageAmount *= 0.5f;
        }
    }
}