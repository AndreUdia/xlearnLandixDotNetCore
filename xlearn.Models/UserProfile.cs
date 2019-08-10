using System;

namespace xlearn.Models
{
    public class UserProfile : BaseEntity
    {
        public UInt64 UserId {get; set;}

        public UInt64 ProfileId { get; set;}

        public User User { get; set;}

        public Profile Profile { get; set;}
    }
}