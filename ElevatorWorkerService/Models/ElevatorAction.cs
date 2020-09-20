namespace ElevatorWorkerService.Models
{
    /// <summary>
    ///     Represents action which do elevator now
    /// </summary>
    public class ElevatorAction
    {
        /// <summary>
        ///     Creates new instance of the elevator action
        /// </summary>
        /// <param name="action"></param>
        public ElevatorAction(ElevatorActionType action)
        {
            Action = action;
        }

        /// <summary>
        ///     Elevator action type
        /// </summary>
        public ElevatorActionType Action { get; }
    }
}