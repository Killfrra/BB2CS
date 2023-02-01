#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CannonBarrage : BBBuffScript
    {
        Vector3 castPosition;
        Particle particle;
        Particle particle2;
        Region bubbleID;
        float moveSpeedMod;
        float attackSpeedMod;
        public CannonBarrage(Vector3 castPosition = default, float moveSpeedMod = default, float attackSpeedMod = default)
        {
            this.castPosition = castPosition;
            this.moveSpeedMod = moveSpeedMod;
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            TeamId teamOfOwner;
            Vector3 castPosition;
            //RequireVar(this.castPosition);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            teamOfOwner = GetTeamID(attacker);
            castPosition = this.castPosition;
            SpellEffectCreate(out this.particle, out this.particle2, "pirate_cannonBarrage_aoe_indicator_green.troy", "pirate_cannonBarrage_aoe_indicator_red.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, default, castPosition, target, default, default, false, false, false, false, false);
            this.bubbleID = AddPosPerceptionBubble(teamOfOwner, 650, castPosition, 8, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            RemovePerceptionBubble(this.bubbleID);
        }
        public override void OnUpdateActions()
        {
            Vector3 centerPosition;
            int level;
            Vector3 castPosition;
            Vector3 cannonPosition;
            float nextBuffVars_MoveSpeedMod;
            float nextBuffVars_AttackSpeedMod;
            attacker = SetBuffCasterUnit();
            centerPosition = this.castPosition;
            level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(RandomChance() < 0.15f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 45);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if(RandomChance() < 0.1765f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 135);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if(RandomChance() < 0.2076f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 225);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else if(RandomChance() < 0.2443f)
            {
                castPosition = GetPointByUnitFacingOffset(owner, 290, 315);
                cannonPosition = GetRandomPointInAreaPosition(castPosition, 300, 50);
            }
            else
            {
                cannonPosition = GetRandomPointInAreaPosition(centerPosition, 480, 100);
            }
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.CannonBarrageBall));
            SpellCast((ObjAIBase)owner, default, cannonPosition, cannonPosition, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, centerPosition, 580, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 100, 1, 1, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}
namespace Spells
{
    public class CannonBarrage : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {-0.25f, -0.25f, -0.25f};
        int[] effect1 = {0, 0, 0, 0, 0};
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 castPosition;
            Vector3 nextBuffVars_CastPosition;
            float nextBuffVars_MoveSpeedMod;
            int nextBuffVars_AttackSpeedMod;
            Minion other1;
            teamOfOwner = GetTeamID(owner);
            castPosition = GetCastSpellTargetPos();
            nextBuffVars_CastPosition = castPosition;
            nextBuffVars_MoveSpeedMod = this.effect0[level];
            nextBuffVars_AttackSpeedMod = this.effect1[level];
            other1 = SpawnMinion("HiddenMinion", "TestCube", "idle.lua", castPosition, teamOfOwner, false, true, true, true, true, true, 0, false, true);
            AddBuff((ObjAIBase)owner, other1, new Buffs.CannonBarrage(nextBuffVars_CastPosition, nextBuffVars_MoveSpeedMod, nextBuffVars_AttackSpeedMod), 1, 1, 6, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}