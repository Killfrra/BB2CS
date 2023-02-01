#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class HowlingGale : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Howling Gale",
            BuffTextureName = "Janna_HowlingGale.dds",
            SpellToggleSlot = 1,
        };
        Vector3 facePos;
        int lifeTime;
        int level;
        Vector3 castPos;
        Particle particle;
        int[] effect0 = {14, 13, 12, 11, 10};
        public HowlingGale(Vector3 facePos = default, int lifeTime = default, int level = default, Vector3 castPos = default)
        {
            this.facePos = facePos;
            this.lifeTime = lifeTime;
            this.level = level;
            this.castPos = castPos;
        }
        public override void OnActivate()
        {
            Vector3 castPos;
            int ownerSkinID;
            //RequireVar(this.facePos);
            //RequireVar(this.lifeTime);
            //RequireVar(this.level);
            castPos = GetUnitPosition(owner);
            this.castPos = castPos;
            ownerSkinID = GetSkinID(owner);
            if(ownerSkinID == 3)
            {
                SpellEffectCreate(out this.particle, out _, "HowlingGale_Frost_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.castPos, target, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out this.particle, out _, "HowlingGale_cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, this.castPos, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID;
            Vector3 castPos; // UNUSED
            Minion other1;
            Vector3 unitPos2;
            Vector3 facePos;
            float aPMod;
            int nextBuffVars_Speed;
            int nextBuffVars_Gravity;
            int nextBuffVars_IdealDistance;
            float cooldownStat;
            float cooldownMod;
            int level;
            float cooldown;
            SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Location, owner);
            SpellEffectRemove(this.particle);
            teamID = GetTeamID(owner);
            castPos = this.castPos;
            other1 = SpawnMinion("TestCube", "TestCubeRender", "idle.lua", this.castPos, teamID, false, true, false, true, false, true, 0, false, true, (Champion)owner);
            unitPos2 = GetUnitPosition(other1);
            facePos = this.facePos;
            FaceDirection(other1, facePos);
            aPMod = GetFlatMagicDamageMod(owner);
            IncPermanentFlatMagicDamageMod(other1, aPMod);
            nextBuffVars_Speed = 150;
            nextBuffVars_Gravity = 45;
            nextBuffVars_IdealDistance = 100;
            if(this.lifeTime <= 1)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell));
            }
            else if(this.lifeTime <= 2)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell1));
            }
            else if(this.lifeTime <= 3)
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell2));
            }
            else
            {
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.ExtraSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.HowlingGaleSpell3));
            }
            SpellCast((ObjAIBase)owner, default, facePos, facePos, 0, SpellSlotType.ExtraSlots, this.level, true, true, false, false, false, true, unitPos2);
            AddBuff(attacker, other1, new Buffs.ExpirationTimer(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            cooldownStat = GetPercentCooldownMod(owner);
            cooldownMod = 1 + cooldownStat;
            level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            cooldown = this.effect0[level];
            cooldown *= cooldownMod;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldown);
        }
        public override void OnUpdateStats()
        {
            this.lifeTime = lifeTime;
        }
    }
}
namespace Spells
{
    public class HowlingGale : BBSpellScript
    {
        public override SpellScriptMetaDataNullable MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            int count;
            Vector3 targetPos;
            Vector3 castPos;
            Vector3 facePos;
            Vector3 nextBuffVars_CastPos;
            Vector3 nextBuffVars_FacePos;
            int nextBuffVars_LifeTime;
            int nextBuffVars_Level;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            count = GetBuffCountFromAll(owner, nameof(Buffs.HowlingGale));
            if(count >= 1)
            {
                SpellBuffRemove(owner, nameof(Buffs.HowlingGale), (ObjAIBase)owner, 0);
            }
            else
            {
                PlayAnimation("Spell1", 0, owner, false, false, false);
                targetPos = GetCastSpellTargetPos();
                FaceDirection(owner, targetPos);
                castPos = GetPointByUnitFacingOffset(owner, 100, 0);
                facePos = GetPointByUnitFacingOffset(owner, 200, 0);
                nextBuffVars_CastPos = castPos;
                nextBuffVars_FacePos = facePos;
                nextBuffVars_LifeTime = 0;
                nextBuffVars_Level = level;
                AddBuff((ObjAIBase)owner, owner, new Buffs.HowlingGale(nextBuffVars_FacePos, nextBuffVars_LifeTime, nextBuffVars_Level, nextBuffVars_CastPos), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                SetTargetingType(0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
            }
        }
    }
}