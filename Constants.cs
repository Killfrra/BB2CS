#nullable enable

public static class Constants
{
    public static Dictionary<string, object?> Table = new();
    static Constants()
    {
        var globals = Table;

        globals["True"] = true;
        globals["False"] = false;

        globals["AI_ORDER_NONE"] = OrderType.OrderNone;
        globals["AI_HOLD"] = OrderType.Hold;
        globals["AI_MOVETO"] = OrderType.MoveTo;
        globals["AI_ATTACKTO"] = OrderType.AttackTo;

        globals["CANCEL_ORDER"] = ForceMovementOrdersType.CANCEL_ORDER;
        globals["POSTPONE_CURRENT_ORDER"] = ForceMovementOrdersType.POSTPONE_CURRENT_ORDER;

        globals["NoTargetYet"] = null; //?
        globals["NoValidTarget"] = null; //?

        globals["BUFF_Internal"] = BuffType.INTERNAL;
        globals["BUFF_Aura"] = BuffType.AURA;
        globals["BUFF_CombatEnchancer"] = BuffType.COMBAT_ENCHANCER;
        globals["BUFF_CombatDehancer"] = BuffType.COMBAT_DEHANCER;
        //...
        globals["BUFF_Stun"] = BuffType.STUN;
        globals["BUFF_Invisibility"] = BuffType.INVISIBILITY;
        globals["BUFF_Silence"] = BuffType.SILENCE;
        globals["BUFF_Taunt"] = BuffType.TAUNT;
        globals["BUFF_Polymorph"] = BuffType.POLYMORPH;
        globals["BUFF_Slow"] = BuffType.SLOW;
        globals["BUFF_Snare"] = BuffType.SNARE;
        globals["BUFF_Damage"] = BuffType.DAMAGE;
        globals["BUFF_Heal"] = BuffType.HEAL;
        globals["BUFF_Haste"] = BuffType.HASTE;
        globals["BUFF_SpellImmunity"] = BuffType.SPELL_IMMUNITY;
        globals["BUFF_PhysicalImmunity"] = BuffType.PHYSICAL_IMMUNITY;
        globals["BUFF_Invulnerability"] = BuffType.INVULNERABILITY;
        globals["BUFF_Sleep"] = BuffType.SLEEP;
        //...
        globals["BUFF_Fear"] = BuffType.FEAR;
        //...
        globals["BUFF_Poison"] = BuffType.POISON;
        globals["BUFF_Suppression"] = BuffType.SUPPRESSION;
        globals["BUFF_Blind"] = BuffType.BLIND;
        //...
        globals["BUFF_Shred"] = BuffType.SHRED;
        //...
        globals["BUFF_AmmoStack"] = BuffType.COUNTER; //?
        globals["BUFF_Net"] = BuffType.CHARM; //?

        globals["BUFF_REPLACE_EXISTING"] = BuffAddType.REPLACE_EXISTING;
        globals["BUFF_RENEW_EXISTING"] = BuffAddType.RENEW_EXISTING;
        globals["BUFF_STACKS_AND_RENEWS"] = BuffAddType.STACKS_AND_RENEWS;
        globals["BUFF_STACKS_AND_CONTINUE"] = BuffAddType.STACKS_AND_CONTINUE;
        globals["BUFF_STACKS_AND_OVERLAPS"] = BuffAddType.STACKS_AND_OVERLAPS;

        globals["ChannelingStopCondition_NotCancelled"] = ChannelingStopCondition.NotCancelled;
        globals["ChannelingStopCondition_Success"] = ChannelingStopCondition.Success;
        globals["ChannelingStopCondition_Cancel"] = ChannelingStopCondition.Cancel;

        globals["ChannelingStopSource_NotCancelled"] = ChannelingStopSource.NotCancelled;
        globals["ChannelingStopSource_TimeCompleted"] = ChannelingStopSource.TimeCompleted;
        //...
        globals["ChannelingStopSource_LostTarget"] = ChannelingStopSource.LostTarget;
        globals["ChannelingStopSource_StunnedOrSilencedOrTaunted"] = ChannelingStopSource.StunnedOrSilencedOrTaunted;
        //...
        globals["ChannelingStopSource_Die"] = ChannelingStopSource.Die;
        //...
        globals["ChannelingStopSource_Move"] = ChannelingStopSource.Move;

        globals["DAMAGESOURCE_RAW"] = DamageSource.DAMAGE_SOURCE_RAW;
        globals["DAMAGESOURCE_INTERNALRAW"] = DamageSource.DAMAGE_SOURCE_INTERNALRAW;
        globals["DAMAGESOURCE_PERIODIC"] = DamageSource.DAMAGE_SOURCE_PERIODIC;
        globals["DAMAGESOURCE_PROC"] = DamageSource.DAMAGE_SOURCE_PROC;
        globals["DAMAGESOURCE_REACTIVE"] = DamageSource.DAMAGE_SOURCE_REACTIVE;
        //...
        globals["DAMAGESOURCE_SPELL"] = DamageSource.DAMAGE_SOURCE_SPELL;
        globals["DAMAGESOURCE_ATTACK"] = DamageSource.DAMAGE_SOURCE_ATTACK;
        globals["DAMAGESOURCE_DEFAULT"] = DamageSource.DAMAGE_SOURCE_DEFAULT;
        globals["DAMAGESOURCE_SPELLAOE"] = DamageSource.DAMAGE_SOURCE_SPELLAOE;
        globals["DAMAGESOURCE_SPELLPERSIST"] = DamageSource.DAMAGE_SOURCE_SPELLPERSIST;
        //...

        globals["TRUE_DAMAGE"] = DamageType.DAMAGE_TYPE_TRUE; //?
        globals["PHYSICAL_DAMAGE"] = DamageType.DAMAGE_TYPE_PHYSICAL; //?
        globals["MAGIC_DAMAGE"] = DamageType.DAMAGE_TYPE_MAGICAL; //?
        //...

        globals["PAR_MANA"] = PrimaryAbilityResourceType.MANA;
        globals["PAR_ENERGY"] = PrimaryAbilityResourceType.Energy;
        //...
        globals["PAR_SHIELD"] = PrimaryAbilityResourceType.Shield;
        globals["PAR_OTHER"] = PrimaryAbilityResourceType.Other;
        //...

        globals["SPELLBOOK_CHAMPION"] = SpellbookType.SPELLBOOK_CHAMPION;
        globals["SPELLBOOK_SUMMONER"] = SpellbookType.SPELLBOOK_SUMMONER;
        //...

        globals["SpellSlots"] = SpellSlotType.SpellSlots;
        //...
        globals["InventorySlots"] = SpellSlotType.InventorySlots;
        //...
        globals["ExtraSlots"] = SpellSlotType.ExtraSlots;

        globals["FACE_MOVEMENT_DIRECTION"] = ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION;
        globals["KEEP_CURRENT_FACING"] = ForceMovementOrdersFacing.KEEP_CURRENT_FACING;

        globals["FURTHEST_WITHIN_RANGE"] = ForceMovementType.FURTHEST_WITHIN_RANGE;
        globals["FIRST_COLLISION_HIT"] = ForceMovementType.FIRST_COLLISION_HIT;
        //...
        globals["FIRST_WALL_HIT"] = ForceMovementType.FIRST_WALL_HIT;

        globals["HAS_SUNGLASSES"] = ExtraAttributeFlag.HAS_SUNGLASSES;

        globals["HIT_Normal"] = HitResult.HIT_Normal;
        globals["HIT_Critical"] = HitResult.HIT_Critical;
        globals["HIT_Dodge"] = HitResult.HIT_Dodge;
        globals["HIT_Miss"] = HitResult.HIT_Miss;

        globals["TEAM_UNKNOWN"] = TeamId.TEAM_UNKNOWN;
        globals["TEAM_ORDER"] = TeamId.TEAM_BLUE;
        globals["TEAM_CHAOS"] = TeamId.TEAM_PURPLE;
        globals["TEAM_NEUTRAL"] = TeamId.TEAM_NEUTRAL;
        globals["TEAM_CASTER"] = TeamId.TEAM_CASTER; //?

        globals["TTYPE_Self"] = TargetingType.Self;
        globals["TTYPE_Target"] = TargetingType.Target;
        globals["TTYPE_Area"] = TargetingType.Area;
        //...
        globals["TTYPE_SelfAOE"] = TargetingType.SelfAOE;
        //...
        globals["TTYPE_Location"] = TargetingType.Location;

        globals["UNITSCAN_Friends"] = null; //?
        globals["SPAWN_LOCATION"] = null; //?

        globals["CO_EQUAL"] = CompareOp.CO_EQUAL;
        globals["CO_NOT_EQUAL"] = CompareOp.CO_NOT_EQUAL;
        globals["CO_LESS_THAN"] = CompareOp.CO_LESS_THAN;
        globals["CO_GREATER_THAN"] = CompareOp.CO_GREATER_THAN;
        globals["CO_LESS_THAN_OR_EQUAL"] = CompareOp.CO_LESS_THAN_OR_EQUAL;
        globals["CO_GREATER_THAN_OR_EQUAL"] = CompareOp.CO_GREATER_THAN_OR_EQUAL;
        globals["CO_SAME_TEAM"] = CompareOp.CO_SAME_TEAM;
        globals["CO_DIFFERENT_TEAM"] = CompareOp.CO_DIFFERENT_TEAM;
        globals["CO_DAMAGE_SOURCETYPE_IS"] = CompareOp.CO_DAMAGE_SOURCETYPE_IS;
        globals["CO_DAMAGE_SOURCETYPE_IS_NOT"] = CompareOp.CO_DAMAGE_SOURCETYPE_IS_NOT;
        globals["CO_IS_TYPE_AI"] = CompareOp.CO_IS_TYPE_AI;
        globals["CO_IS_NOT_AI"] = CompareOp.CO_IS_NOT_AI;
        globals["CO_IS_TYPE_HERO"] = CompareOp.CO_IS_TYPE_HERO;
        globals["CO_IS_NOT_HERO"] = CompareOp.CO_IS_NOT_HERO;
        globals["CO_IS_MELEE"] = CompareOp.CO_IS_MELEE;
        globals["CO_IS_RANGED"] = CompareOp.CO_IS_RANGED;
        globals["CO_RANDOM_CHANCE_LESS_THAN"] = CompareOp.CO_RANDOM_CHANCE_LESS_THAN;
        globals["CO_IS_TYPE_TURRET"] = CompareOp.CO_IS_TYPE_TURRET;
        globals["CO_IS_NOT_TURRET"] = CompareOp.CO_IS_NOT_TURRET;
        globals["CO_IS_DEAD"] = CompareOp.CO_IS_DEAD;
        globals["CO_IS_NOT_DEAD"] = CompareOp.CO_IS_NOT_DEAD;
        globals["CO_IS_TARGET_IN_FRONT_OF_ME"] = CompareOp.CO_IS_TARGET_IN_FRONT_OF_ME;
        globals["CO_IS_TARGET_BEHIND_ME"] = CompareOp.CO_IS_TARGET_BEHIND_ME;
        globals["CO_IS_CLONE"] = CompareOp.CO_IS_CLONE;

        globals["MO_MODULO"] = MathOp.MO_MODULO;
        globals["MO_MULTIPLY"] = MathOp.MO_MULTIPLY;
        globals["MO_ADD"] = MathOp.MO_ADD;
        globals["MO_SUBTRACT"] = MathOp.MO_SUBTRACT;
        globals["MO_DIVIDE"] = MathOp.MO_DIVIDE;
        globals["MO_MIN"] = MathOp.MO_MIN;
        globals["MO_MAX"] = MathOp.MO_MAX;
        globals["MO_ROUND"] = MathOp.MO_ROUND;
        globals["MO_ROUNDUP"] = MathOp.MO_ROUNDUP;
    }
}