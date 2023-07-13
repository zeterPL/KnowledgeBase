using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;

namespace KnowledgeBase.Logic.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Guid Add(TagDto tagDto)
        {
            Tag tag = new Tag
            {
                Id = tagDto.Id,
                Name = tagDto.Name,
            };
           return _tagRepository.Add(tag);
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

        public TagDto? GetTagByName(string name)
        {
            var tag = _tagRepository.GetAll().Where(t => t.Name == name).SingleOrDefault();
            if (tag is null) return null;
            else return tag.ToTagDto();
        }
    }
}