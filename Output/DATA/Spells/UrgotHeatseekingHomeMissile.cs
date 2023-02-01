#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotHeatseekingHomeMissile : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = {10, 40, 70, 100, 130};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            TeamId teamID;
            float baseDamage;
            float attackDamage;
            float scaling;
            float bonusAD;
            float totalDamage;
            float nextBuffVars_MoveSpeedMod;
            Particle asdf; // UNUSED
            teamID = GetTeamID(attacker);
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            attackDamage = GetTotalAttackDamage(owner);
            scaling = 0.85f;
            bonusAD = scaling * attackDamage;
            totalDamage = baseDamage + bonusAD;
            hitResult = false;
            BreakSpellShields(target);
            ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, attacker);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.UrgotTerrorCapacitorActive2)) > 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_MoveSpeedMod = this.effect1[level];
                AddBuff(attacker, target, new Buffs.UrgotSlow(), 100, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
            AddBuff((ObjAIBase)owner, target, new Buffs.UrgotEntropyPassive(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            SpellEffectCreate(out asdf, out _, "UrgotHeatSeekingMissile_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            DestroyMissile(missileNetworkID);
        }
    }
}