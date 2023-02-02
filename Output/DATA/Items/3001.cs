#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Items
{
    public class _3001 : BBItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if(ExecutePeriodically(0.9f, ref this.lastTimeExecuted, false))
            {
                if(!owner.IsDead)
                {
                    int nextBuffVars_MagicResistanceMod;
                    nextBuffVars_MagicResistanceMod = -20;
                    AddBuff((ObjAIBase)owner, owner, new Buffs.AbyssalScepterAuraSelf(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 1000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.AbyssalScepterAura), false))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.AbyssalScepterAura(nextBuffVars_MagicResistanceMod), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                    }
                }
            }
        }
    }
}