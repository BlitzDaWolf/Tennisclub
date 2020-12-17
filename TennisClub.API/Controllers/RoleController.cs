using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BAL;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;
using TennisClub.DTO.Role;

namespace TennisClub.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly TennisContext tennisContext;

        public RoleController(UnitOfWork unitOfWork, IMapper mapper, TennisContext tennisContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.tennisContext = tennisContext;
        }

        [HttpGet]
        public IEnumerable<RoleDTO> index()
        {
            //IEnumerable<Role> role = unitOfWork.RoleRepository.Get();
            //var role = unitOfWork.context.Roles.ToList();
            var role = unitOfWork.RoleRepository.GetAll();
            return mapper.Map<IEnumerable<RoleDTO>>(role);
            //return role;
        }

        [HttpGet("{id}")]
        public RoleDTO GetById(int id)
        {
            return mapper.Map<RoleDTO>(unitOfWork.context.Roles.Where(role => role.Id == id));
        }

        [HttpPost("create")]
        public RoleCreateDTO Create(RoleCreateDTO role)
        {
            unitOfWork.RoleRepository.Insert(mapper.Map<Role>(role));
            unitOfWork.Save();
            return role;
        }

        [HttpPut("update")]
        public RoleUpdateDTO Update(RoleUpdateDTO role)
        {
            var mappedRole = mapper.Map<Role>(role);
            var roleToUpdate = unitOfWork.RoleRepository.GetById(mappedRole.Id).ToList();
            roleToUpdate.ForEach(x => x.Name = role.Name);
            //unitOfWork.RoleRepository.Update(mapper.Map<Role>(role));
            unitOfWork.Save();
            return role;
        }
    }
}
