#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CaitlynYordleTrapSight : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "global_Watched.troy", "", },
            BuffName = "CaitlynYordleTrapSight",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        float damagePerTick;
        float dOTCounter;
        Region bubbleID;
        Region bubbleID2;
        float lastTimeExecuted;
        int[] effect0 = {80, 130, 180, 230, 280};
        public override void OnActivate()
        {
            int level;
            float baseDamage;
            float aP;
            float aPBonus;
            float totalDamage;
            TeamId team;
            level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            baseDamage = this.effect0[level];
            aP = GetFlatMagicDamageMod(attacker);
            aPBonus = aP * 0.6f;
            totalDamage = baseDamage + aPBonus;
            this.damagePerTick = totalDamage / 3;
            this.dOTCounter = 0;
            team = GetTeamID(attacker);
            this.bubbleID = AddUnitPerceptionBubble(team, 100, owner, 20, default, default, false);
            this.bubbleID2 = AddUnitPerceptionBubble(team, 100, owner, 20, default, default, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(this.bubbleID);
            RemovePerceptionBubble(this.bubbleID2);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, true))
            {
                if(this.dOTCounter < 1.5f)
                {
                    this.dOTCounter += 0.5f;
                    ApplyDamage(attacker, owner, this.damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}