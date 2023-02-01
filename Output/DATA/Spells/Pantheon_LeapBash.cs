#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Pantheon_LeapBash : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "pantheon_aegis_self.troy", },
        };
        float damageLvl;
        float stunLength;
        public Pantheon_LeapBash(float damageLvl = default, float stunLength = default)
        {
            this.damageLvl = damageLvl;
            this.stunLength = stunLength;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageLvl);
            //RequireVar(this.stunLength);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster;
            float distanceTar;
            caster = SetBuffCasterUnit();
            distanceTar = DistanceBetweenObjects("Caster", "Owner");
            if(distanceTar <= 800)
            {
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, this.damageLvl, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 1, false, false, (ObjAIBase)owner);
                ApplyStun(owner, caster, this.stunLength);
            }
            if(target is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}
namespace Spells
{
    public class Pantheon_LeapBash : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {50, 75, 100, 125, 150};
        int[] effect1 = {1, 1, 1, 1, 1};
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove;
            bool canCast;
            canMove = GetCanMove(owner);
            canCast = GetCanCast(owner);
            if(!canMove)
            {
                returnValue = false;
            }
            if(!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            int count;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield2)) == 0)
            {
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.Pantheon_AegisShield)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_Aegis_Counter(), 5, 1, 25000, BuffAddType.STACKS_AND_OVERLAPS, BuffType.AURA, 0, false, false, false);
                    count = GetBuffCountFromAll(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    if(count >= 4)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.Pantheon_AegisShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                        SpellBuffClear(owner, nameof(Buffs.Pantheon_Aegis_Counter));
                    }
                }
            }
        }
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_DamageLvl;
            float nextBuffVars_stunLength;
            float distance;
            float gravityVar;
            float speedVar;
            float factor;
            Vector3 targetPos;
            nextBuffVars_DamageLvl = this.effect0[level];
            nextBuffVars_stunLength = this.effect1[level];
            AddBuff((ObjAIBase)target, owner, new Buffs.Pantheon_LeapBash(nextBuffVars_DamageLvl, nextBuffVars_stunLength), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            distance = DistanceBetweenObjects("Attacker", "Target");
            if(distance >= 600)
            {
                gravityVar = 60;
                speedVar = 1150;
            }
            else if(distance >= 500)
            {
                gravityVar = 70;
                speedVar = 1075;
            }
            else if(distance >= 375)
            {
                gravityVar = 80;
                speedVar = 1000;
            }
            else if(distance >= 275)
            {
                gravityVar = 100;
                speedVar = 950;
            }
            else if(distance >= 175)
            {
                gravityVar = 120;
                speedVar = 900;
            }
            else if(distance >= 75)
            {
                gravityVar = 150;
                speedVar = 875;
            }
            else if(distance >= 0)
            {
                gravityVar = 300;
                speedVar = 850;
            }
            factor = distance / 600;
            factor = Math.Max(factor, 0.75f);
            factor = Math.Min(factor, 1.5f);
            PlayAnimation("Spell2", factor, attacker, false, false, true);
            targetPos = GetUnitPosition(target);
            Move(attacker, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
    }
}