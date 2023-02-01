#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class NocturneDuskbringerTrail : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NocturneDuskbringerTrail",
            BuffTextureName = "Nocturne_Duskbringer.dds",
        };
        Vector3 lastPosition;
        float lastTimeExecuted;
        float[] effect0 = {0.15f, 0.2f, 0.25f, 0.3f, 0.35f};
        int[] effect1 = {25, 35, 45, 55, 65};
        public NocturneDuskbringerTrail(Vector3 lastPosition = default)
        {
            this.lastPosition = lastPosition;
        }
        public override void OnActivate()
        {
            //RequireVar(this.lastPosition);
        }
        public override void OnUpdateActions()
        {
            TeamId casterID;
            int level;
            Vector3 curPos;
            float distance;
            Minion other3;
            float nextBuffVars_HastePercent;
            int nextBuffVars_BonusAD;
            casterID = GetTeamID(attacker);
            level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if(ExecutePeriodically(0.25f, ref this.lastTimeExecuted, true))
            {
                curPos = GetPointByUnitFacingOffset(owner, 0, 0);
                distance = DistanceBetweenPoints(this.lastPosition, curPos);
                if(distance > 25)
                {
                    other3 = SpawnMinion("DarkPath", "testcube", "idle.lua", curPos, casterID, true, true, true, true, false, true, 0, default, true);
                    nextBuffVars_HastePercent = this.effect0[level];
                    nextBuffVars_BonusAD = this.effect1[level];
                    AddBuff(attacker, other3, new Buffs.NocturneDuskbringer(nextBuffVars_HastePercent, nextBuffVars_BonusAD), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    this.lastPosition = curPos;
                }
            }
        }
    }
}