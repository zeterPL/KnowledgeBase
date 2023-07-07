using AutoMapper;
using KnowledgeBase.Data.Models;
using KnowledgeBase.Data.Repositories.Interfaces;
using KnowledgeBase.Logic.Dto;
using KnowledgeBase.Logic.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace KnowledgeBase.Logic.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
		private readonly IMapper _mapper;

		public ResourceService(IResourceRepository resourceService, IMapper mapper)
        {
            _resourceRepository = resourceService;
			_mapper = mapper;
		}

		public Resource Get(Guid id)
        {
            return _resourceRepository.Get(id);
        }

        public void Add(ResourceDto resourcedto)
        {
			Resource resource = _mapper.Map<Resource>(resourcedto);
			_resourceRepository.Add(resource);
        }

        public void Remove(ResourceDto resourcedto)
        {
			Resource resource = _mapper.Map<Resource>(resourcedto);
			_resourceRepository.Remove(resource);
        }

        public void Deleted(ResourceDto resourcedto)
        {
			Resource resource = Get(_mapper.Map<Resource>(resourcedto).Id);
			_resourceRepository.Deleted(resource);
        }

        public void Update(ResourceDto resourcedto)
        {
			Resource resource = _mapper.Map<Resource>(resourcedto);
			_resourceRepository.Update(resource);
        }

        public IEnumerable<Resource> GetAll()
        {
            IEnumerable<Resource> resourceList = _resourceRepository.GetAll();
            return resourceList.ToList();
        }
    }
}



