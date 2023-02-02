#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class SilentKiller : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Silent Killer",
            BuffTextureName = "Evelynn_Stalk.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        public override void OnKill()
        {
            if(target is Champion)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    IncHealth(owner, charVars.MaliceHeal, owner);
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }
        public override void OnAssist()
        {
            if(target is Champion)
            {
                int level;
                level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    IncHealth(owner, charVars.MaliceHeal, owner);
                    SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                }
            }
        }
        public override void OnBeingHit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId attackerID;
            attackerID = GetTeamID(attacker);
            if(attackerID == TeamId.TEAM_NEUTRAL)
            {
            }
            else if(attacker is ObjAIBase)
            {
                if(attacker is Champion)
                {
                }
                else if(attacker is BaseTurret)
                {
                }
                else
                {
                    damageAmount *= 0.5f;
                }
            }
        }
    }
}