using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using TennisClub.BAL;
using TennisClub.Data.Model;
using TennisClub.DTO.Game;

namespace TennisClub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly UnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GameController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<GameDTO> index()
        {
            IEnumerable<Game> games = unitOfWork.GameRepository.Get(includeProperties: "Member,League");
            return mapper.Map<IEnumerable<GameDTO>>(games);
        }

        [HttpGet("{id}")]
        public Game GetById(int id)
        {
            return unitOfWork.GameRepository.GetByID(id);
        }

        [HttpPost("create")]
        public Game Create(Game Game)
        {
            unitOfWork.GameRepository.Insert(Game);
            unitOfWork.Save();
            return Game;
        }

        [HttpDelete("{id:int}")]
        public void Delete(int? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.GameRepository.Delete(id);
                    unitOfWork.Save();
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Oops! Het is niet mogelijk om wijzigingen op te slaan!");
            }
        }

        [HttpPut("update")]
        public Game Update(Game Game)
        {
            unitOfWork.GameRepository.Update(Game);
            unitOfWork.Save();
            return Game;
        }
    }
}
