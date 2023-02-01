#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HateSpikeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = 0,
                CanHitSameTarget = 0,
                CanHitSameTargetConsecutively = 0,
                MaximumHits = new[]{ 2, 2, 2, 2, 2, },
            },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        float[] effect0 = {12.5f, 20, 27.5f, 35, 42.5f};
        int[] effect1 = {25, 40, 55, 70, 85};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int hSCounter;
            hSCounter = GetCastSpellTargetsHitPlusOne();
            if(target is Champion)
            {
                AddBuff(attacker, target, new Buffs.Malice_marker(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            if(hSCounter == 2)
            {
                ApplyDamage((ObjAIBase)owner, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.14f);
            }
            else
            {
                ApplyDamage((ObjAIBase)owner, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.28f);
            }
        }
    }
}