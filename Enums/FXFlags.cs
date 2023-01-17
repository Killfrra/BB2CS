[Flags]
public enum FXFlags
{
    GivenDirection = 1 << 4,
    BindDirection = 1 << 5,
    Unknown3 = GivenDirection + BindDirection,
    Unknown4 = 1 << 6,
    TargetDirection = 1 << 7,
    Unknown6 = BindDirection + TargetDirection,
    Unknown7 = 1 << 8,
    Unknown8 = GivenDirection + Unknown7,
    Unknown9 = BindDirection + TargetDirection + Unknown7,
    Unknown10 = BindDirection + Unknown4 + TargetDirection + Unknown7
}