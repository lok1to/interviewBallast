using InterviewBallast.Domain.Common;

namespace InterviewBallast.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int AddressId { get; set; }
        public bool Active { get; set; } = true;

        public virtual Address? Address { get; set; }
    }
}
