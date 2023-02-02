#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class ViktorAugmentE : BBBuffScript
    {
        float abilityPower;
        Particle staffIdleRED; // UNUSED
        public override void OnActivate()
        {
            float aP;
            TeamId ownerTeam; // UNITIALIZED
            aP = GetFlatMagicDamageMod(owner);
            this.abilityPower = aP * 0.1f;
            SetSlotSpellIcon(2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, 2);
            SpellEffectCreate(out this.staffIdleRED, out _, "Viktorb_red.troy", default, ownerTeam ?? TeamId.TEAM_UNKNOWN, 200, 0, TeamId.TEAM_NEUTRAL, ownerTeam, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, "BUFFBONE_CSTM_WEAPON_1", default, false, false, false, false, false);
        }
        public override void OnUpdateActions()
        {
            float aP;
            aP = GetFlatMagicDamageMod(owner);
            aP -= this.abilityPower;
            if(GetBuffCountFromCaster(owner, owner, nameof(Buffs.ZhonyasRing)) > 0)
            {
                this.abilityPower = aP * 0.07f;
            }
            else
            {
                this.abilityPower = aP * 0.1f;
            }
        }
    }
}