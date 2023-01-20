#nullable enable

using System.Numerics;

public static partial class Functions
{
    [BBFunc(Dest = "Position")]
    public static Vector3 GetPointByUnitFacingOffset(AttackableUnit unit, float distance, float offsetAngle){
        return default!;
    }

    [BBFunc]
    public static float DistanceBetweenPoints(Vector3 point1, Vector3 point2){
        return default!;
    }

    [BBFunc(Dest = "Seconds")]
    public static float GetGameTime(){
        return default!;
    }

    [BBFunc]
    public static Minion SpawnMinion(
        string name,
        string skin,
        string? aiScript,
        Vector3 pos,
        TeamId team,
        bool stunned,
        bool rooted,
        bool silenced,
        bool invulnerable,
        bool magicImmune,
        bool ignoreCollision,
        float visibilitySize,
        bool isWard = false,
        bool placemarker = false,
        Champion? goldRedirectTarget = null,

        ObjAIBase? owner = null // for non-BB
    ){
        return default!;
    }

    [BBFunc] //TODO: BB-only and Lua-only versions?
    public static void SpellBuffAdd(
        // Beginning of positional parameters
        ObjAIBase attacker,
        AttackableUnit target,
        string buffName = "",
        int maxStack = 1, //TODO: Maybe there should be "TickRate"?
        int numberOfStacks = 1,
        float duration = 25000,

        [BBParam("", null, null, null)]
        object? buffVarsTable = null,
        // Ending of positional parameters

        BuffAddType buffAddType = BuffAddType.REPLACE_EXISTING,
        BuffType buffType = BuffType.INTERNAL,
        float tickRate = 0,
        bool stacksExclusive = false,
        bool canMitigateDuration = false,
        bool isHiddenOnClient = false,

        Spell? originSpell = null // for non-BB
    ){}

    [BBFunc]
    public static void SpellBuffRemoveCurrent(AttackableUnit target){}

    [BBFunc]
    public static void SpellBuffRemove(
        AttackableUnit target,
        string buffName,
        ObjAIBase? attacker = null,
        float resetDuration = 0
    ){}

    [BBFunc]
    public static void PreloadParticle(string name){}

    [BBFunc]
    public static void PreloadSpell(string name){}

    [BBFunc]
    public static void PreloadCharacter(string name){}

    [BBFunc]
    public static void SetSlotSpellCooldownTime(
        ObjAIBase owner,
        [BBParam(ValuePostfix = "Value")] int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType = SpellSlotType.SpellSlots,
        [BBParam(ValuePostfix = "Value")] float src = 0
    ){}

    [BBFunc(Dest = "Position")]
    public static Vector3 GetUnitPosition(AttackableUnit? unit = null){
        return default!;
    }

    [BBFunc]
    public static Vector3 GetCastSpellTargetPos(){
        return default!;
    }

    [BBFunc]
    public static TeamId GetTeamID(AttackableUnit? u = null)
    {
        return default!;
    }

    [BBFunc]
    public static void Move(
        AttackableUnit unit,
        Vector3 target,
        float speed,
        float gravity,
        float moveBackBy,
        ForceMovementType movementType,
        ForceMovementOrdersType movementOrdersType,
        float idealDistance,
        ForceMovementOrdersFacing movementOrdersFacing = ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION
    ){}

    [BBFunc]
    public static void ApplyAssistMarker(
        ObjAIBase source,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc]
    public static void SpellEffectCreate(
        out Particle effectID,
        out Particle? effectID2,

        string effectName = "",
        string effectNameForOtherTeam = "",

        [BBParam("OverrideVar", "OverrideVarTable", "", null)]
        TeamId FOWTeam = TeamId.TEAM_UNKNOWN,
        float FOWVisibilityRadius = 0,

        FXFlags flags = 0,

        TeamId specificTeamOnly = TeamId.TEAM_UNKNOWN,
        AttackableUnit? specificUnitOnly = null,
        bool useSpecificUnit = false,

        AttackableUnit? bindObject = null,
        string boneName = "",
        Vector3 pos = default,

        AttackableUnit? targetObject = null,
        string targetBoneName = "",
        Vector3 targetPos = default,

        bool sendIfOnScreenOrDiscard = false,
        bool persistsThroughReconnect = false,
        bool bindFlexToOwnerPAR = false,
        bool followsGroundTilt = false,
        bool facesTarget = false,

        object? orientTowards = null, // Vector3 or AttackableUnit

        ObjAIBase? caster = null // for non-BB
    ){
        effectID = effectID2 = default!;
    }

