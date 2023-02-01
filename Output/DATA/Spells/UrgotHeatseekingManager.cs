#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UrgotHeatseekingManager : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        float reloadRate;
        public UrgotHeatseekingManager(float reloadRate = default)
        {
            this.reloadRate = reloadRate;
        }
        public override void OnActivate()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotHeatseekingAmmo(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
        }
        public override void OnUpdateActions()
        {
            if(ExecutePeriodically(0, ref this.lastTimeExecuted, false, this.reloadRate))
            {
                if(!owner.IsDead)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.UrgotHeatseekingAmmo(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            int count;
            spellName = GetSpellName();
            if(spellName == nameof(Spells.UrgotHeatseekingMissile))
            {
                count = GetBuffCountFromAll(owner, nameof(Buffs.UrgotHeatseekingAmmo));
                if(count == 4)
                {
                    ExecutePeriodicallyReset(out this.lastTimeExecuted);
                }
            }
        }
    }
}