#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class OrianaDamageIzuna : BBBuffScript
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
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            this.landed = false;
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 ownerCenter; // UNUSED
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            ownerCenter = GetUnitPosition(owner);
        }
        public override void OnUpdateActions()
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
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
    public class OrianaDamageIzuna : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        object targetPos; // UNITIALIZED
        bool landed; // UNUSED
        float[] effect0 = {-0.24f, -0.28f, -0.32f, -0.36f, -0.4f};
        int[] effect1 = {0, 0, 0, 0, 0};
        int[] effect2 = {60, 100, 140, 180, 220};
        int[] effect3 = {50, 80, 110, 140, 170};
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            bool correctSpell;
            float duration;
            TeamId teamID;
            object targetPos; // UNUSED
            Minion other3;
            float nextBuffVars_MovementSpeedMod;
            int nextBuffVars_AttackSpeedMod;
            float nextBuffVars_TotalDamage;
            float baseDamage;
            float aP;
            float bonusDamage;
            float totalDamage;
            correctSpell = false;
            duration = GetBuffRemainingDuration(owner, nameof(Buffs.OrianaIzuna));
            if(spellName == nameof(Spells.Yomuizuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaIzuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.OrianaFastIzuna))
            {
                correctSpell = true;
            }
            else if(spellName == nameof(Spells.Yomufastizuna))
            {
                correctSpell = true;
            }
            if(duration >= 0.001f)
            {
                if(correctSpell)
                {
                    teamID = GetTeamID(owner);
                    targetPos = this.targetPos;
                    other3 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, default, true, (Champion)owner);
                    AddBuff((ObjAIBase)owner, other3, new Buffs.OrianaGhost(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, other3, new Buffs.OrianaGhostMinion(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    nextBuffVars_MovementSpeedMod = this.effect0[level];
                    nextBuffVars_AttackSpeedMod = this.effect1[level];
                    foreach(AttackableUnit unit in GetUnitsInArea(attacker, other3.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.OrianaIzunaDamage), false))
                    {
                        BreakSpellShields(unit);
                        baseDamage = this.effect2[level];
                        aP = GetFlatMagicDamageMod(owner);
                        bonusDamage = aP * 0.5f;
                        totalDamage = bonusDamage + baseDamage;
                        nextBuffVars_TotalDamage = totalDamage;
                        AddBuff(attacker, unit, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    charVars.GhostAlive = false;
                    DestroyMissile(charVars.MissileID);
                    SpellBuffClear(owner, nameof(Buffs.OrianaIzuna));
                    this.landed = true;
                }
                else
                {
                    Say(owner, "SpellName: ", correctSpell);
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseDamage;
            float aP;
            float bonusDamage;
            float totalDamage;
            float nextBuffVars_TotalDamage;
            if(GetBuffCountFromCaster(target, attacker, nameof(Buffs.OrianaIzunaDamage)) == 0)
            {
                BreakSpellShields(target);
                baseDamage = this.effect3[level];
                aP = GetFlatMagicDamageMod(owner);
                bonusDamage = aP * 0.5f;
                totalDamage = bonusDamage + baseDamage;
                totalDamage *= 1.25f;
                nextBuffVars_TotalDamage = totalDamage;
                AddBuff(attacker, target, new Buffs.OrianaIzunaDamage(nextBuffVars_TotalDamage), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}