#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class ShenShadowStep : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float distance;
            float gravityVar;
            float speedVar;
            AddBuff((ObjAIBase)target, attacker, new Buffs.ShenShadowStep(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            distance = DistanceBetweenObjects("Attacker", "Target");
            if(distance >= 600)
            {
                gravityVar = 70;
                speedVar = 1150;
            }
            else if(distance >= 500)
            {
                gravityVar = 80;
                speedVar = 1150;
            }
            else if(distance >= 400)
            {
                gravityVar = 100;
                speedVar = 1080;
            }
            else if(distance >= 300)
            {
                gravityVar = 120;
                speedVar = 1010;
            }
            else if(distance >= 200)
            {
                gravityVar = 150;
                speedVar = 950;
            }
            else if(distance >= 100)
            {
                gravityVar = 300;
                speedVar = 900;
            }
            else if(distance >= 0)
            {
                gravityVar = 1000;
                speedVar = 900;
            }
            Move(attacker, target.Position, speedVar, gravityVar, 100, ForceMovementType.FURTHEST_WITHIN_RANGE);
        }
    }
}
namespace Buffs
{
    public class ShenShadowStep : BBBuffScript
    {
        bool hasDealtDamage;
        public override void OnActivate()
        {
            this.hasDealtDamage = false;
        }
        public override void OnUpdateActions()
        {
            if(!this.hasDealtDamage)
            {
                float distance;
                distance = DistanceBetweenObjects("Owner", "Attacker");
                if(distance <= 500)
                {
                    this.hasDealtDamage = true;
                    SpellCast((ObjAIBase)owner, attacker, attacker.Position, attacker.Position, 0, SpellSlotType.ExtraSlots, 1, true, false, false);
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}