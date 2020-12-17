using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisClub.BAL;
using TennisClub.Data.Model;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        UnitOfWork unitOfWork;

        public GameController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<Game> index([FromQuery] string includes = "")
        {
            return unitOfWork.GameRepository.Get(includeProperties: includes);
        }

        [HttpPost("/create")]
        public Game Create(Game Game)
        {
            unitOfWork.GameRepository.Insert(Game);
            unitOfWork.Save();
            return Game;
        }

        [HttpPut]
        public Game Update(Game Game)
        {
            unitOfWork.GameRepository.Update(Game);
            unitOfWork.Save();
            return Game;
        }

        [HttpGet("{id}")]
        public Game GetById(int id)
        {
            return unitOfWork.GameRepository.GetByID(id);
        }
    }
}
