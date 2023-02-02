#nullable disable

using System.Numerics;
using static Functions;
using static Functions_CS;
using Math = System.Math;

namespace Buffs
{
    public class TalonShadowAssaultBuff : BBBuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TalonDisappear",
            BuffTextureName = "TalonShadowAssault.dds",
            SpellToggleSlot = 1,
        };
        Fade iD;
        Particle talon_ult_sound;
        public override void OnActivate()
        {
            TeamId teamID;
            float nextBuffVars_MoveSpeedMod;
            Vector3 ownerPos;
            teamID = GetTeamID(owner);
            nextBuffVars_MoveSpeedMod = 0.4f;
            this.iD = PushCharacterFade(owner, 0.2f, 0);
            SetStealthed(owner, true);
            AddBuff((ObjAIBase)owner, owner, new Buffs.TalonHaste(nextBuffVars_MoveSpeedMod), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ownerPos = GetUnitPosition(owner);
            SpellEffectCreate(out this.talon_ult_sound, out _, "talon_ult_sound.troy", default, teamID ?? TeamId.TEAM_UNKNOWN, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, ownerPos, default, default, ownerPos, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultMisOne), (ObjAIBase)owner, 0);
            SpellBuffRemove(owner, nameof(Buffs.TalonHaste), (ObjAIBase)owner, 0);
            SetStealthed(owner, false);
            PopCharacterFade(owner, this.iD);
            SpellEffectRemove(this.talon_ult_sound);
        }
        public override void OnSpellCast(string spellName, SpellScriptMetaData spellVars)
        {
            PopCharacterFade(owner, this.iD);
            SetStealthed(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), (ObjAIBase)owner, 0);
        }
        public override void OnHitUnit(float damageAmount, DamageType damageType, DamageSource damageSource, HitResult hitResult)
        {
            PopCharacterFade(owner, this.iD);
            SetStealthed(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.TalonShadowAssaultBuff), (ObjAIBase)owner, 0);
        }
    }
}