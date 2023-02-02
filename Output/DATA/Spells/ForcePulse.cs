#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ForcePulse : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {80, 130, 180, 230, 280};
        float[] effect1 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.ForcePulseCanCast), (ObjAIBase)owner, 0);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 1, false, false, attacker);
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            nextBuffVars_AttackSpeedMod = 0;
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ForcePulse : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse.dds",
        };
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(!spellVars.DoesntTriggerSpellCasts)
            {
                attacker = SetBuffCasterUnit();
                if(GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.ForcePulseCanCast)) > 0)
                {
                }
                else
                {
                    AddBuff(attacker, attacker, new Buffs.ForcePulseCounter(), 6, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
    }
}