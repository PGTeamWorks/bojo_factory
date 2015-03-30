using AutoMapper;
using BojoFactory.ViewModel;
using Dominio.Entidades;

namespace BojoFactory.AutoMapper
{
    public class ViewModelToDomainMappingProfile :Profile
    {
        public override string ProfileName 
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Cliente, ClienteViewModel>();
        }
    }
}