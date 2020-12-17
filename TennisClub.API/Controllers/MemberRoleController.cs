using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BAL;
using TennisClub.Data.Model;
using TennisClub.DTO.Role;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberRoleController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberRoleController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<MemberRoleDTO> index()
        {
            IEnumerable<MemberRole> memberRole = unitOfWork.MemberRoleRepository.Get(includeProperties: "Role,Member");
            return mapper.Map<IEnumerable<MemberRoleDTO>>(memberRole);
        }

        [HttpPost("create")]
        public MemberRoleCreateDTO Create(MemberRoleCreateDTO memberRole)
        {
            unitOfWork.MemberRoleRepository.Insert(mapper.Map<MemberRole>(memberRole));
            unitOfWork.Save();
            return memberRole;
        }

        [HttpPut("update")]
        public MemberRoleUpdateDTO Update(MemberRoleUpdateDTO member)
        {
            unitOfWork.MemberRoleRepository.Update(mapper.Map<MemberRole>(member));
            unitOfWork.Save();
            return member;
        }
    }
}
