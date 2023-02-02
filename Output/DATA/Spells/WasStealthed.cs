#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WasStealthed : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WasStealthed",
        };
        float moveSpeedMod;
        float breakDamage;
        bool willRemove;
        public WasStealthed(float moveSpeedMod = default, float breakDamage = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.breakDamage = breakDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.breakDamage);
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.teamID);
        }
        public override void OnUpdateActions()
        {
            if(this.willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            this.willRemove = true;
            if(target is ObjAIBase)
            {
                BreakSpellShields(target);
                if(target is not BaseTurret)
                {
                    float nextBuffVars_MoveSpeedMod;
                    nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
                    AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
            if(this.breakDamage > 0)
            {
                ApplyDamage((ObjAIBase)owner, target, this.breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
        public override void OnSpellHit()
        {
            float nextBuffVars_MoveSpeedMod;
            ObjAIBase caster;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            caster = SetBuffCasterUnit();
            if(caster != target)
            {
                this.willRemove = true;
                AddBuff((ObjAIBase)owner, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 3, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            if(this.breakDamage > 0)
            {
                ApplyDamage((ObjAIBase)owner, target, this.breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
            }
        }
    }
}