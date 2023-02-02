#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FrostArrow : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float baseAttackDamage;
            if(target is ObjAIBase)
            {
                if(target is BaseTurret)
                {
                }
                else
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.FrostArrowApplicator(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            baseAttackDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class FrostArrow : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "Global_Freeze.troy", },
            BuffName = "Frost Arrow",
            BuffTextureName = "3022_Frozen_Heart.dds",
            IsDeathRecapSource = true,
            PopupMessage = new[]{ "game_floatingtext_Slowed", },
        };
        float movementSpeedMod;
        public FrostArrow(float movementSpeedMod = default)
        {
            this.movementSpeedMod = movementSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.movementSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, this.movementSpeedMod);
        }
    }
}