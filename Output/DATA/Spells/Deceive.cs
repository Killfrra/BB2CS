#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class Deceive : BBSpellScript
    {
        int[] effect0 = {11, 11, 11, 11, 11};
        float[] effect1 = {-0.6f, -0.4f, -0.2f, 0, 0.2f};
        public override void SelfExecute()
        {
            int nextBuffVars_DCooldown;
            Particle hi; // UNUSED
            Vector3 castPos;
            Vector3 ownerPos;
            float distance;
            Vector3 nextBuffVars_CastPos;
            float nextBuffVars_CritDmgBonus;
            nextBuffVars_DCooldown = this.effect0[level];
            SpellEffectCreate(out hi, out _, "jackintheboxpoof2.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position, owner, default, default, true, false, false, false, false);
            castPos = GetCastSpellTargetPos();
            ownerPos = GetUnitPosition(owner);
            distance = DistanceBetweenPoints(ownerPos, castPos);
            if(distance > 500)
            {
                FaceDirection(owner, castPos);
                castPos = GetPointByUnitFacingOffset(owner, 500, 0);
            }
            nextBuffVars_CastPos = castPos;
            nextBuffVars_CritDmgBonus = this.effect1[level];
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff((ObjAIBase)owner, owner, new Buffs.DeceiveFade(nextBuffVars_DCooldown, nextBuffVars_CastPos, nextBuffVars_CritDmgBonus), 1, 1, 0.05f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}
namespace Buffs
{
    public class Deceive : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deceive",
            BuffTextureName = "Jester_ManiacalCloak2.dds",
            IsDeathRecapSource = true,
        };
        float dCooldown;
        public Deceive(float dCooldown = default)
        {
            this.dCooldown = dCooldown;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dCooldown);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat;
            float multiplier;
            float newCooldown;
            SetStealthed(owner, false);
            PushCharacterFade(owner, 1, 0);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            cooldownStat = GetPercentCooldownMod(owner);
            multiplier = 1 + cooldownStat;
            newCooldown = multiplier * this.dCooldown;
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            if(spellVars.CastingBreaksStealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
            else if(!spellVars.CastingBreaksStealth)
            {
            }
            else if(!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreAttack()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}