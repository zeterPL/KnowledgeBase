using KnowledgeBase.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Dto
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
    }

    public static class TagExtensions
    {
        public static TagDto ToTagDto(this Tag tag)
        {
            return new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
            };
        }
    }
}
