#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShenKiAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDmg;
            float maxHP;
            float bonusDmgFromHP;
            float shurikenDamage;
            float damageToDeal;
            if(target is ObjAIBase)
            {
                Particle hi; // UNUSED
                SpellEffectCreate(out hi, out _, "Globalhit_red.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position, owner, default, default, true, default, default, false);
            }
            baseDmg = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            maxHP = GetFlatHPPoolMod(owner);
            bonusDmgFromHP = maxHP * 0.08f;
            level = GetLevel(owner);
            shurikenDamage = this.effect0[level];
            damageToDeal = bonusDmgFromHP + shurikenDamage;
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            SpellBuffRemove(attacker, nameof(Buffs.ShenWayOfTheNinjaAura), attacker, 0);
        }
    }
}