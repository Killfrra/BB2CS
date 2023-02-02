#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MaokaiDrain3 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {0.8f, 0.8f, 0.8f};
        float[] effect1 = {0.8f, 0.75f, 0.7f};
        int[] effect2 = {15, 15, 15};
        int[] effect3 = {100, 150, 200};
        int[] effect4 = {200, 250, 300};
        public override void SelfExecute()
        {
            Vector3 targetPos;
            Vector3 nextBuffVars_TargetPos;
            float nextBuffVars_DefenseBonus;
            float nextBuffVars_CCReduction;
            float nextBuffVars_ManaCost;
            float nextBuffVars_BaseDamage;
            float nextBuffVars_BonusCap;
            targetPos = GetCastSpellTargetPos();
            nextBuffVars_TargetPos = targetPos;
            nextBuffVars_DefenseBonus = this.effect0[level];
            nextBuffVars_CCReduction = this.effect1[level];
            nextBuffVars_ManaCost = this.effect2[level];
            nextBuffVars_BaseDamage = this.effect3[level];
            nextBuffVars_BonusCap = this.effect4[level];
            AddBuff((ObjAIBase)owner, owner, new Buffs.MaokaiDrain3(nextBuffVars_TargetPos, nextBuffVars_DefenseBonus, nextBuffVars_CCReduction, nextBuffVars_ManaCost, nextBuffVars_BaseDamage, nextBuffVars_BonusCap), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class MaokaiDrain3 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiDrain",
            BuffTextureName = "Maokai_VengefulMaelstrom.dds",
            SpellToggleSlot = 4,
        };
        Vector3 targetPos;
        float defenseBonus;
        float cCReduction;
        float manaCost;
        float baseDamage;
        float bonusCap;
        Particle particle;
        Particle particle2;
        Particle particle3;
        float damageManaTimer;
        float slowTimer;
        int[] effect0 = {40, 30, 20};
        public MaokaiDrain3(Vector3 targetPos = default, float defenseBonus = default, float cCReduction = default, float manaCost = default, float baseDamage = default, float bonusCap = default)
        {
            this.targetPos = targetPos;
            this.defenseBonus = defenseBonus;
            this.cCReduction = cCReduction;
            this.manaCost = manaCost;
            this.baseDamage = baseDamage;
            this.bonusCap = bonusCap;
        }
        public override void OnActivate()
        {
            Vector3 targetPos;
            TeamId teamOfOwner;
            int ownerSkinID;
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MaokaiDrain3Toggle));
            SetSlotSpellCooldownTimeVer2(1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            //RequireVar(this.targetPos);
            //RequireVar(this.defenseBonus);
            //RequireVar(this.cCReduction);
            //RequireVar(this.manaCost);
            //RequireVar(this.baseDamage);
            //RequireVar(this.bonusCap);
            targetPos = this.targetPos;
            teamOfOwner = GetTeamID(owner);
            ownerSkinID = GetSkinID(owner);
            SpellEffectCreate(out this.particle, out _, "maoki_torrent_cas_01.troy", default, teamOfOwner ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, true, false, false, false, false);
            if(ownerSkinID == 3)
            {
                SpellEffectCreate(out this.particle2, out this.particle3, "maoki_torrent_01_teamID_Christmas_green.troy", "maoki_torrent_01_teamID_Christmas_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 300, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle2, out this.particle3, "maoki_torrent_01_teamID_green.troy", "maoki_torrent_01_teamID_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 300, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, false, false, false, false);
            }
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_DefenseBonus;
                float nextBuffVars_CCReduction; // UNUSED
                Vector3 nextBuffVars_TargetPos;
                nextBuffVars_DefenseBonus = this.defenseBonus;
                nextBuffVars_CCReduction = this.cCReduction;
                nextBuffVars_TargetPos = this.targetPos;
                AddBuff(attacker, unit, new Buffs.MaokaiDrain3Defense(nextBuffVars_DefenseBonus, nextBuffVars_TargetPos), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                ApplyAssistMarker((ObjAIBase)owner, unit, 10);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            int level;
            float cooldown;
            float cooldownStat;
            float multiplier;
            float newCooldown;
            teamID = GetTeamID(owner);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.MaokaiDrain3));
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown = this.effect0[level];
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = cooldown * multiplier;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            SpellEffectRemove(this.particle3);
            if(charVars.Tally >= 0)
            {
                float bonus;
                float totalDamage;
                Particle explosionVFX; // UNUSED
                bonus = charVars.Tally * 2;
                bonus = Math.Min(this.bonusCap, bonus);
                totalDamage = this.baseDamage + bonus;
                SpellEffectCreate(out explosionVFX, out _, "maoki_torrent_deflect_self_cas.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.targetPos, owner, default, default, true, false, false, false, false);
                SpellEffectCreate(out explosionVFX, out _, "maoki_torrent_deflect_cas_02.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.targetPos, owner, default, default, true, false, false, false, false);
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, this.targetPos, 550, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    Particle targetVFX; // UNUSED
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.5f, 1, false, false, attacker);
                    SpellEffectCreate(out targetVFX, out _, "maoki_torrent_unit_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                charVars.Tally = 0;
            }
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.damageManaTimer, false))
            {
                float curMana;
                curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if(this.manaCost > curMana)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    float negMana;
                    negMana = this.manaCost * -1;
                    IncPAR(owner, negMana, PrimaryAbilityResourceType.MANA);
                }
            }
            if(ExecutePeriodically(0.25f, ref this.slowTimer, false))
            {
                Vector3 targetPos;
                Vector3 ownerPos;
                float distance;
                targetPos = this.targetPos;
                ownerPos = GetUnitPosition(owner);
                distance = DistanceBetweenPoints(ownerPos, targetPos);
                if(distance >= 1400)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                foreach(AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 550, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    Vector3 nextBuffVars_TargetPos;
                    float nextBuffVars_DefenseBonus;
                    float nextBuffVars_CCReduction; // UNUSED
                    nextBuffVars_TargetPos = this.targetPos;
                    nextBuffVars_DefenseBonus = this.defenseBonus;
                    nextBuffVars_CCReduction = this.cCReduction;
                    AddBuff(attacker, unit, new Buffs.MaokaiDrain3Defense(nextBuffVars_DefenseBonus, nextBuffVars_TargetPos), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    ApplyAssistMarker((ObjAIBase)owner, unit, 10);
                }
            }
        }
    }
}