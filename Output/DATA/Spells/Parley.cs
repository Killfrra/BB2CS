#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Parley : BBBuffScript
    {
        float goldGain;
        public Parley(float goldGain = default)
        {
            this.goldGain = goldGain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.goldGain);
        }
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnDeath()
        {
            ObjAIBase caster;
            caster = SetBuffCasterUnit();
            if(!attacker.IsDead)
            {
                if(attacker == caster)
                {
                    IncGold(attacker, this.goldGain);
                }
            }
        }
    }
}
namespace Spells
{
    public class Parley : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {4, 5, 6, 7, 8};
        int[] effect1 = {20, 45, 70, 95, 120};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float critChance;
            float nextBuffVars_GoldGain;
            float parBaseDamage;
            float baseDamage;
            float damageVar;
            critChance = GetFlatCritChanceMod(attacker);
            if(RandomChance() < critChance)
            {
                hitResult = HitResult.HIT_Critical;
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
            }
            BreakSpellShields(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_GoldGain = this.effect0[level];
            AddBuff(attacker, target, new Buffs.Parley(nextBuffVars_GoldGain), 1, 1, 0.01f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            parBaseDamage = this.effect1[level];
            baseDamage = GetBaseAttackDamage(owner);
            baseDamage *= 1;
            damageVar = parBaseDamage + baseDamage;
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}