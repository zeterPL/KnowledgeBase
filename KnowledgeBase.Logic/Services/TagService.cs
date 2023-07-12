using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public void Add(TagDto tagDto)
        {
            Tag tag = new Tag
            {
                Id = tagDto.Id,
                Name = tagDto.Name,
            };
            _tagRepository.Add(tag);
        }

        public void Delete(TagDto tagDto)
        {
            Tag tag = new Tag
            {
                Id = tagDto.Id,
                Name = tagDto.Name,
            };
            _tagRepository.Remove(tag);
        }

        public IList<TagDto> GetAll()
        {
            return _tagRepository.GetAll()
                .Select(t => t.ToTagDto()).ToList();
        }

        public TagDto GetById(Guid id)
        {
            return _tagRepository.Get(id).ToTagDto();
        }
    }
}
