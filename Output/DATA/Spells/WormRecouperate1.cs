#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class WormRecouperate1 : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "", "", },
            AutoBuffActivateEffect = new[]{ "", "", "", },
            BuffName = "WormRecouperate1",
            BuffTextureName = "1035_Short_Sword.dds",
            NonDispellable = true,
        };
        public override void OnActivate()
        {
            TeamId teamID; // UNUSED
            teamID = GetTeamID(owner);
            SpellBuffRemove(owner, nameof(Buffs.WormRecouperateOn), (ObjAIBase)owner);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
        public override void OnUpdateActions()
        {
            float healthPercent;
            healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if(healthPercent < 1)
            {
                float maxHealth;
                float healthToInc;
                maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                healthToInc = maxHealth * 0.03f;
                IncHealth(owner, healthToInc, owner);
            }
            if(healthPercent >= 1)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            }
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.WormRecoupDebuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}