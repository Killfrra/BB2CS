#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OlafRagnarok : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", "L_BUFFBONE_GLB_HAND_LOC", "R_BUFFBONE_GLB_HAND_LOC", },
            AutoBuffActivateEffect = new[]{ "olaf_ragnorok_shield_02.troy", "olaf_ragnorok_shield_01.troy", "olaf_ragnorok_buff.troy", "olaf_ragnorok_buff.troy", },
            BuffName = "OlafRagnarok",
            BuffTextureName = "OlafRagnarok.dds",
            NonDispellable = true,
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        float damageAbsorption;
        public OlafRagnarok(float damageAbsorption = default)
        {
            this.damageAbsorption = damageAbsorption;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(type == BuffType.SNARE)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.SLOW)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.FEAR)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.CHARM)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.SLEEP)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.STUN)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.TAUNT)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.SUPPRESSION)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.BLIND)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            if(type == BuffType.SILENCE)
            {
                Say(owner, "game_lua_Ragnarok");
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageAbsorption);
            IncScaleSkinCoef(0.1f, owner);
            OverrideAnimation("Attack1", "Attack3", owner);
            OverrideAnimation("Attack2", "Attack3", owner);
            OverrideAnimation("Run", "Spell2", owner);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Attack1", owner);
            ClearOverrideAnimation("Attack2", owner);
            ClearOverrideAnimation("Run", owner);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.1f, owner);
        }
        public override void OnPreDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if(damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                if(this.damageAbsorption <= damageAmount)
                {
                    damageAmount -= this.damageAbsorption;
                }
                else
                {
                    damageAmount -= damageAmount;
                }
            }
        }
    }
}
namespace Spells
{
    public class OlafRagnarok : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 120f, 120f, 120f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {20, 30, 40};
        int[] effect1 = {6, 6, 6};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageAbsorption;
            nextBuffVars_DamageAbsorption = this.effect0[level];
            AddBuff(attacker, target, new Buffs.OlafRagnarok(nextBuffVars_DamageAbsorption), 1, 1, this.effect1[level], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}