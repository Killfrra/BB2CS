#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class CrestofCrushingWrath : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Crest Of Crushing Wrath",
            BuffTextureName = "WaterWizard_Vortex.dds",
            NonDispellable = true,
        };
        Particle buffParticle;
        float damageVar;
        float lastTimeExecuted;
        int[] effect0 = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18};
        public override void OnActivate()
        {
            SpellEffectCreate(out this.buffParticle, out _, "Speed_runes_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
            this.damageVar = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.buffParticle);
        }
        public override void OnUpdateStats()
        {
            float level;
            level = GetLevel(owner);
            level *= 0.01f;
            IncPercentPhysicalDamageMod(owner, level);
            IncPercentMagicDamageMod(owner, level);
        }
        public override void OnUpdateActions()
        {
            int level;
            float currentDamage;
            if(ExecutePeriodically(10, ref this.lastTimeExecuted, true))
            {
                level = GetLevel(owner);
                currentDamage = this.effect0[level];
                if(currentDamage > this.damageVar)
                {
                    this.damageVar = currentDamage;
                    SetBuffToolTipVar(1, currentDamage);
                }
            }
        }
    }
}