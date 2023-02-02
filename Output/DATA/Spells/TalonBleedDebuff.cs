#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonBleedDebuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "Bleed",
            BuffTextureName = "TalonNoxianDiplomacy.dds",
            IsDeathRecapSource = true,
        };
        TeamId attackerTeamID;
        float bonusDamage;
        Particle blood1;
        Particle blood2;
        Region unitBubble;
        float lastTimeExecuted2;
        float lastTimeExecuted;
        int[] effect0 = {3, 6, 9, 12, 15};
        int[] effect1 = {3, 6, 9, 12, 15};
        public override void OnActivate()
        {
            float totalAD;
            float baseAD;
            float bonusAD;
            TeamId attackerTeamID; // UNITIALIZED
            this.attackerTeamID = GetTeamID(attacker);
            totalAD = GetTotalAttackDamage(attacker);
            baseAD = GetBaseAttackDamage(attacker);
            bonusAD = totalAD - baseAD;
            this.bonusDamage = bonusAD * 0.2f;
            SpellEffectCreate(out this.blood1, out _, "talon_Q_bleed_indicator.troy", default, attackerTeamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out this.blood2, out _, "talon_Q_bleed.troy", default, attackerTeamID ?? TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            this.unitBubble = AddUnitPerceptionBubble(this.attackerTeamID, 400, owner, 6, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level;
            float poisonBaseDamage;
            float poisonTotalDamage;
            float baseAttackDamage; // UNUSED
            float flatAPBonus;
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            poisonBaseDamage = this.effect0[level];
            poisonTotalDamage = 0;
            baseAttackDamage = GetBaseAttackDamage(attacker);
            flatAPBonus *= 0.1f;
            poisonTotalDamage = poisonBaseDamage + this.bonusDamage;
            ApplyDamage(attacker, owner, poisonTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            SpellEffectRemove(this.blood1);
            SpellEffectRemove(this.blood2);
            RemovePerceptionBubble(this.unitBubble);
        }
        public override void OnUpdateActions()
        {
            if(owner is Champion)
            {
                if(ExecutePeriodically(1, ref this.lastTimeExecuted2, true))
                {
                    Vector3 ownerPos;
                    Minion other1;
                    ownerPos = GetUnitPosition(owner);
                    other1 = SpawnMinion("BloodDrop", "TestCube", "Idle.lua", ownerPos, this.attackerTeamID ?? TeamId.TEAM_UNKNOWN, false, true, false, true, true, true, 450, false, false, (Champion)attacker);
                    SetTargetable(other1, false);
                    AddBuff(other1, other1, new Buffs.ExpirationTimer(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
                {
                    int level;
                    float poisonBaseDamage;
                    float poisonTotalDamage;
                    float baseAttackDamage; // UNUSED
                    float flatAPBonus;
                    level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    poisonBaseDamage = this.effect1[level];
                    poisonTotalDamage = 0;
                    baseAttackDamage = GetBaseAttackDamage(attacker);
                    flatAPBonus *= 0.1f;
                    poisonTotalDamage = poisonBaseDamage + this.bonusDamage;
                    ApplyDamage(attacker, owner, poisonTotalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}