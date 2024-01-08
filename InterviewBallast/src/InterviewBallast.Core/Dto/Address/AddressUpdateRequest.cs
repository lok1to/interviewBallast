using InterviewBallast.Domain.Common;

namespace InterviewBallast.Core.Dto.Address
{
    public class AddressUpdateRequest
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
