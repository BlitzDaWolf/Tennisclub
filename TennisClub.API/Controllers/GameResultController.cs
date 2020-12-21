using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BAL;
using TennisClub.Data.Model;
using TennisClub.DTO.Game;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameResultController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GameResultController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<GameResultDTO> index()
        {
            IEnumerable<GameResult> gameResult = unitOfWork.GameResultRepository.Get(includeProperties: "Game");
            return mapper.Map<IEnumerable<GameResultDTO>>(gameResult);
        }

        [HttpPost("create")]
        public GameResultCreateDTO createResult(GameResultCreateDTO result)
        {
            unitOfWork.GameResultRepository.Insert(mapper.Map<GameResult>(result));
            unitOfWork.Save();
            return result;
        }

        [HttpPut("update")]
        public GameResultUpdateDTO Update(GameResultUpdateDTO GameResult)
        {
            unitOfWork.GameResultRepository.Update(mapper.Map<GameResult>(GameResult));
            unitOfWork.Save();
            return GameResult;
        }
    }
}
