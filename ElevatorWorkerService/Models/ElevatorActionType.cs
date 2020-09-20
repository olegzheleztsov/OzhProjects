namespace ElevatorWorkerService.Models
{
    /// <summary>
    ///     Represent action which do elevator no
    /// </summary>
    public enum ElevatorActionType
    {
        /// <summary>
        ///     Action of start moving
        /// </summary>
        StartMoving,

        /// <summary>
        ///     Action of end moving
        /// </summary>
        EndMoving,

        /// <summary>
        ///     Action of opening elevator's door
        /// </summary>
        Opening,

        /// <summary>
        ///     Elevator wait for people again
        /// </summary>
        RetryOpening
    }
}