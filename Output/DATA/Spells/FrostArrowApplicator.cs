#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class FrostArrowApplicator : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "FrostArrow",
            BuffTextureName = "3022_Frozen_Heart.dds",
        };
        float[] effect0 = {-0.15f, -0.2f, -0.25f, -0.3f, -0.35f};
        public override void OnUpdateActions()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    if(hitResult != HitResult.HIT_Miss)
                    {
                        if(hitResult != HitResult.HIT_Dodge)
                        {
                            int level;
                            float nextBuffVars_MovementSpeedMod;
                            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                            nextBuffVars_MovementSpeedMod = this.effect0[level];
                            AddBuff((ObjAIBase)owner, target, new Buffs.FrostArrow(nextBuffVars_MovementSpeedMod), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}