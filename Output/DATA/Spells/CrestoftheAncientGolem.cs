#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CrestoftheAncientGolem : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "CrestoftheAncientGolem",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        int cooldownVar; // UNUSED
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "NeutralMonster_buf_blue_defense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            this.cooldownVar = 0;
            SetBuffToolTipVar(1, 20);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            if(owner is Champion)
            {
                float maxMana;
                float manaRegen;
                float maxEnergy;
                float energyRegen;
                IncPercentCooldownMod(owner, -0.2f);
                maxMana = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
                manaRegen = maxMana * 0.01f;
                IncFlatPARRegenMod(owner, 5 + manaRegen, PrimaryAbilityResourceType.MANA);
                maxEnergy = GetMaxPAR(target, PrimaryAbilityResourceType.Energy);
                energyRegen = maxEnergy * 0.01f;
                IncFlatPARRegenMod(owner, 5 + energyRegen, PrimaryAbilityResourceType.Energy);
            }
        }
        public override void OnDeath()
        {
            int count;
            float newDuration;
            count = GetBuffCountFromAll(attacker, nameof(Buffs.APBonusDamageToTowers));
            if(attacker is Champion)
            {
                if(!attacker.IsDead)
                {
                    newDuration = 150;
                    if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.MonsterBuffs)) > 0)
                    {
                        newDuration *= 1.2f;
                    }
                    AddBuff(attacker, attacker, new Buffs.CrestoftheAncientGolem(), 1, 1, newDuration, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                }
            }
            else if(count != 0)
            {
                ObjAIBase caster;
                caster = GetPetOwner((Pet)attacker);
                if(caster is Champion)
                {
                    if(!caster.IsDead)
                    {
                        newDuration = 150;
                        if(GetBuffCountFromCaster(caster, caster, nameof(Buffs.MonsterBuffs)) > 0)
                        {
                            newDuration *= 1.2f;
                        }
                        AddBuff(caster, caster, new Buffs.CrestoftheAncientGolem(), 1, 1, newDuration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
            }
        }
    }
}