    [BBFunc]
    public static void SpellEffectRemove(
        Particle? effectID
    ){}

    [BBFunc]
    public static void ApplyDamage(
        ObjAIBase attacker,
        AttackableUnit target,
        float damage,
        DamageType damageType,
        DamageSource sourceDamageType,
        float percentOfAttack = 0,
        float spellDamageRatio = 0,
        float physicalDamageRatio = 0,
        bool ignoreDamageIncreaseMods = false,
        bool ignoreDamageCrit = false,

        ObjAIBase? callForHelpAttacker = null
    ){}

    [BBFunc]
    public static void SetBuffToolTipVar(
        int index,
        float value
    ){}

    [BBFunc]
    public static void ForEachUnitInTargetArea(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        string? buffNameFilter,
        bool? inclusiveBuffFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc]
    public static void ForEachUnitInTargetAreaRandom(
        ObjAIBase attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        string? buffNameFilter,
        bool? inclusiveBuffFilter,
        int maximumUnitsToPick,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc]
    public static int GetNumberOfHeroesOnTeam(
        TeamId team,
        bool connectedFromStart,
        bool includeBots = true
    ){
        return default!;
    }

    [BBFunc]
    public static void SpellBuffClear(AttackableUnit target, string buffName){}

    [BBFunc]
    public static void MoveAway(
        AttackableUnit unit,
        Vector3 awayFrom,
        float speed,
        float gravity,
        float distance,
        float distanceInner,
        ForceMovementType movementType,
        ForceMovementOrdersType movementOrdersType,
        float idealDistance,
        ForceMovementOrdersFacing movementOrdersFacing
    ){}

    [BBFunc(Dest = "Result")]
    public static Vector3 GetRandomPointInAreaUnit(
        AttackableUnit target,
        float radius,
        float innerRadius
    ){
        return default!;
    }

    [BBFunc(Dest = "BubbleID")]
    public static Region AddPosPerceptionBubble(
        TeamId team,
        float radius,
        Vector3 pos,
        float duration,
        AttackableUnit? specificUnitsClientOnly,
        bool revealSteath
    ){
        return default!;
    }

    [BBFunc]
    public static void FaceDirection(
        AttackableUnit target,
        Vector3 location
    ){}

    [BBFunc]
    public static void SetSpell(
        ObjAIBase target,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType slotBook,
        string spellName
    ){}

    public static string GetSlotSpellName(
        ObjAIBase owner,
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType
    ){
        return default!;
    }
    public static float GetSlotSpellCooldownTime(
        ObjAIBase owner,
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType
    ){
        return default!;
    }
    public static int GetSlotSpellLevel(
        ObjAIBase owner,
        int spellSlot,
        SpellbookType spellbookType,
        SpellSlotType slotType = SpellSlotType.SpellSlots
    ){
        return default!;
    }

    [BBFunc]
    public static void SpellCast(
        ObjAIBase caster,
        AttackableUnit? target,
        Vector3? pos,
        Vector3? endPos,
        bool? overrideCastPosition,
        int slotNumber,
        SpellSlotType slotType,
        int overrideForceLevel,
        bool overrideCoolDownCheck,
        bool fireWithoutCasting,
        bool useAutoAttackSpell,
        bool forceCastingOrChannelling = false,
        bool updateAutoAttackTimer = false,
        Vector3? overrideCastPos = null
    ){}

    [BBFunc]
    public static void SealSpellSlot(
        int spellSlot,
        SpellSlotType slotType,
        ObjAIBase target,
        bool state,
        SpellbookType spellbookType = SpellbookType.SPELLBOOK_CHAMPION //TODO: Verify
    ){}

    [BBFunc]
    public static void SpellBuffRemoveType(
        AttackableUnit target,
        BuffType type
    ){}

    [BBFunc]
    public static void SetSlotSpellCooldownTimeVer2(
        float src,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType spellbookType,
        ObjAIBase owner,
        bool? broadcastEvent
    ){}

