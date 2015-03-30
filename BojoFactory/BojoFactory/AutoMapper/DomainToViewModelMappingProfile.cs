using AutoMapper;
using BojoFactory.ViewModel;
using Dominio.Entidades;

namespace BojoFactory.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {

        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ClienteViewModel, Cliente>();

        }
    }
}