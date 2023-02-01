#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class AuraofDespairParticle : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "head", },
            AutoBuffActivateEffect = new[]{ "Despair_buf.troy", },
            BuffName = "AuraofDespair",
            BuffTextureName = "SadMummy_AuraofDespair.dds",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float nextBuffVars_LifeLossPercent;
            if(ExecutePeriodically(1, ref this.lastTimeExecuted, false))
            {
                nextBuffVars_LifeLossPercent = charVars.LifeLossPercent;
                if(charVars.LifeLossPercent == charVars.LastLifeLossPercent)
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.AuraofDespairDrainLife(nextBuffVars_LifeLossPercent), 1, 1, 1.2f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                    }
                }
                else
                {
                    foreach(AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.AuraofDespairDrainLife(nextBuffVars_LifeLossPercent), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
                    }
                }
                charVars.LastLifeLossPercent = charVars.LifeLossPercent;
            }
        }
    }
}