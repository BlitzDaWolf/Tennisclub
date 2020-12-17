using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TennisClub.BAL;
using TennisClub.DAL.Context;
using TennisClub.Data.Model;
using TennisClub.DTO.Gender;

namespace TennisClub.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GenderController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public List<GenderDTO> index()
        {
            var res = unitOfWork.GenderRepository.Get();
            return mapper.Map<List<GenderDTO>>(res);
        }

        [HttpGet("{id}")]
        public GenderDTO GetById(int id)
        {
            return mapper.Map<GenderDTO>(unitOfWork.GenderRepository.GetByID(id));
        }
    }
}
