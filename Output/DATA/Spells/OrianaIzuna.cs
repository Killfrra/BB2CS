#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaIzuna : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
            SpellVOOverrideSkins = new[]{ "BroOlaf", },
        };
        bool landed; // UNUSED
        int missilePosition; // UNUSED
        public override void OnActivate()
        {
            //RequireVar(this.castPos);
            //RequireVar(this.targetPos);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            this.landed = false;
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 ownerCenter; // UNUSED
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            ownerCenter = GetUnitPosition(owner);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            SpellMissile missileId; // UNITIALIZED
            Vector3 missilePosition;
            charVars.MissileID = missileId;
            charVars.GhostAlive = true;
            missilePosition = GetMissilePosFromID(missileId ?? 0);
            this.missilePosition = missilePosition;
        }
    }
}
namespace Spells
{
    public class OrianaIzuna : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        bool landed; // UNUSED
        object targetPos; // UNITIALIZED
        int[] effect0 = {60, 100, 140, 180, 220};
        int[] effect1 = {60, 100, 140, 180, 220};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell;
            float duration;
            TeamId teamID;
            object targetPos; // UNUSED
            Minion other3;
            Particle temp; // UNUSED
            float baseDamage;
            float aP;
            float bonusDamage;
            float totalDamage;
            float nextBuffVars_TotalDamage;
            correctSpell = false;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaIzuna));
            if(spellName == nameof(Spells.OrianaIzuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaIzuna))
            {
                correctSpell = true;
            }
            if(true)
            {
                charVars.GhostAlive = false;
                SpellBuffClear(owner, nameof(Buffs.OrianaIzuna));
                this.landed = true;
            }
            if(duration >= 0.001f)
            {
                if(correctSpell)
                {
                    teamID = GetTeamID(owner);
                    targetPos = this.targetPos;
                    other3 = SpawnMinion("TheDoomBall", "OriannaBall", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true, (Champion)owner);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    SpellEffectCreate(out temp, out _, "Oriana_Izuna_nova.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, missileEndPosition, default, default, missileEndPosition, true, default, default, false, false);
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, other3.Position, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
                    {
                        baseDamage = this.effect0[level];
                        aP = GetFlatMagicDamageMod(owner);
                        bonusDamage = aP * 0.6f;
                        totalDamage = bonusDamage + baseDamage;
                        totalDamage *= charVars.IzunaPercent;
                        charVars.IzunaPercent *= 0.9f;
                        charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                        nextBuffVars_TotalDamage = totalDamage;
                        if(GetBuffCountFromCaster(unit, default, nameof(Buffs.OrianaIzunaDamage)) == 0)
                        {
                            BreakSpellShields(unit);
                            AddBuff(attacker, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
            DestroyMissile(charVars.MissileID);
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float aP;
            float bonusDamage;
            float totalDamage;
            float nextBuffVars_TotalDamage;
            if(GetBuffCountFromCaster(target, default, nameof(Buffs.OrianaIzunaDamaged)) == 0)
            {
                baseDamage = this.effect1[level];
                aP = GetFlatMagicDamageMod(owner);
                bonusDamage = aP * 0.6f;
                totalDamage = bonusDamage + baseDamage;
                totalDamage *= charVars.IzunaPercent;
                charVars.IzunaPercent *= 0.9f;
                charVars.IzunaPercent = Math.Max(0.4f, charVars.IzunaPercent);
                BreakSpellShields(target);
                nextBuffVars_TotalDamage = totalDamage;
                AddBuff(attacker, target, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}