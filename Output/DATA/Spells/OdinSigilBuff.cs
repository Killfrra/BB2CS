#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinSigilBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "OdinRedBuff",
            BuffTextureName = "48thSlave_WaveOfLoathing.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        public override void OnActivate()
        {
            IncScaleSkinCoef(0.25f, owner);
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.25f, owner);
        }
        public override void OnPreDealDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            string targetName;
            targetName = GetUnitSkinName(target);
            if(targetName != "OdinChaosGuardian")
            {
                if(targetName != "OdinOrderGuardian")
                {
                    if(targetName != "OdinNeutralGuardian")
                    {
                        if(targetName != "OdinShrineBomb")
                        {
                            damageAmount *= 1.4f;
                        }
                    }
                }
            }
        }
    }
}