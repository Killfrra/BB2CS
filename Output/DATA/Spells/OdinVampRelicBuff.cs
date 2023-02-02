#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OdinVampRelicBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "PotionofElusiveness_itm.troy", },
            BuffName = "OdinVampRelic",
            BuffTextureName = "2038_Potion_of_Elusiveness.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        float vampVar;
        float spellVampVar;
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            this.vampVar = 0.3f;
            this.spellVampVar = 0.5f;
            IncPercentLifeStealMod(owner, this.vampVar);
            IncPercentSpellVampMod(owner, this.spellVampVar);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, this.vampVar);
            IncPercentSpellVampMod(owner, this.spellVampVar);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    Particle num; // UNUSED
                    SpellEffectCreate(out num, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, default, false, false);
                }
            }
        }
    }
}