#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HowlingGaleSpell4 : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {160, 220, 280, 340, 400};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            Vector3 bouncePos;
            Vector3 nextBuffVars_Position;
            int nextBuffVars_IdealDistance;
            float nextBuffVars_Speed;
            float nextBuffVars_Gravity;
            attacker = SetBuffCasterUnit();
            if(attacker != target)
            {
                BreakSpellShields(target);
                ApplyDamage(attacker, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                nextBuffVars_Position = bouncePos;
                nextBuffVars_IdealDistance = 100;
                nextBuffVars_Speed = 100;
                nextBuffVars_Gravity = 20;
                AddBuff(attacker, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
        }
    }
}