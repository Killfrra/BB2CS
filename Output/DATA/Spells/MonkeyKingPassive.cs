#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MonkeyKingPassive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MonkeyKingPassive",
            BuffTextureName = "Cassiopeia_PetrifyingGaze.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defenseToAdd;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            this.defenseToAdd = 0;
            SetBuffToolTipVar(1, 4);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, this.defenseToAdd);
            IncFlatSpellBlockMod(owner, this.defenseToAdd);
        }
        public override void OnUpdateActions()
        {
            float count;
            bool canSee;
            int ownerLevel;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, true))
            {
                count = 0;
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1400, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    canSee = CanSeeTarget(owner, unit);
                    if(canSee)
                    {
                        count++;
                    }
                }
                ownerLevel = GetLevel(owner);
                if(ownerLevel > 12)
                {
                    this.defenseToAdd = count * 8;
                    this.defenseToAdd = Math.Min(this.defenseToAdd, 40);
                    SetBuffToolTipVar(1, 8);
                }
                else if(ownerLevel > 6)
                {
                    this.defenseToAdd = count * 6;
                    this.defenseToAdd = Math.Min(this.defenseToAdd, 30);
                    SetBuffToolTipVar(1, 6);
                }
                else
                {
                    this.defenseToAdd = count * 4;
                    this.defenseToAdd = Math.Min(this.defenseToAdd, 20);
                    SetBuffToolTipVar(1, 4);
                }
            }
        }
    }
}