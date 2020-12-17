using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BAL;
using TennisClub.Data.Model;
using TennisClub.DTO.Fine;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FineController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<FineDTO> index()
        {
            IEnumerable<Fine> fine = unitOfWork.FineRepository.Get(includeProperties: "Member");
            return mapper.Map<IEnumerable<FineDTO>>(fine);
        }


        [HttpGet("{id}")]
        public FineDTO GetById(int id)
        {
            return mapper.Map<FineDTO>(unitOfWork.FineRepository.GetByID(id));
        }

        [HttpPost("create")]
        public FineCreateDTO Create(FineCreateDTO fine)
        {
            unitOfWork.FineRepository.Insert(mapper.Map<Fine>(fine));
            unitOfWork.Save();
            return fine;
        }

        [HttpPut("update")]
        public FineUpdateDTO Update(FineUpdateDTO fine)
        {
            unitOfWork.FineRepository.Update(mapper.Map<Fine>(fine));
            unitOfWork.Save();
            return fine;
        }
    }
}
