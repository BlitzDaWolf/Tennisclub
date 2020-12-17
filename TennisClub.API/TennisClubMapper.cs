using AutoMapper;
using TennisClub.Data.Model;
using TennisClub.DTO.Fine;
using TennisClub.DTO.Game;
using TennisClub.DTO.Gender;
using TennisClub.DTO.Member;
using TennisClub.DTO.Role;

namespace TennisClub.API
{
    public class TennisClubMapper : Profile
    {
        public TennisClubMapper()
        {
            {
                CreateMap<Game, GameDTO>().ReverseMap();
                CreateMap<League, LeagueDTO>().ReverseMap();
                CreateMap<GameResult, GameResultDTO>().ReverseMap();
            }

            {
                CreateMap<Gender, GenderDTO>().ReverseMap();
            }

            {
                CreateMap<Member, MemberDTO>().ReverseMap();
                CreateMap<Member, UpdateMemberDTO>().ReverseMap();
                CreateMap<Member, MemberCreateDTO>().ReverseMap();
            }
            {
                CreateMap<Role, RoleDTO>().ReverseMap();
                CreateMap<Role, RoleCreateDTO>().ReverseMap();
                CreateMap<Role, RoleUpdateDTO>().ReverseMap();
                CreateMap<MemberRole, MemberRoleDTO>().ReverseMap();
                CreateMap<MemberRole, MemberRoleCreateDTO>().ReverseMap();
                CreateMap<MemberRole, MemberRoleUpdateDTO>().ReverseMap();
            }

            {
                CreateMap<Fine, FineDTO>().ReverseMap();
                CreateMap<Fine, FineCreateDTO>().ReverseMap();
                CreateMap<Fine, FineUpdateDTO>().ReverseMap();
            }
        }
    }
}
