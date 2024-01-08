using InterviewBallast.Core.Dto.Address;
using InterviewBallast.Domain.Common;

namespace InterviewBallast.Core.Dto.Player
{
    public class PlayerUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool Active { get; set; }
        public AddressUpdateRequest? Address { get; set; }
    }
}
