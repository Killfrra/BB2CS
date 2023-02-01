#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserChildrenOfTheGrave : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "MordekaiserCOTGDot",
            BuffTextureName = "Mordekaiser_COTG.dds",
            NonDispellable = false,
            OnPreDamagePriority = 10,
        };
        float lifestealPercent;
        float damageToDeal;
        Particle mordekaiserParticle;
        bool removeParticle;
        float lastTimeExecuted;
        public MordekaiserChildrenOfTheGrave(float lifestealPercent = default)
        {
            this.lifestealPercent = lifestealPercent;
        }
        public override void OnActivate()
        {
            float maxHealth;
            //RequireVar(this.lifestealPercent);
            maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            this.damageToDeal = maxHealth * this.lifestealPercent;
            SpellEffectCreate(out this.mordekaiserParticle, out _, "mordekeiser_cotg_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            this.removeParticle = true;
        }
        public override void OnDeactivate(bool expired)
        {
            if(this.removeParticle)
            {
                SpellEffectRemove(this.mordekaiserParticle);
            }
        }
        public override void OnUpdateActions()
        {
            float nextBuffVars_DamageToDeal;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                nextBuffVars_DamageToDeal = this.damageToDeal;
                AddBuff((ObjAIBase)owner, attacker, new Buffs.MordekaiserCOTGDot(nextBuffVars_DamageToDeal), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            ObjAIBase caster;
            Particle nextBuffVars_MordekaiserParticle;
            if(owner is Champion)
            {
                curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                if(curHealth <= 0)
                {
                    caster = SetBuffCasterUnit();
                    this.removeParticle = false;
                    nextBuffVars_MordekaiserParticle = this.mordekaiserParticle;
                    AddBuff((ObjAIBase)owner, caster, new Buffs.MordekaiserCOTGRevive(nextBuffVars_MordekaiserParticle), 1, 1, 30, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class MordekaiserChildrenOfTheGrave : BBSpellScript
    {
        float[] effect0 = {0.24f, 0.29f, 0.34f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float mordAP;
            float damageToDeal;
            float maxHealth;
            float mordAP1;
            float initialDamageToDeal;
            float tickDamage;
            float nextBuffVars_LifestealPercent;
            float nextBuffVars_DamageToDeal;
            mordAP = GetFlatMagicDamageMod(owner);
            damageToDeal = this.effect0[level];
            maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            mordAP1 = mordAP * 0.0004f;
            damageToDeal += mordAP1;
            damageToDeal *= 0.5f;
            initialDamageToDeal = maxHealth * damageToDeal;
            tickDamage = damageToDeal * 0.1f;
            nextBuffVars_LifestealPercent = tickDamage;
            AddBuff((ObjAIBase)owner, target, new Buffs.MordekaiserChildrenOfTheGrave(nextBuffVars_LifestealPercent), 1, 1, 10.4f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            nextBuffVars_DamageToDeal = initialDamageToDeal;
            AddBuff((ObjAIBase)target, attacker, new Buffs.MordekaiserCOTGDot(nextBuffVars_DamageToDeal), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}