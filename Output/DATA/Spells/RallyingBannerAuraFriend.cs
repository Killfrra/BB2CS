#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RallyingBannerAuraFriend : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Stark's Fervor Aura",
            BuffTextureName = "3050_Rallying_Banner.dds",
        };
        float lifeStealMod;
        float attackSpeedMod;
        float healthRegenMod;
        Particle starkAuraParticle;
        public RallyingBannerAuraFriend(float lifeStealMod = default, float attackSpeedMod = default, float healthRegenMod = default)
        {
            this.lifeStealMod = lifeStealMod;
            this.attackSpeedMod = attackSpeedMod;
            this.healthRegenMod = healthRegenMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lifeStealMod);
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.healthRegenMod);
            SpellEffectCreate(out this.starkAuraParticle, out _, "RallyingBanner_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, true, target, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.starkAuraParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, this.attackSpeedMod);
            IncPercentLifeStealMod(owner, this.lifeStealMod);
            IncFlatHPRegenMod(owner, this.healthRegenMod);
        }
    }
}