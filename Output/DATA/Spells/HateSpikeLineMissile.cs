#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HateSpikeLineMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {30, 50, 70, 90, 110};
        int[] effect1 = {30, 50, 70, 90, 110};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            BreakSpellShields(target);
            if(target is Champion)
            {
                ApplyDamage((ObjAIBase)owner, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.33f, 1, false, false, attacker);
                AddBuff((ObjAIBase)owner, target, new Buffs.EvelynnSoulEater(), 4, 1, 4, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
            }
            else
            {
                ApplyDamage((ObjAIBase)owner, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.33f, 1, false, false, attacker);
            }
        }
    }
}