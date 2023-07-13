using KnowledgeBase.Data.Models;
using KnowledgeBase.Logic.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Logic.Services.Interfaces
{
    public interface ITagService
    {
        public IList<TagDto> GetAll();
        public TagDto GetById(Guid id);
        public void Delete(TagDto tag);
        public Guid Add(TagDto tag);   
        public TagDto? GetTagByName(string name);
        public IList<Tag> GetAllByProjectId(Guid projectId);
    }
}
