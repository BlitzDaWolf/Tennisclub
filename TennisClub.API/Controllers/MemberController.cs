using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using TennisClub.BAL;
using TennisClub.Data.Model;
using TennisClub.DTO.Member;

namespace TennisClub.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemberController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<MemberDTO> index()
        {
            IEnumerable<Member> members = unitOfWork.MemberRepository.Get(includeProperties: "Gender,Roles.Role,Games.League,Games.Results");
            return mapper.Map<IEnumerable<MemberDTO>>(members);
        }

        [HttpGet("{id:int}")]
        public MemberDTO GetById(int? id)
        {
            return mapper.Map<MemberDTO>(unitOfWork.MemberRepository.GetByID(id));
        }

        [HttpDelete("{id:int}")]
        public void Delete(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.MemberRepository.Delete(id);
                    unitOfWork.Save();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Oops! Het is niet mogelijk om wijzigingen op te slaan!");
            }
        }

        [HttpPost("create")]
        public MemberCreateDTO Create(MemberCreateDTO member)
        {
            unitOfWork.MemberRepository.Insert(mapper.Map<Member>(member));
            unitOfWork.Save();
            return member;
        }

        [HttpPut("update")]
        public UpdateMemberDTO Update(UpdateMemberDTO member)
        {
            unitOfWork.MemberRepository.Update(mapper.Map<Member>(member));
            unitOfWork.Save();
            return member;
        }
    }
}
