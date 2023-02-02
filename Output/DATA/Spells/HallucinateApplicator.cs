#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HallucinateApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hallucinate",
            BuffTextureName = "Jester_HallucinogenBomb.dds",
            IsPetDurationBuff = true,
        };
        float damageAmount;
        float damageDealt;
        float damageTaken;
        float shacoDamageTaken;
        public HallucinateApplicator(float damageAmount = default, float damageDealt = default, float damageTaken = default, float shacoDamageTaken = default)
        {
            this.damageAmount = damageAmount;
            this.damageDealt = damageDealt;
            this.damageTaken = damageTaken;
            this.shacoDamageTaken = shacoDamageTaken;
        }
        public override void OnActivate()
        {
            Vector3 ownerPos;
            TeamId teamID;
            Particle fadeParticle; // UNUSED
            //RequireVar(this.damageAmount);
            //RequireVar(this.damageDealt);
            //RequireVar(this.damageTaken);
            //RequireVar(this.shacoDamageTaken);
            SetStunned(owner, true);
            SetTargetable(owner, false);
            SetNoRender(owner, true);
            ownerPos = GetUnitPosition(owner);
            teamID = GetTeamID(owner);
            SpellEffectCreate(out fadeParticle, out _, "HallucinatePoof.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            Vector3 pos1;
            int level; // UNUSED
            Pet other1;
            TeamId teamID;
            float nextBuffVars_DamageAmount;
            Particle fadeParticle; // UNUSED
            Vector3 pos2;
            float nextBuffVars_DamageDealt;
            float nextBuffVars_DamageTaken;
            float nextBuffVars_shacoDamageTaken; // UNUSED
            SetStunned(owner, false);
            SetNoRender(owner, false);
            SetTargetable(owner, true);
            pos1 = GetRandomPointInAreaUnit(owner, 250, 50);
            level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            other1 = CloneUnitPet(owner, nameof(Buffs.Hallucinate), 18, pos1, 0, 0, true);
            teamID = GetTeamID(owner);
            nextBuffVars_DamageAmount = this.damageAmount;
            nextBuffVars_DamageDealt = this.damageDealt;
            nextBuffVars_DamageTaken = this.damageTaken;
            nextBuffVars_shacoDamageTaken = this.shacoDamageTaken;
            AddBuff((ObjAIBase)owner, other1, new Buffs.HallucinateFull(nextBuffVars_DamageAmount, nextBuffVars_DamageDealt, nextBuffVars_DamageTaken), 1, 1, 18, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, other1, new Buffs.Backstab(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            SpellEffectCreate(out fadeParticle, out _, "HallucinatePoof.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, other1, default, default, target, default, default, true, false, false, false, false);
            SetStealthed(other1, false);
            pos2 = GetRandomPointInAreaUnit(owner, 250, 50);
            TeleportToPosition(owner, pos2);
            SpellEffectCreate(out fadeParticle, out _, "HallucinatePoof.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
    }
}