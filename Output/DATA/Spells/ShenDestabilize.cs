#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShenDestabilize : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {-15, -22, -29, -36, -43};
        float[] effect1 = {0.1f, 0.15f, 0.2f, 0.25f, 0.3f};
        float[] effect2 = {0.1f, 0.1f, 0.1f, 0.1f, 0.1f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_ArmorMod;
            float nextBuffVars_LifeReturn;
            float nextBuffVars_NinjaBonus;
            nextBuffVars_ArmorMod = this.effect0[level];
            nextBuffVars_LifeReturn = this.effect1[level];
            nextBuffVars_NinjaBonus = this.effect2[level];
            AddBuff(attacker, target, new Buffs.ShenDestabilize(nextBuffVars_NinjaBonus, nextBuffVars_LifeReturn, nextBuffVars_ArmorMod), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0);
        }
    }
}
namespace Buffs
{
    public class ShenDestabilize : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "archersmark_tar.troy", },
            BuffName = "Shen Destabilize",
            BuffTextureName = "GSB_Stun.dds",
        };
        float ninjaBonus;
        float lifeReturn;
        float armorMod;
        public ShenDestabilize(float ninjaBonus = default, float lifeReturn = default, float armorMod = default)
        {
            this.ninjaBonus = ninjaBonus;
            this.lifeReturn = lifeReturn;
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.ninjaBonus);
            //RequireVar(this.lifeReturn);
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.armorMod);
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            SetTriggerUnit(target);
            if(target is Champion)
            {
                float healAmount;
                float healTotal;
                ObjAIBase caster;
                if(GetBuffCountFromCaster(target, target, nameof(Buffs.IsNinja)) > 0)
                {
                    healAmount = this.lifeReturn + this.ninjaBonus;
                }
                else
                {
                    healAmount = this.lifeReturn;
                }
                healTotal = healAmount * damageAmount;
                caster = SetBuffCasterUnit();
                IncHealth(target, healTotal, caster);
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
            }
        }
    }
}