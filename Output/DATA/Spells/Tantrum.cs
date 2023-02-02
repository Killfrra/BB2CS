#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Tantrum : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {75, 100, 125, 150, 175};
        public override void SelfExecute()
        {
            Particle hi; // UNUSED
            SpellEffectCreate(out hi, out _, "Tantrum_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class Tantrum : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Tantrum",
            BuffTextureName = "SadMummy_Tantrum.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            int level;
            float damageReduction;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            damageReduction = level + 0;
            damageReduction *= 2;
            IncFlatPhysicalReduction(owner, damageReduction);
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            float cooldown;
            cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(cooldown > 0)
            {
                float newCooldown;
                newCooldown = cooldown - 0.5f;
                SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            }
        }
    }
}