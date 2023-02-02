#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class PowerBall : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {100, 150, 200, 250, 300};
        float[] effect1 = {-0.2f, -0.25f, -0.3f, -0.35f, -0.4f};
        float[] effect2 = {0.03f, 0.03f, 0.03f, 0.03f, 0.03f};
        public override void SelfExecute()
        {
            TeamId teamID;
            float reductionPerc;
            float cooldownTime;
            teamID = GetTeamID(owner);
            reductionPerc = GetPercentCooldownMod(owner);
            reductionPerc++;
            cooldownTime = 10 * reductionPerc;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PowerBall)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PowerBall), (ObjAIBase)owner, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownTime);
                SetCanAttack(owner, true);
                PopAllCharacterData(owner);
                SpellEffectCreate(out _, out _, "PowerBallStop.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                float nextBuffVars_AoEDamage;
                float nextBuffVars_SlowPercent;
                float nextBuffVars_MoveSpeedMod;
                TeamId nextBuffVars_CasterID; // UNUSED
                nextBuffVars_AoEDamage = this.effect0[level];
                nextBuffVars_SlowPercent = this.effect1[level];
                nextBuffVars_MoveSpeedMod = this.effect2[level];
                nextBuffVars_CasterID = GetTeamID(owner);
                AddBuff(attacker, owner, new Buffs.PowerBall(nextBuffVars_AoEDamage, nextBuffVars_SlowPercent, nextBuffVars_MoveSpeedMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.HASTE, 0, true, false, false);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
            }
        }
    }
}
namespace Buffs
{
    public class PowerBall : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "ArmordilloSpin.troy", "Powerball_buf.troy", },
            AutoBuffActivateEffectFlags = EffCreate.UPDATE_ORIENTATION,
            BuffName = "PowerBall",
            BuffTextureName = "Armordillo_Powerball.dds",
            SpellToggleSlot = 1,
        };
        float aoEDamage;
        float slowPercent;
        float moveSpeedMod;
        int casterID; // UNUSED
        bool willRemove;
        float lastTimeExecuted;
        public PowerBall(float aoEDamage = default, float slowPercent = default, float moveSpeedMod = default)
        {
            this.aoEDamage = aoEDamage;
            this.slowPercent = slowPercent;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.DefensiveBallCurl)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.DefensiveBallCurl), (ObjAIBase)owner, 0);
            }
            //RequireVar(this.aoEDamage);
            //RequireVar(this.slowPercent);
            //RequireVar(this.moveSpeedMod);
            SetCanAttack(owner, false);
            this.casterID = PushCharacterData("RammusPB", owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            this.willRemove = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            float reductionPerc;
            float cooldownTime;
            teamID = GetTeamID(owner);
            reductionPerc = GetPercentCooldownMod(owner);
            reductionPerc++;
            cooldownTime = 10 * reductionPerc;
            SetCanAttack(owner, true);
            PopAllCharacterData(owner);
            SpellEffectCreate(out _, out _, "PowerBallStop.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownTime);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, false))
            {
                this.moveSpeedMod += 0.05f;
            }
            IncPercentMultiplicativeMovementSpeedMod(owner, this.moveSpeedMod);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                bool collide;
                bool isStealthed;
                bool canSee;
                collide = false;
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 160, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                {
                    isStealthed = GetStealthed(unit);
                    if(!isStealthed)
                    {
                        collide = true;
                    }
                    else
                    {
                        if(unit is Champion)
                        {
                            collide = true;
                        }
                        else
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                collide = true;
                            }
                        }
                    }
                }
                foreach(AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    isStealthed = GetStealthed(unit);
                    if(!isStealthed)
                    {
                        collide = true;
                    }
                    else
                    {
                        if(unit is Champion)
                        {
                            collide = true;
                        }
                        else
                        {
                            canSee = CanSeeTarget(owner, unit);
                            if(canSee)
                            {
                                collide = true;
                            }
                        }
                    }
                }
                if(collide)
                {
                    ApplyRoot(attacker, owner, 0.5f);
                    foreach(AttackableUnit other1 in GetUnitsInArea((ObjAIBase)owner, owner.Position, 325, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        Particle a; // UNUSED
                        float nextBuffVars_SlowPercent;
                        SpellEffectCreate(out a, out _, "PowerballHit.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
                        BreakSpellShields(other1);
                        nextBuffVars_SlowPercent = this.slowPercent;
                        ApplyDamage(attacker, other1, this.aoEDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                        AddBuff(attacker, other1, new Buffs.PowerBallSlow(nextBuffVars_SlowPercent), 1, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                        AddBuff(attacker, other1, new Buffs.PowerballStun(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        this.willRemove = true;
                    }
                }
            }
        }
    }
}