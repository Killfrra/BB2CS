#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ExaltedWithBaronNashor : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Exalted with Baron Nashor",
            BuffTextureName = "Averdrian_AstralBeam.dds",
            NonDispellable = true,
        };
        float bonusAttack;
        Particle buffParticle;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float gameTime;
            float bonusAttack;
            IncPermanentFlatPARRegenMod(owner, 3, PrimaryAbilityResourceType.MANA);
            gameTime = GetGameTime();
            bonusAttack = gameTime / 30;
            bonusAttack -= 15;
            bonusAttack = Math.Min(bonusAttack, 40);
            bonusAttack = Math.Max(bonusAttack, 20);
            this.bonusAttack = bonusAttack;
            IncPermanentFlatMagicDamageMod(owner, bonusAttack);
            IncPermanentFlatPhysicalDamageMod(owner, bonusAttack);
            SpellEffectCreate(out this.buffParticle, out _, "nashor_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusAttack;
            IncPermanentFlatPARRegenMod(owner, -3, PrimaryAbilityResourceType.MANA);
            bonusAttack = -1 * this.bonusAttack;
            IncPermanentFlatPhysicalDamageMod(owner, bonusAttack);
            IncPermanentFlatMagicDamageMod(owner, bonusAttack);
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(5, ref this.lastTimeExecuted, false))
            {
                float health;
                float healthInc;
                Particle particle; // UNUSED
                health = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                healthInc = health * 0.03f;
                IncHealth(owner, healthInc, owner);
                SpellEffectCreate(out particle, out _, "InnervatingLocket_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}