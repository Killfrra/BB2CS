#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class PoisonTrail : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "bag_b", },
            AutoBuffActivateEffect = new[]{ "AcidTrail_buf.troy", },
            BuffName = "Poison Trail",
            BuffTextureName = "ChemicalMan_AcidSpray.dds",
            SpellToggleSlot = 1,
        };
        Vector3 lastPosition;
        int damagePerTick;
        float manaCost;
        float lastTimeExecuted;
        public PoisonTrail(Vector3 lastPosition = default, int damagePerTick = default, float manaCost = default)
        {
            this.lastPosition = lastPosition;
            this.damagePerTick = damagePerTick;
            this.manaCost = manaCost;
        }
        public override void OnActivate()
        {
            TeamId teamID;
            Vector3 curPos;
            int nextBuffVars_DamagePerTick;
            Minion other3;
            //RequireVar(this.lastPosition);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.manaCost);
            teamID = GetTeamID(owner);
            curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            nextBuffVars_DamagePerTick = this.damagePerTick;
            other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)owner);
            AddBuff((ObjAIBase)owner, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.25f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
            this.lastPosition = curPos;
        }
        public override void OnUpdateActions()
        {
            Vector3 curPos;
            float distance;
            float ownerMana;
            float negManaCost;
            TeamId teamID;
            Vector3 frontPos;
            float nextBuffVars_DamagePerTick;
            Minion other3;
            curPos = GetPointByUnitFacingOffset(owner, 25, 180);
            distance = DistanceBetweenPoints(curPos, this.lastPosition);
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if(ownerMana < this.manaCost)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    negManaCost = -1 * this.manaCost;
                    IncPAR(owner, negManaCost, PrimaryAbilityResourceType.MANA);
                    if(distance <= 90)
                    {
                        teamID = GetTeamID(attacker);
                        frontPos = GetPointByUnitFacingOffset(owner, 35, 0);
                        nextBuffVars_DamagePerTick = this.damagePerTick;
                        other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", frontPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                        AddBuff(attacker, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.5f, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                        this.lastPosition = curPos;
                    }
                }
            }
            if(distance >= 90)
            {
                teamID = GetTeamID(attacker);
                nextBuffVars_DamagePerTick = this.damagePerTick;
                other3 = SpawnMinion("AcidTrail", "TestCube", "idle.lua", curPos, teamID, true, false, false, true, false, true, 0, false, true, (Champion)attacker);
                AddBuff(attacker, other3, new Buffs.PoisonTrailApplicator(nextBuffVars_DamagePerTick), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                this.lastPosition = curPos;
            }
        }
    }
}
namespace Spells
{
    public class PoisonTrail : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = {22, 34, 46, 58, 70};
        public override void SelfExecute()
        {
            Vector3 pos;
            Vector3 nextBuffVars_LastPosition;
            int nextBuffVars_DamagePerTick;
            float nextBuffVars_ManaCost;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.PoisonTrail)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.PoisonTrail), (ObjAIBase)owner, 0);
                SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 1);
            }
            else
            {
                pos = GetUnitPosition(owner);
                nextBuffVars_LastPosition = pos;
                nextBuffVars_DamagePerTick = this.effect0[level];
                nextBuffVars_ManaCost = 13;
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoisonTrail(nextBuffVars_LastPosition, nextBuffVars_DamagePerTick, nextBuffVars_ManaCost), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}