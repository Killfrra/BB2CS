#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class UrgotPlasmaGrenadeBoom : BBSpellScript
    {
        int[] effect0 = {5, 5, 5, 5, 5};
        int[] effect1 = {75, 130, 185, 240, 295};
        float[] effect2 = {-0.12f, -0.14f, -0.16f, -0.18f, -0.2f};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 targetPos;
            TeamId teamID;
            float buffDuration;
            Particle particle; // UNUSED
            float aD;
            float dmg;
            float bonusDamage;
            float totalDamage;
            float remainder;
            float ticks;
            float tickDamage;
            float nextBuffVars_ArmorReduced;
            targetPos = GetUnitPosition(target);
            level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            teamID = GetTeamID(owner);
            buffDuration = this.effect0[level];
            SpellEffectCreate(out particle, out _, "UrgotPlasmaGrenade_tar.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, default, default, targetPos, true, false, false, false, false);
            aD = GetFlatPhysicalDamageMod(owner);
            dmg = this.effect1[level];
            bonusDamage = aD * 0.6f;
            totalDamage = bonusDamage + dmg;
            remainder = buffDuration % 0.5f;
            ticks = buffDuration - remainder;
            tickDamage = totalDamage / ticks;
            nextBuffVars_ArmorReduced = this.effect2[level];
            foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, targetPos, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                bool isStealthed;
                float nextBuffVars_TickDamage;
                isStealthed = GetStealthed(unit);
                if(!isStealthed)
                {
                    BreakSpellShields(unit);
                    nextBuffVars_TickDamage = tickDamage;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                    AddBuff((ObjAIBase)owner, unit, new Buffs.UrgotPlasmaGrenadeBoom(nextBuffVars_ArmorReduced), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, true, false);
                }
                else
                {
                    if(unit is Champion)
                    {
                        BreakSpellShields(unit);
                        nextBuffVars_TickDamage = tickDamage;
                        AddBuff((ObjAIBase)owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                    }
                    else
                    {
                        bool canSee;
                        canSee = CanSeeTarget(owner, unit);
                        if(canSee)
                        {
                            BreakSpellShields(unit);
                            nextBuffVars_TickDamage = tickDamage;
                            AddBuff((ObjAIBase)owner, unit, new Buffs.UrgotCorrosiveDebuff(nextBuffVars_TickDamage), 1, 1, buffDuration, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, true, false);
                        }
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class UrgotPlasmaGrenadeBoom : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrgotCorrosiveDebuff",
            BuffTextureName = "UrgotCorrosiveCharge.dds",
        };
        Particle hitParticle;
        float armorReduced;
        public UrgotPlasmaGrenadeBoom(float armorReduced = default)
        {
            this.armorReduced = armorReduced;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out this.hitParticle, out _, "UrgotPlasmaGrenade_hit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.armorReduced);
            IncPercentArmorMod(owner, this.armorReduced);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.hitParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, this.armorReduced);
        }
    }
}