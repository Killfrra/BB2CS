#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class RumbleOverheatAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[]{ "GangsterTwitch", "PunkTwitch", },
        };
        int punchdmg; // UNUSED
        public override void OnActivate()
        {
            this.punchdmg = 0;
        }
    }
}
namespace Spells
{
    public class RumbleOverheatAttack : BBSpellScript
    {
        float punchdmg;
        int[] effect0 = {25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.RumbleOverheat)) > 0)
                    {
                        level = GetLevel(owner);
                        this.punchdmg = this.effect0[level];
                        level = GetLevel(owner);
                        ApplyDamage(attacker, target, this.punchdmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0.3f, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}