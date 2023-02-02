#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class JaxRelentlessAttack : BBSpellScript
    {
        int[] effect0 = {100, 160, 220};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            if(hitResult != HitResult.HIT_Dodge)
            {
                if(hitResult != HitResult.HIT_Miss)
                {
                    float baseAttackDamage;
                    baseAttackDamage = GetBaseAttackDamage(owner);
                    ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                    if(target is not BaseTurret)
                    {
                        if(target is ObjAIBase)
                        {
                            Particle a; // UNUSED
                            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            ApplyDamage((ObjAIBase)owner, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, attacker);
                            SpellBuffRemove(owner, nameof(Buffs.JaxRelentlessAttack), (ObjAIBase)owner, 0);
                            SpellEffectCreate(out a, out _, "RelentlessAssault_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class JaxRelentlessAttack : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JaxRelentlessAttack",
            BuffTextureName = "BlindMonk_BlindingStrike.dds",
            IsDeathRecapSource = true,
        };
        public override void OnActivate()
        {
            OverrideAutoAttack(2, SpellSlotType.ExtraSlots, owner, 1, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
        }
    }
}