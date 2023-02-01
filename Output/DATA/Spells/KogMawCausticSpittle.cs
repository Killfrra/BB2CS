#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class KogMawCausticSpittle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "KogMawCausticSpittle",
            BuffTextureName = "KogMaw_CausticSpittle.dds",
            IsPetDurationBuff = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float[] effect0 = {0.1f, 0.15f, 0.2f, 0.25f, 0.3f};
        int[] effect1 = {10, 15, 20, 25, 30};
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 10);
        }
        public override void OnUpdateStats()
        {
            int level;
            float attackSpeed;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            attackSpeed = this.effect0[level];
            IncPercentAttackSpeedMod(owner, attackSpeed);
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level;
            float spittleAttackSpeed;
            if(slot == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                spittleAttackSpeed = this.effect1[level];
                SetBuffToolTipVar(1, spittleAttackSpeed);
            }
        }
    }
}
namespace Spells
{
    public class KogMawCausticSpittle : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {60, 110, 160, 210, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0.7f, 1, false, false, attacker);
            AddBuff(attacker, target, new Buffs.KogMawCausticSpittleCharged(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
        }
    }
}