    [BBFunc]
    public static void SetSpellToolTipVar(
        float value,
        int index,
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType slotBook,
        Champion target
    ){}

    [BBFunc]
    public static void OverrideAnimation(
        string toOverrideAnim,
        string overrideAnim,
        AttackableUnit owner
    ){}

    [BBFunc]
    public static float GetTotalAttackDamage(
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static int GetLevel(
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static void IncHealth(
        AttackableUnit target,
        float delta,
        AttackableUnit healer
    ){}

    [BBFunc]
    public static void PlayAnimation(
        string animationName,
        float scaleTime,
        AttackableUnit target,
        bool loop,
        bool blend,
        bool Lock = false
    ){}

    [BBFunc(Dest = "Caster")]
    public static AttackableUnit SetBuffCasterUnit(){
        return default!;
    }

    [BBFunc]
    public static void ClearOverrideAnimation(
        string toOverrideAnim,
        AttackableUnit owner
    ){}

    [BBFunc]
    public static void ForNClosestUnitsInTargetArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        bool? inclusiveBuffFilter,
        string? buffNameFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc(Dest = "Result")]
    public static bool CanSeeTarget(
        AttackableUnit viewer,
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc(Dest = "SkinID")]
    public static int GetSkinID(AttackableUnit unit){
        return default!;
    }

    [BBFunc]
    public static void DestroyMissile(
        SpellMissile missileID
    ){}

    [BBFunc(Dest = "BubbleID")]
    public static Region AddUnitPerceptionBubble(
        TeamId team,
        float radius,
        AttackableUnit target,
        float duration,
        AttackableUnit specificUnitsClientOnly,
        AttackableUnit revealSpecificUnitOnly,
        bool revealSteath
    ){
        return default!;
    }

    [BBFunc]
    public static void UnlockAnimation(
        AttackableUnit owner,
        bool blend = false
    ){}

    [BBFunc]
    public static void RemovePerceptionBubble(
        Region bubbleID
    ){}

    public static string GetSpellName(AttackableUnit target)
    {
        return default!;
    }
    public static float GetPARCost(AttackableUnit target)
    {
        return default!;
    }
    public static int GetSpellSlot(AttackableUnit target)
    {
        return default!;
    }
    public static int GetCastSpellLevelPlusOne(AttackableUnit target)
    {
        return default!;
    }
    public static bool GetIsAttackOverride(AttackableUnit target)
    {
        return default!;
    }
    public static int GetCastSpellTargetsHitPlusOne(AttackableUnit target)
    {
        return default!;
    }

    [BBFunc]
    public static void SetDodgePiercing(
        AttackableUnit target,
        bool value
    ){}

    [BBFunc]
    public static void Say(
        AttackableUnit owner,
        string toSay,
        object? src
    ){}

    [BBFunc(Dest = "ID")]
    public static Fade? PushCharacterFade(
        AttackableUnit target,
        float fadeAmount,
        float fadeTime,
        Fade? ID = null
    ){
        return default!;
    }

    [BBFunc]
    public static void PopCharacterFade(
        AttackableUnit target,
        Fade? ID
    ){}

    [BBFunc]
    public static void OverrideAutoAttack(
        int spellSlot,
        SpellSlotType slotType,
        AttackableUnit owner,
        int autoAttackSpellLevel,
        bool cancelAttack
    ){}

    [BBFunc]
    public static void ApplyStun(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc]
    public static void RemoveOverrideAutoAttack(
        AttackableUnit owner,
        bool cancelAttack
    ){}

    [BBFunc]
    public static void SetPARCostInc(
        ObjAIBase spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        float cost,
        PrimaryAbilityResourceType PARType
    ){}


    [BBFunc]
    public static void CancelAutoAttack(
        AttackableUnit target,
        bool reset
    ){}

    [BBFunc]
    public static float GetBuffRemainingDuration(
        AttackableUnit target,
        string buffName
    ){
        return default!;
    }

    [BBFunc]
    public static Champion? GetChampionBySkinName(
        string skin,
        TeamId team
    ){
        return default!;
    }

    [BBFunc]
    public static void IssueOrder(
        AttackableUnit whomToOrder,
        OrderType order,
        Vector3? targetOfOrderPosition,
        AttackableUnit? targetOfOrder
    ){}

    [BBFunc]
    public static void LinkVisibility(
        AttackableUnit unit1,
        AttackableUnit unit2
    ){}

    [BBFunc]
    public static void ApplyTaunt(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc]
    public static void StopChanneling(
        ObjAIBase caster,
        ChannelingStopCondition stopCondition,
        ChannelingStopSource stopSource
    ){}

    [BBFunc]
    public static void ForEachChampion(
        TeamId team,
        string? buffNameFilter,
        bool? inclusiveBuffFilter,

        [BBSubBlocks("Iterator")]
        Action<Champion> subBlocks
    ){}

    [BBFunc]
    public static void DebugSay(
        AttackableUnit owner,
        string toSay,
        object? src
    ){}

    [BBFunc]
    public static void TeleportToPosition(
        AttackableUnit owner,
        [BBParam("Name", null, "", null)]
        Vector3 castPosition
    ){}

    [BBFunc]
    public static void IncScaleSkinCoef(
        float scale,
        AttackableUnit owner
    ){}

    [BBFunc(Dest = "ID")]
    public static int PushCharacterData(
        string skinName,
        AttackableUnit target,
        bool overrideSpells
    ){
        return default!;
    }

    [BBFunc]
    public static void ApplySilence(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc]
    public static float DistanceBetweenObjectAndPoint(
        AttackableUnit Object,
        Vector3 point
    ){
        return default!;
    }

    [BBFunc]
    public static string GetUnitSkinName(
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static ObjAIBase GetPetOwner(
        Pet pet
    ){
        return default!;
    }

    [BBFunc]
    public static void StartTrackingCollisions(
        AttackableUnit target,
        bool value
    ){}

    [BBFunc]
    public static void IncGold(
        AttackableUnit target,
        float delta
    ){}

    [BBFunc]
    public static void SetTargetingType(
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType? slotBook,
        AttackableUnit targetType,
        AttackableUnit target
    ){}

    [BBFunc]
    public static void ForNClosestVisibleUnitsInTargetArea(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        bool? inclusiveBuffFilter,
        string? buffNameFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc]
    public static void ForEachUnitInTargetRectangle(
        AttackableUnit attacker,
        Vector3 center,
        float halfWidth,
        float halfLength,
        SpellDataFlags flags,
        bool? inclusiveBuffFilter,
        string? buffNameFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc]
    public static void DestroyMissileForTarget(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void TeleportToKeyLocation(
        AttackableUnit owner,
        SpawnType location,
        TeamId team
    ){}

    [BBFunc]
    public static void SetPARColorOverride(
        AttackableUnit spellSlotOwner,
        int colorR,
        int colorG,
        int colorB,
        int colorA,
        int fadeR,
        int fadeG,
        int fadeB,
        int fadeA
    ){}

    [BBFunc]
    public static float GetArmor(
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static void SetPARMultiplicativeCostInc(
        AttackableUnit spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        float cost,
        PrimaryAbilityResourceType PARType
    ){}

    [BBFunc]
    public static void PopAllCharacterData(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetSpellOffsetTarget(
        int slotNumber,
        SpellSlotType slotType,
        string spellName,
        SpellbookType spellbookType,
        AttackableUnit owner,
        AttackableUnit offsetTarget
    ){}

    [BBFunc]
    public static Pet CloneUnitPet(
        AttackableUnit unitToClone,
        string buff,
        float duration,
        Vector3 pos,
        float healthBonus,
        float damageBonus,
        bool showMinimapIcon
    ){
        return default!;
    }

    [BBFunc]
    public static void PauseAnimation(
        AttackableUnit unit,
        bool pause
    ){}

    [BBFunc]
    public static void IncMaxHealth(
        AttackableUnit target,
        float delta,
        bool incCurrentHealth
    ){}

    [BBFunc]
    public static void SkipNextAutoAttack(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetCameraPosition(
        Champion owner,
        Vector3 position
    ){}

    [BBFunc]
    public static void StopMove(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetUseSlotSpellCooldownTime(
        int src,
        AttackableUnit owner,
        bool broadcastEvent
    ){}

    [BBFunc(Dest = "OutputAngle")]
    public static float GetOffsetAngle(
        AttackableUnit unit,
        Vector3 offsetPoint
    ){
        return default!;
    }

    [BBFunc]
    public static void SetTriggerUnit(
        AttackableUnit trigger
    ){}

    [BBFunc]
    public static void PopCharacterData(
        AttackableUnit target,
        int ID
    ){}

    [BBFunc]
    public static AttackableUnit SetUnit(
        AttackableUnit src
    ){
        return default!;
    }

    [BBFunc]
    public static void SetSlotSpellIcon(
        int slotNumber,
        SpellSlotType slotType,
        SpellbookType spellbookType,
        ObjAIBase owner,
        int iconIndex
    ){}

    [BBFunc(Dest = "Result")]
    public static bool IsPathable(
        Vector3 destPos
    ){
        return default!;
    }

    [BBFunc]
    public static void SetAutoAcquireTargets(
        AttackableUnit target,
        bool value
    ){}

    [BBFunc]
    public static void RedirectGold(
        AttackableUnit redirectSource,
        AttackableUnit redirectTarget
    ){}

    [BBFunc]
    public static void GetGroundHeight(
        Vector3 queryPos,
        Vector3 groundPos
    ){}

    [BBFunc]
    public static void ShowHealthBar(
        AttackableUnit unit,
        bool show
    ){}

    [BBFunc(Dest = "Result")]
    public static Vector3 GetRandomPointInAreaPosition(
        Vector3 pos,
        float radius,
        float innerRadius
    ){
        return default!;
    }

    [BBFunc]
    public static void FadeInColorFadeEffect(
        int colorRed,
        int colorGreen,
        int colorBlue,
        float fadeTime,
        float maxWeight,
        TeamId specificToTeam
    ){}

    [BBFunc]
    public static float GetSpellBlock(
        AttackableUnit target
    ){
        return default!;
    }

    [BBFunc]
    public static float GetBuffStartTime(
        AttackableUnit target,
        string buffName
    ){
        return default!;
    }

    [BBFunc]
    public static void IncExp(
        AttackableUnit target,
        float delta
    ){}

    [BBFunc]
    public static void StopCurrentOverrideAnimation(
        string animationName,
        AttackableUnit target,
        bool blend
    ){}

    [BBFunc]
    public static void ApplyFear(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc(Dest = "Result")]
    public static Vector3 GetMissilePosFromID(
        SpellMissile targetID
    ){
        return default!;
    }

    [BBFunc(Dest = "Result")]
    public static bool GetIsZombie(
        AttackableUnit unit
    ){
        return default!;
    }

    [BBFunc]
    public static void ForEachPointOnLine(
        Vector3 center,
        Vector3 faceTowardsPos,
        float size,
        float pushForward,
        int iterations,

        [BBSubBlocks("Iterator")]
        Action<Vector3> subBlocks
    ){}

    [BBFunc(Dest = "Position")]
    public static Vector3 ModifyPosition(float x, float y, float z){
        return default!;
    }

    [BBFunc(Dest = "Result")]
    public static bool IsInBrush(AttackableUnit unit){
        return default!;
    }

    [BBFunc]
    public static void MoveToUnit(
        AttackableUnit unit,
        AttackableUnit target,
        float speed,
        float gravity,
        ForceMovementOrdersType movementOrdersType,
        float moveBackBy,
        float maxTrackDistance,
        float idealDistance,
        float timeOverride
    ){}

    [BBFunc]
    public static void ForEachUnitInTargetAreaAddBuff(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        AttackableUnit buffAttacker,
        string buffName,
        BuffAddType buffAddType,
        BuffType buffType,
        int buffMaxStack,
        int buffNumberOfStacks,
        float buffDuration,
        Table buffVarsTable,
        float tickRate,
        bool isHiddenOnClient,
        bool? inclusiveBuffFilter
    ){}

    [BBFunc]
    public static void ForceDead(
        AttackableUnit owner
    ){}

    [BBFunc]
    public static void FadeOutColorFadeEffect(
        float fadeTime,
        TeamId specificToTeam
    ){}

    [BBFunc]
    public static void ApplyNearSight(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc(Dest = "NewPosition")]
    public static Vector3 GetNearestPassablePosition(
        [BBParam("", "VarTable", null, null)] //TODO: Validate "VarTable"
        AttackableUnit owner,
        Vector3 position
    ){
        return default!;
    }

    [BBFunc]
    public static void StopMoveBlock(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void ReincarnateHero(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void ApplyRoot(
        AttackableUnit attacker,
        AttackableUnit target,
        float duration
    ){}

    [BBFunc]
    public static Pet SpawnPet(
        string name,
        string skin,
        string buff,
        string? aiScript,
        float duration,
        Vector3 pos,
        float healthBonus,
        float damageBonus
    ){
        return default!;
    }

    [BBFunc]
    public static void Alert(
        string toAlert,
        object? src
    ){}

    [BBFunc]
    public static string GetDamagingBuffName(){
        return default!;
    }

    [BBFunc]
    public static Vector3 GetCastSpellDragEndPos(){
        return default!;
    }

    [BBFunc]
    public static void InvalidateUnit(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void ForEachVisibleUnitInTargetAreaRandom(
        AttackableUnit attacker,
        Vector3 center,
        float range,
        SpellDataFlags flags,
        int maximumUnitsToPick,
        string buffNameFilter,
        bool inclusiveBuffFilter,

        [BBSubBlocks("Iterator")]
        Action<AttackableUnit> subBlocks
    ){}

    [BBFunc]
    public static void IncSpellLevel(
        ObjAIBase spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType
    ){}

    [BBFunc]
    public static void SetNotTargetableToTeam(
        AttackableUnit target,
        bool toAlly,
        bool toEnemy
    ){}

    [BBFunc]
    public static void SetScaleSkinCoef(
        float scale,
        AttackableUnit owner
    ){}

    [BBFunc]
    public static void ClearPARColorOverride(
        AttackableUnit spellSlotOwner
    ){}

    [BBFunc(Dest = "IncCost")]
    public static float GetPARMultiplicativeCostInc(
        AttackableUnit spellSlotOwner,
        int spellSlot,
        SpellSlotType slotType,
        PrimaryAbilityResourceType PARType
    ){
        return default!;
    }

    [BBFunc]
    public static void ForEachPointAroundCircle(
        Vector3 center,
        float radius,
        int iterations,

        [BBSubBlocks("Iterator")]
        Action<Vector3> subBlocks
    ){}

    [BBFunc]
    public static void ForceRefreshPath(
        AttackableUnit unit
    ){}

    [BBFunc]
    public static void RemoveLinkVisibility(
        AttackableUnit unit1,
        AttackableUnit unit2
    ){}

    [BBFunc]
    public static void UpdateCanCast(
        AttackableUnit target
    ){}

    [BBFunc]
    public static void SetSpellCastRange(
        float newRange
    ){}

    [BBFunc]
    public static void DispellNegativeBuffs(
        AttackableUnit attacker
    ){}

    [BBFunc]
    public static void SetVoiceOverride(
        string overrideSuffix,
        AttackableUnit target
    ){}

    [BBFunc]
    public static void ResetVoiceOverride(
        AttackableUnit target
    ){}

    [BBFunc(Dest = "Result")]
    public static bool TestUnitAttributeFlag(
        AttackableUnit target,
        ExtraAttributeFlag attributeFlag
    ){
        return default!;
    }

    [BBFunc(Dest = "Range")]
    public static float GetCastRange(
        ObjAIBase spellSlotOwner,
        int slotNumber,
        SpellSlotType slotType
    ){
        return default!;
    }

    #region Shields
    [BBFunc]
    public static void ModifyShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false,
        bool noFade = false //TODO: Validate
    ){}
    [BBFunc]
    public static void IncreaseShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    ){}
    [BBFunc]
    public static void ReduceShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    ){}
    [BBFunc]
    public static void RemoveShield(
        AttackableUnit unit,
        float amount = 0,
        bool magicShield = false,
        bool physicalShield = false
    ){}
    #endregion

    [BBFunc]
    public static void CreateItem(
        ObjAIBase unit, //TODO: Move Inventory to AttackableUnit?
        int itemID
    ){}

    [BBFunc]
    public static void DefUpdateAura(
        Vector3 center,
        float range,
        AttackableUnit unitScan,
        string buffName
    ){}

    [BBFunc]
    public static void SetCanCastWhileDisabled(bool canCast){}

    [BBFunc]
    public static float GetGold(
        //TODO:
    ){
        return default!;
    }

    [BBFunc]
    public static void SpellBuffRenew(
        AttackableUnit target,
        string buffName,
        float resetDuration = 0
    ){}
}