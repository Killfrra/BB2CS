#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class UdyrTigerShred : BBBuffScript
    {
        Particle lhand;
        Particle rhand;
        int[] effect0 = {30, 80, 130, 180, 230};
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(owner);
            SpellEffectCreate(out this.lhand, out _, "Udyr_Tiger_buf.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_Finger", default, owner, default, default, true, default, default, false, false);
            SpellEffectCreate(out this.rhand, out _, "Udyr_Tiger_buf_R.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "R_Finger", default, owner, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.lhand);
            SpellEffectRemove(this.rhand);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            TeamId teamID; // UNUSED
            int level;
            float baseDamage;
            float tAD;
            float dotDamage;
            float nextBuffVars_DotDamage;
            if(target is ObjAIBase)
            {
                if(target is not BaseTurret)
                {
                    teamID = GetTeamID(owner);
                    level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    baseDamage = this.effect0[level];
                    tAD = GetTotalAttackDamage(owner);
                    dotDamage = tAD * 1.5f;
                    dotDamage += baseDamage;
                    dotDamage *= 0.25f;
                    nextBuffVars_DotDamage = dotDamage;
                    AddBuff(attacker, target, new Buffs.UdyrTigerPunchBleed(nextBuffVars_DotDamage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.UdyrTigerShred), (ObjAIBase)owner, 0);
                }
            }
        }
    }
}