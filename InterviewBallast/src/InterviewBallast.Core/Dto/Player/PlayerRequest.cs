using InterviewBallast.Core.Dto.Address;
using InterviewBallast.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Core.Dto.Player
{
    public class PlayerRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public AddressRequest? Address { get; set; }
    }
}
