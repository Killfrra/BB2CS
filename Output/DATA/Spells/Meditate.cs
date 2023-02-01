#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class Meditate : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Meditate",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        float healthTick;
        float lastTimeExecuted;
        int[] effect0 = {100, 150, 200, 250, 300};
        int[] effect1 = {100, 150, 200, 250, 300};
        public Meditate(float healthTick = default)
        {
            this.healthTick = healthTick;
        }
        public override void OnActivate()
        {
            int level; // UNUSED
            Particle arr; // UNUSED
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            //RequireVar(this.healthTick);
            IncHealth(owner, this.healthTick, owner);
            SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateStats()
        {
            int level;
            level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncFlatArmorMod(owner, this.effect0[level]);
            IncFlatSpellBlockMod(owner, this.effect1[level]);
        }
        public override void OnUpdateActions()
        {
            Particle arr; // UNUSED
            if(ExecutePeriodically(0.5f, ref this.lastTimeExecuted, false))
            {
                IncHealth(owner, this.healthTick, owner);
                SpellEffectCreate(out arr, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}
namespace Spells
{
    public class Meditate : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[]{ 50f, 50f, 50f, 50f, 50f, },
            ChannelDuration = 5f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = {11.7f, 23.3f, 35, 46.7f, 58.3f};
        float[] effect1 = {25, 50, 83.3f, 125, 183.3f};
        public override void ChannelingStart()
        {
            float healthTick;
            float abilityPower;
            float nextBuffVars_HealthTick;
            healthTick = this.effect0[level];
            abilityPower = GetFlatMagicDamageMod(owner);
            abilityPower *= 0.33f;
            healthTick += abilityPower;
            nextBuffVars_HealthTick = healthTick;
            AddBuff((ObjAIBase)owner, owner, new Buffs.Meditate(nextBuffVars_HealthTick), 1, 1, 4.9f, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false);
        }
        public override void ChannelingSuccessStop()
        {
            IncHealth(owner, this.effect1[level], owner);
            SpellBuffRemove(owner, nameof(Buffs.Meditate), (ObjAIBase)owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Meditate), (ObjAIBase)owner);
        }
    }
}