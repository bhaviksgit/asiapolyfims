using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Services.RoleMenuMap
{
    public class RoleMenuMapService : IRoleMenuMapService
    {
        private readonly IRepository<TblRoleMenuMap> _repository;

        //private IMapper _mapper { get; set; }

        public RoleMenuMapService(IRepository<TblRoleMenuMap> repository)
        {
            //_mapper = mapper;
            _repository = repository;
        }

        //private UserAccessRightsModel Map(TblRoles unit)
        //{
        //    return _mapper.Map<RolesModel>(unit);
        //}

        public int Update(TblRoleMenuMap entity)
        {
            //TblRoles obj = _mapper.Map<RolesModel, TblRoles>(entity);
            _repository.Update(entity);
            return entity.RoleId;
        }
    }
}
