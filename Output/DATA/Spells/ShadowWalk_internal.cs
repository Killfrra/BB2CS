#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ShadowWalk_internal : BBBuffScript
    {
        float timeLastHit;
        float moveSpeedMod;
        float stealthDuration;
        TeamId teamID;
        bool willFade;
        public ShadowWalk_internal(float timeLastHit = default, float moveSpeedMod = default, float stealthDuration = default, TeamId teamID = default)
        {
            this.timeLastHit = timeLastHit;
            this.moveSpeedMod = moveSpeedMod;
            this.stealthDuration = stealthDuration;
            this.teamID = teamID;
        }
        public override void OnActivate()
        {
            Fade iD; // UNUSED
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.stealthDuration);
            //RequireVar(this.initialTime);
            //RequireVar(this.timeLastHit);
            iD = PushCharacterFade(owner, 0.2f, 1.5f);
            this.willFade = false;
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            //RequireVar(this.teamID);
        }
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove;
            float nextBuffVars_MoveSpeedMod;
            TeamId nextBuffVars_TeamID;
            nextBuffVars_WillRemove = false;
            nextBuffVars_MoveSpeedMod = this.moveSpeedMod;
            nextBuffVars_TeamID = this.teamID;
            AddBuff((ObjAIBase)owner, owner, new Buffs.ShadowWalk(nextBuffVars_MoveSpeedMod, nextBuffVars_WillRemove, nextBuffVars_TeamID), 1, 1, this.stealthDuration, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
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
                iD = PushCharacterFade(owner, 0.2f, 1);
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
    }
}