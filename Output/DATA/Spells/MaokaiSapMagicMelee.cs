#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MaokaiSapMagicMelee : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "BUFFBONE_CSTM_SHIELDEYE_L", "BUFFBONE_CSTM_SHIELDEYE_R", },
            AutoBuffActivateEffect = new[]{ "maokai_passive_indicator_left_eye.troy", "maokai_passive_indicator_right_eye.troy", },
            BuffName = "MaokaiSapMagicMelee",
            BuffTextureName = "Maokai_SapMagicReady.dds",
        };
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    float healthPercent;
                    healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
                    if(healthPercent < 1)
                    {
                        TeamId teamID;
                        float maxHealth;
                        int level; // UNUSED
                        float regenPercent;
                        float healthToInc;
                        Particle ar; // UNUSED
                        teamID = GetTeamID(owner);
                        maxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
                        level = GetLevel(owner);
                        regenPercent = 0.07f;
                        healthToInc = maxHealth * regenPercent;
                        IncHealth(owner, healthToInc, owner);
                        SpellEffectCreate(out ar, out _, "Maokai_Heal.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        SpellBuffClear(owner, nameof(Buffs.MaokaiSapMagicMelee));
                    }
                }
            }
        }
        public override void OnActivate()
        {
            OverrideAnimation("Attack", "Passive", owner);
            OverrideAnimation("Attack2", "Passive", owner);
            OverrideAnimation("Crit", "Passive", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Attack", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Crit", owner);
        }
    }
}