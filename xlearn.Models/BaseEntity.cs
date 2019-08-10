using System;
using System.ComponentModel.DataAnnotations;

namespace xlearn.Models
{
    public class BaseEntity
    {
        [KeyAttribute]
        public UInt64 Id { get; set;}
    }
}