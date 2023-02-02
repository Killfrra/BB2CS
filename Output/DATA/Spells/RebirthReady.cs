#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RebirthReady : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RebirthReady",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            NonDispellable = true,
            OnPreDamagePriority = 8,
            PersistsThroughDeath = true,
        };
        bool willRemove;
        int[] effect0 = {-40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20};
        int[] effect1 = {-40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20};
        int[] effect2 = {-40, -40, -40, -40, -25, -25, -25, -10, -10, -10, -10, 5, 5, 5, 20, 20, 20, 20};
        public override void OnActivate()
        {
            int level;
            float rebirthArmorMod;
            level = GetLevel(owner);
            this.willRemove = false;
            rebirthArmorMod = this.effect0[level];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth;
            curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if(curHealth <= damageAmount)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.WillRevive)) > 0)
                {
                }
                else
                {
                    damageAmount = 0;
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Rebirth)) == 0)
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                        {
                            int level;
                            float nextBuffVars_RebirthArmorMod;
                            level = GetLevel(owner);
                            nextBuffVars_RebirthArmorMod = this.effect1[level];
                            AddBuff((ObjAIBase)owner, owner, new Buffs.Rebirth(nextBuffVars_RebirthArmorMod), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        }
                    }
                    this.willRemove = true;
                }
            }
        }
        public override void OnLevelUp()
        {
            int level;
            float rebirthArmorMod;
            level = GetLevel(owner);
            rebirthArmorMod = this.effect2[level];
            SetBuffToolTipVar(1, rebirthArmorMod);
        }
    }
}