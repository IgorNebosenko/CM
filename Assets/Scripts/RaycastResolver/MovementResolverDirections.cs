namespace CM.RaycastResolver
{
    public enum MovementResolverDirections
    {
        None = 0,
        Forward = 1 << 0,
        Backward = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }
}