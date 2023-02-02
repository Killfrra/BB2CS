#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class MissFortuneScattershot : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            TeamId teamOfOwner;
            Vector3 targetPos;
            Region bubbleID; // UNUSED
            Minion other3;
            teamOfOwner = GetTeamID(owner);
            targetPos = GetCastSpellTargetPos();
            bubbleID = AddPosPerceptionBubble(teamOfOwner, 200, targetPos, 2.6f, default, false);
            other3 = SpawnMinion("SpellBook1", "SpellBook1", "idle.lua", targetPos, teamOfOwner ?? TeamId.TEAM_CASTER, true, true, true, true, true, true, 0, default, true, (Champion)owner);
            AddBuff(attacker, other3, new Buffs.MissFortuneScattershot(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(attacker, other3, new Buffs.MissFortuneScatterParticle(), 1, 1, 2.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class MissFortuneScattershot : BBBuffScript
    {
        int[] effect0 = {90, 145, 200, 255, 310};
        public override void OnDeactivate(bool expired)
        {
            int level;
            float nextBuffVars_Damage;
            level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            nextBuffVars_Damage = this.effect0[level];
            AddBuff(attacker, owner, new Buffs.MissFortuneScatterAoE(nextBuffVars_Damage), 1, 1, 1.75f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}