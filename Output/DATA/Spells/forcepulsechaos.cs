#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Forcepulsechaos : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {60, 120, 180, 240, 300};
        float[] effect1 = {-0.3f, -0.35f, -0.4f, -0.45f, -0.5f};
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.ForcePulseCanCast), (ObjAIBase)owner);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false);
            nextBuffVars_MoveSpeedMod = this.effect1[level];
            nextBuffVars_AttackSpeedMod = 0;
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 100, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true);
        }
    }
}
namespace Buffs
{
    public class Forcepulsechaos : BBBuffScript
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
                    AddBuff(attacker, attacker, new Buffs.ForcePulseCounter(), 6, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true);
                }
            }
        }
    }
}