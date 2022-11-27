using LendThingsAPI.DataAccess;

namespace LendThingsAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LendThingsContext context;
        public ICategoryRepository CategoryRepository { get; private set; }

        public ILoanRepository LoanRepository { get; private set; }

        public IPersonRepository PersonRepository { get; private set; }

        public IThingRepository ThingRepository { get; private set; }

        public UnitOfWork(LendThingsContext context)
        {
            this.context = context;
            CategoryRepository= new CategoryRepository(context);
            LoanRepository = new LoanRepository(context);
            PersonRepository = new PersonRepository(context);
            ThingRepository = new ThingRepository(context);
        }


        async public Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}