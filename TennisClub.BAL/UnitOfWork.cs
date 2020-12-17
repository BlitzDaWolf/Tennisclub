using System;
using TennisClub.DAL.Context;
using TennisClub.DAL.Repository;

namespace TennisClub.BAL
{
    public class UnitOfWork : IDisposable
    {
        public TennisContext context = new TennisContext();
        private FineRepository fineRepository;
        private GameRepository gameRepository;
        private GameResultRepository gameResultRepository;
        private GenderRepository genderRepository;
        private LeagueRepository leagueRepository;
        private MemberRepository memberRepository;
        private MemberRoleRepository memberRoleRepository;
        private RoleRepository roleRepository;


        public UnitOfWork(TennisContext context)
        {
            this.context = context;
        }

        public FineRepository FineRepository => this.fineRepository ?? new FineRepository(context);
        public GameRepository GameRepository => this.gameRepository ?? new GameRepository(context);
        public GameResultRepository GameResultRepository => this.gameResultRepository ?? new GameResultRepository(context);
        public GenderRepository GenderRepository => this.genderRepository ?? new GenderRepository(context);
        public LeagueRepository LeagueRepository => this.leagueRepository ?? new LeagueRepository(context);
        public MemberRepository MemberRepository => this.memberRepository ?? new MemberRepository(context);
        public MemberRoleRepository MemberRoleRepository => this.memberRoleRepository ?? new MemberRoleRepository(context);
        public RoleRepository RoleRepository => this.roleRepository ?? new RoleRepository(context);


        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose all contextes
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
