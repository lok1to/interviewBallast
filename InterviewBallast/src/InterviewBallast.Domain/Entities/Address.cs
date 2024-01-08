using InterviewBallast.Domain.Common;

namespace InterviewBallast.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }

        public virtual Player Player { get; set; }
    }
}
