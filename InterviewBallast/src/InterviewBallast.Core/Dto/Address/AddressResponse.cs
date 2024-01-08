using InterviewBallast.Domain.Common;

namespace InterviewBallast.Core.Dto.Address
{
    public class AddressResponse : BaseEntity
    {
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
