#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class MordekaiserCreepingDeathSelf : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "mordekaiser_creepingDeath_aura.troy", },
            BuffName = "MordekaiserCreepingDeath",
            BuffTextureName = "FallenAngel_TormentedSoil.dds",
        };
        float damagePerTick;
        float lastTimeExecuted;
        public MordekaiserCreepingDeathSelf(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    float nextBuffVars_DamagePerTick;
                    nextBuffVars_DamagePerTick = this.damagePerTick;
                    AddBuff((ObjAIBase)owner, unit, new Buffs.MordekaiserCreepingDeathDebuff(nextBuffVars_DamagePerTick), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0);
                }
            }
        }
    }
}