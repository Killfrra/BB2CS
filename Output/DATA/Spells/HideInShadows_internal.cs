#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HideInShadows_internal : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[]{ "", },
        };
        float timeLastHit;
        float attackSpeedMod;
        float stealthDuration;
        bool willFade;
        public HideInShadows_internal(float timeLastHit = default, float attackSpeedMod = default, float stealthDuration = default)
        {
            this.timeLastHit = timeLastHit;
            this.attackSpeedMod = attackSpeedMod;
            this.stealthDuration = stealthDuration;
        }
        public override void OnActivate()
        {
            Fade iD; // UNUSED
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            iD = PushCharacterFade(owner, 0.2f, 1.5f);
            this.willFade = false;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove;
            float nextBuffVars_AttackSpeedMod;
            nextBuffVars_WillRemove = false;
            nextBuffVars_AttackSpeedMod = this.attackSpeedMod;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HideInShadows(nextBuffVars_AttackSpeedMod, nextBuffVars_WillRemove), 1, 1, this.stealthDuration, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            float curTime;
            float timeSinceLastHit;
            curTime = GetTime();
            timeSinceLastHit = curTime - this.timeLastHit;
            if(timeSinceLastHit >= 1.5f)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(this.willFade)
            {
                Fade iD; // UNUSED
                iD = PushCharacterFade(owner, 0.2f, 1.5f);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            Fade iD; // UNUSED
            this.timeLastHit = GetTime();
            iD = PushCharacterFade(owner, 1, 0);
            this.willFade = true;
        }
        public override void OnTakeDamage(float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            Fade iD; // UNUSED
            this.timeLastHit = GetTime();
            iD = PushCharacterFade(owner, 1, 0);
            this.willFade = true;
        }
        public override void OnDeath()
        {
            if(owner.IsDead)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}