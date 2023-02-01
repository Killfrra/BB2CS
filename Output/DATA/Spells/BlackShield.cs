#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class BlackShield : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Spellimmunity.troy", },
            BuffName = "Black Shield",
            BuffTextureName = "FallenAngel_BlackShield.dds",
            OnPreDamagePriority = 2,
            DoOnPreDamageInExpirationOrder = true,
        };
        float shieldHealth;
        float oldArmorAmount;
        public BlackShield(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.FEAR)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.CHARM)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SILENCE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SLEEP)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SLOW)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SNARE)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.STUN)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.TAUNT)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.BLIND)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else if(type == BuffType.SUPPRESSION)
                {
                    Say(owner, "game_lua_BlackShield_immune");
                    returnValue = false;
                }
                else
                {
                    returnValue = true;
                }
            }
            else
            {
                returnValue = true;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            ApplyAssistMarker(attacker, owner, 10);
            IncreaseShield(owner, this.shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            if(this.shieldHealth > 0)
            {
                RemoveShield(owner, this.shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if(this.shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            this.oldArmorAmount = this.shieldHealth;
            if(damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if(this.shieldHealth >= damageAmount)
                {
                    this.shieldHealth -= damageAmount;
                    damageAmount = 0;
                    this.oldArmorAmount -= this.shieldHealth;
                    ReduceShield(owner, this.oldArmorAmount, true, false);
                }
                else
                {
                    damageAmount -= this.shieldHealth;
                    this.shieldHealth = 0;
                    ReduceShield(owner, this.oldArmorAmount, true, false);
                }
            }
        }
    }
}
namespace Spells
{
    public class BlackShield : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 16f, 16f, 16f, 16f, 16f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {95, 160, 225, 290, 355};
        int[] effect1 = {5, 5, 5, 5, 5};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float abilityPower;
            float baseHealth;
            float abilityPowerMod;
            float shieldHealth;
            float nextBuffVars_ShieldHealth;
            abilityPower = GetFlatMagicDamageMod(owner);
            baseHealth = this.effect0[level];
            abilityPowerMod = abilityPower * 0.7f;
            shieldHealth = abilityPowerMod + baseHealth;
            nextBuffVars_ShieldHealth = shieldHealth;
            AddBuff(attacker, target, new Buffs.BlackShield(nextBuffVars_ShieldHealth), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.SPELL_IMMUNITY, 0, true, false, false);
        }
    }
}