#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class DeceiveFade : BBBuffScript
    {
        int dCooldown;
        Vector3 castPos;
        float critDmgBonus;
        public DeceiveFade(int dCooldown = default, Vector3 castPos = default, float critDmgBonus = default)
        {
            this.dCooldown = dCooldown;
            this.castPos = castPos;
            this.critDmgBonus = critDmgBonus;
        }
        public override void OnActivate()
        {
            Fade iD; // UNUSED
            //RequireVar(this.castPos);
            //RequireVar(this.critDmgBonus);
            //RequireVar(this.dCooldown);
            iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            float nextBuffVars_CritDmgBonus;
            Vector3 castPos;
            float nextBuffVars_DCooldown;
            nextBuffVars_CritDmgBonus = this.critDmgBonus;
            nextBuffVars_DCooldown = this.dCooldown;
            castPos = this.castPos;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Deceive(nextBuffVars_DCooldown), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INVISIBILITY, 0, true, false, false);
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeceiveCritBonus(nextBuffVars_CritDmgBonus), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            TeleportToPosition(owner, castPos);
        }
        public override void OnUpdateStats()
        {
            IncFlatCritChanceMod(owner, 1);
            SetStealthed(owner, true);
        }
        public override void OnPreAttack()
        {
            float nextBuffVars_CritDmgBonus;
            nextBuffVars_CritDmgBonus = this.critDmgBonus;
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeceiveCritBonus(nextBuffVars_CritDmgBonus), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}