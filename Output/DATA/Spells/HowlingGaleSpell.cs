#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class HowlingGaleSpell : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {60, 85, 110, 135, 160};
        int[] effect1 = {60, 85, 110, 135, 160};
        int[] effect2 = {60, 85, 110, 135, 160};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            int nextBuffVars_Speed;
            bool isStealthed;
            int nextBuffVars_Gravity;
            int nextBuffVars_IdealDistance; // UNUSED
            nextBuffVars_Speed = 150;
            nextBuffVars_Gravity = 45;
            nextBuffVars_IdealDistance = 100;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            isStealthed = GetStealthed(target);
            if(owner.Team != target.Team)
            {
                Vector3 bouncePos;
                Vector3 nextBuffVars_Position;
                if(!isStealthed)
                {
                    BreakSpellShields(target);
                    ApplyDamage((ObjAIBase)owner, target, this.effect0[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                    bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                    nextBuffVars_Position = bouncePos;
                    AddBuff((ObjAIBase)owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
                else
                {
                    if(target is Champion)
                    {
                        BreakSpellShields(target);
                        ApplyDamage((ObjAIBase)owner, target, this.effect1[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                        bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                        nextBuffVars_Position = bouncePos;
                        AddBuff((ObjAIBase)owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                    else
                    {
                        bool canSee;
                        canSee = CanSeeTarget(owner, target);
                        if(canSee)
                        {
                            BreakSpellShields(target);
                            ApplyDamage((ObjAIBase)owner, target, this.effect2[level], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, attacker);
                            bouncePos = GetRandomPointInAreaUnit(target, 100, 100);
                            nextBuffVars_Position = bouncePos;
                            AddBuff((ObjAIBase)owner, target, new Buffs.Move(nextBuffVars_Speed, nextBuffVars_Gravity, nextBuffVars_Position), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                        }
                    }
                }
            }
        }
    }
}