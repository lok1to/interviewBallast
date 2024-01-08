using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Core.Dto.Address
{
    public class AddressRequest
    {
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
    }
}
