#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ViktorPowerTransfer : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = {80, 125, 170, 215, 260};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            float baseDamage;
            float aPVAL;
            float aPBONUS;
            Vector3 targetPos;
            teamID = GetTeamID(attacker);
            AddBuff(attacker, owner, new Buffs.ViktorPowerTransfer(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            aPVAL = GetFlatMagicDamageMod(owner);
            aPBONUS = aPVAL * 0.65f;
            charVars.TotalDamage = aPBONUS + baseDamage;
            if(target is Champion)
            {
                charVars.IsChampTarget = true;
                ApplyDamage(attacker, target, charVars.TotalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            }
            else
            {
                charVars.IsChampTarget = true;
                ApplyDamage(attacker, target, charVars.TotalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            }
            targetPos = GetUnitPosition(target);
            SpellCast((ObjAIBase)owner, owner, default, default, 2, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, true, targetPos);
        }
    }
}
namespace Buffs
{
    public class ViktorPowerTransfer : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}