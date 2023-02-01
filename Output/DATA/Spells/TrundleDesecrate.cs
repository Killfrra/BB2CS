#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TrundleDesecrate : BBBuffScript
    {
        Particle particle2;
        Particle particle;
        float[] effect0 = {0.2f, 0.3f, 0.4f, 0.5f, 0.6f};
        float[] effect1 = {0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
        float[] effect2 = {0.8f, 0.75f, 0.7f, 0.65f, 0.6f};
        public override void OnActivate()
        {
            TeamId teamID;
            teamID = GetTeamID(attacker);
            SpellEffectCreate(out this.particle2, out this.particle, "trundledesecrate_green.troy", "trundledesecrate_red.troy", teamID, 10, 0, TeamId.TEAM_BLUE, default, default, false, owner, default, default, owner, default, default, false, default, default, false, false);
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(this.particle);
            SpellEffectRemove(this.particle2);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 0, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnUpdateStats()
        {
            int level;
            float nextBuffVars_SelfASMod;
            float nextBuffVars_SelfMSMod;
            float nextBuffVars_CCReduc;
            foreach(AttackableUnit unit in GetUnitsInArea(attacker, owner.Position, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, nameof(Buffs.TrundleDiseaseOverseer), true))
            {
                level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_SelfASMod = this.effect0[level];
                nextBuffVars_SelfMSMod = this.effect1[level];
                nextBuffVars_CCReduc = this.effect2[level];
                AddBuff((ObjAIBase)unit, unit, new Buffs.TrundleDesecrateBuffs(nextBuffVars_SelfASMod, nextBuffVars_SelfMSMod, nextBuffVars_CCReduc), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetCanAttack(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
        }
    }
}
namespace Spells
{
    public class TrundleDesecrate : BBSpellScript
    {
        public override void SelfExecute()
        {
            Vector3 ownerPos;
            TeamId teamID;
            Minion other1;
            ownerPos = GetCastSpellTargetPos();
            teamID = GetTeamID(owner);
            other1 = SpawnMinion("birds", "TestCube", "idle.lua", ownerPos, teamID, true, true, true, true, true, true, 0, false, true);
            AddBuff(attacker, other1, new Buffs.TrundleDesecrate(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell2", 1, owner, false, true, true);
        }
    }
}