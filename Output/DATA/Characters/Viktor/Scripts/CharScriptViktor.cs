#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Chars
{
    public class CharScriptViktor : BBCharScript
    {
        Particle staffIdle;
        Particle staffIdle2;
        Particle staffIdleYELLOW; // UNUSED
        Particle staffIdleBLUE; // UNUSED
        Particle staffIdleRED; // UNUSED
        public override void OnUpdateActions()
        {
            TeamId ownerTeam;
            Particle staffIdleYELLOW2; // UNUSED
            Particle staffIdleBLUE2; // UNUSED
            Particle staffIdleRED2; // UNUSED
            if(!charVars.HasRemoved)
            {
                ownerTeam = GetTeamID(owner);
                if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentQ)) > 0)
                {
                    SpellEffectRemove(this.staffIdle);
                    SpellEffectRemove(this.staffIdle2);
                    charVars.HasRemoved = true;
                    SpellEffectCreate(out this.staffIdleYELLOW, out staffIdleYELLOW2, "Viktorb_yellow.troy", "Viktorb_yellow.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                }
                else
                {
                    if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentW)) > 0)
                    {
                        SpellEffectRemove(this.staffIdle);
                        SpellEffectRemove(this.staffIdle2);
                        charVars.HasRemoved = true;
                        SpellEffectCreate(out this.staffIdleBLUE, out staffIdleBLUE2, "Viktorb_blue.troy", "Viktorb_blue.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                    }
                    else
                    {
                        if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ViktorAugmentE)) > 0)
                        {
                            SpellEffectRemove(this.staffIdle);
                            SpellEffectRemove(this.staffIdle2);
                            charVars.HasRemoved = true;
                            SpellEffectCreate(out this.staffIdleRED, out staffIdleRED2, "Viktorb_red.troy", "Viktorb_red.troy", ownerTeam, 0, 0, TeamId.TEAM_UNKNOWN, ownerTeam, default, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            TeamId ownerTeam;
            AddBuff((ObjAIBase)owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorPassiveAPPerLev(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            CreateItem((ObjAIBase)owner, 3200);
            ownerTeam = GetTeamID(owner);
            SpellEffectCreate(out this.staffIdle, out this.staffIdle2, "Viktor_idle.troy", "Viktor_idle.troy", ownerTeam, 0, 0, TeamId.TEAM_NEUTRAL, ownerTeam, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ViktorPassiveAPPerLev(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.25f, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast((ObjAIBase)owner, owner, owner.Position, owner.Position, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}