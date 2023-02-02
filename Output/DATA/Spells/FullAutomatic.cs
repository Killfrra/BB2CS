#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Spells
{
    public class FullAutomatic : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 90f, 75f, 60f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = {5, 6, 7};
        int[] effect1 = {15, 25, 35};
        public override void TargetExecute(SpellMissile missileNetworkID, HitResult hitResult)
        {
            float nextBuffVars_numAttacks;
            float nextBuffVars_bonusDamage;
            nextBuffVars_numAttacks = this.effect0[level];
            nextBuffVars_bonusDamage = this.effect1[level];
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, level, false);
            AddBuff(attacker, attacker, new Buffs.FullAutomatic(nextBuffVars_numAttacks, nextBuffVars_bonusDamage), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, attacker, new Buffs.TwitchSprayAndPray(), 10, nextBuffVars_numAttacks, 12, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class FullAutomatic : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[]{ "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[]{ "twitch_ambush_buf.troy", "twitch_ambush_buf.troy", "twitch_ambush_buf_02.troy", },
            BuffName = "Full Automatic",
            BuffTextureName = "Twitch_Clone.dds",
            SpellFXOverrideSkins = new[]{ "GangsterTwitch", "PunkTwitch", },
        };
        float numAttacks;
        float bonusDamage;
        public FullAutomatic(float numAttacks = default, float bonusDamage = default)
        {
            this.numAttacks = numAttacks;
            this.bonusDamage = bonusDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.numAttacks);
            //RequireVar(this.bonusDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemoveStacks(owner, owner, nameof(Buffs.TwitchSprayAndPray), 0);
        }
        public override void OnUpdateStats()
        {
            IncFlatAttackRangeMod(owner, 375);
            IncFlatPhysicalDamageMod(owner, this.bonusDamage);
        }
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            this.numAttacks--;
            SpellBuffRemove(owner, nameof(Buffs.TwitchSprayAndPray), (ObjAIBase)owner);
            if(this.numAttacks <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}