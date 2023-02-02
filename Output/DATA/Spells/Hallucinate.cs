#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Hallucinate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {300, 450, 600};
        float[] effect1 = {0.7f, 0.7f, 0.7f};
        float[] effect2 = {1.35f, 1.35f, 1.35f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            bool isStealthed;
            SpellBuffRemoveType(owner, BuffType.COMBAT_ENCHANCER);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.SHRED);
            isStealthed = GetStealthed(owner);
            DestroyMissileForTarget(owner);
            if(!isStealthed)
            {
                SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            else
            {
                Vector3 pos;
                Pet other1;
                int nextBuffVars_DamageAmount;
                float nextBuffVars_DamageDealt;
                float nextBuffVars_DamageTaken;
                pos = GetRandomPointInAreaUnit(owner, 100, 0);
                other1 = CloneUnitPet(owner, nameof(Buffs.Hallucinate), 18, pos, 0, 0, true);
                nextBuffVars_DamageAmount = this.effect0[level];
                nextBuffVars_DamageDealt = this.effect1[level];
                nextBuffVars_DamageTaken = this.effect2[level];
                AddBuff(attacker, other1, new Buffs.HallucinateFull(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken), 1, 1, 18, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, other1, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, other1, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff((ObjAIBase)owner, other1, new Buffs.Backstab(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                SetStealthed(other1, false);
            }
        }
    }
}
namespace Buffs
{
    public class Hallucinate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
            IsPetDurationBuff = true,
        };
    }
}