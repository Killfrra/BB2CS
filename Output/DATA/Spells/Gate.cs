#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Gate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 100f, 85f, 70f, 55f, 40f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        Particle gateParticle;
        Particle gateParticle2;
        public override void SelfExecute()
        {
            Vector3 targetPos;
            TeamId teamID; // UNUSED
            targetPos = GetCastSpellTargetPos();
            targetPos = GetNearestPassablePosition(owner, targetPos);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Destiny_marker)) > 0)
            {
                TeamId teamOfOwner;
                Particle nextBuffVars_GateParticle;
                Particle nextBuffVars_GateParticle2;
                Vector3 nextBuffVars_TargetPos;
                teamOfOwner = GetTeamID(owner);
                SpellEffectCreate(out this.gateParticle, out this.gateParticle2, "GateMarker_green.troy", "GateMarker_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
                nextBuffVars_GateParticle = this.gateParticle;
                nextBuffVars_GateParticle2 = this.gateParticle2;
                nextBuffVars_TargetPos = targetPos;
                AddBuff((ObjAIBase)owner, owner, new Buffs.Gate(nextBuffVars_GateParticle, nextBuffVars_GateParticle2, nextBuffVars_TargetPos), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SpellBuffRemove(owner, nameof(Buffs.Destiny_marker), (ObjAIBase)owner, 0);
            }
            else
            {
                SpellCast((ObjAIBase)owner, owner, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            }
            teamID = GetTeamID(owner);
        }
    }
}
namespace Buffs
{
    public class Gate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
            BuffName = "Gate",
            BuffTextureName = "Cardmaster_Premonition.dds",
        };
        int isDisabled;
        Particle gateParticle;
        Particle gateParticle2;
        Vector3 targetPos;
        Particle particle3;
        Particle particle4;
        public Gate(Particle gateParticle = default, Particle gateParticle2 = default, Vector3 targetPos = default)
        {
            this.gateParticle = gateParticle;
            this.gateParticle2 = gateParticle2;
            this.targetPos = targetPos;
        }
        public override bool OnAllowAdd(BuffType type, string scriptName, int maxStack, float duration)
        {
            bool returnValue = true;
            if(owner.Team != attacker.Team)
            {
                if(type == BuffType.FEAR)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.CHARM)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.SLEEP)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.STUN)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.TAUNT)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.SILENCE)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
                if(type == BuffType.SUPPRESSION)
                {
                    this.isDisabled = 1;
                    SpellBuffRemoveCurrent(owner);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            //RequireVar(this.gateParticle);
            //RequireVar(this.gateParticle2);
            //RequireVar(this.targetPos);
            this.isDisabled = 0;
            teamOfOwner = GetTeamID(owner);
            SpellEffectCreate(out this.particle3, out this.particle4, "CardmasterTeleport_green.troy", "CardmasterTeleport_red.troy", teamOfOwner ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamOfOwner; // UNUSED
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
            teamOfOwner = GetTeamID(owner);
            SpellEffectRemove(this.gateParticle2);
            SpellEffectRemove(this.gateParticle);
            SpellEffectRemove(this.particle3);
            SpellEffectRemove(this.particle4);
            DestroyMissileForTarget(owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            if(this.isDisabled == 0)
            {
                if(expired)
                {
                    Vector3 targetPos;
                    targetPos = this.targetPos;
                    TeleportToPosition(owner, targetPos);
                }
            }
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
        }
    }
}