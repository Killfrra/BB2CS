#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class GatlingGunAttack : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_SpellLevel; // UNUSED
            float baseDamage;
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_SpellLevel = level;
            baseDamage = GetBaseAttackDamage(owner);
            baseDamage *= 0.4f;
            AddBuff(attacker, target, new Buffs.GatlingDebuff(), 10, 1, 1, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0);
            if(GetBuffCountFromCaster(target, owner, nameof(Buffs.GatlingDebuffCheck)) > 0)
            {
            }
            else
            {
                ApplyDamage((ObjAIBase)owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0);
                AddBuff(attacker, target, new Buffs.GatlingDebuffCheck(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                DestroyMissile(missileNetworkID);
            }
        }
    }
}