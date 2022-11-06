using LendThingsAPI.DataAccess;

namespace LendThingsAPI.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LendThingsContext context;
        public CategoryRepository CategoryRepository { get; private set; }

        public LoanRepository LoanRepository { get; private set; }

        public PersonRepository PersonRepository { get; private set; }

        public ThingRepository ThingRepository { get; private set; }

        public UnitOfWork(LendThingsContext context)
        {
            this.context = context;
            CategoryRepository= new CategoryRepository(context);
            LoanRepository = new LoanRepository(context);
            PersonRepository = new PersonRepository(context);
            ThingRepository = new ThingRepository(context);
        }


        public int CompleteAsync()
        {
            return context.SaveChanges();
        }
    }
}