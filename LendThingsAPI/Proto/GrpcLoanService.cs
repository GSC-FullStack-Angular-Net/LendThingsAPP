using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LendThingsAPI.DataAccess;

namespace LendThingsAPI.Proto
{
    public class GrpcLoanService: ProtoLoanService.ProtoLoanServiceBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public GrpcLoanService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }

        public override Task<EndLoanResponse> EndLoan(EndLoanRequest request, ServerCallContext context)
        {
            var loan = uow.LoanRepository.GetById(request.IdLoanToEnd);
            if (loan is null)
            {
                return Task.FromResult(new EndLoanResponse { Success = false, Message = $"The Loan with id {request.IdLoanToEnd} cannot be found." });
            }

            if(loan.ReturnDate is not null)
            {

                return Task.FromResult(new EndLoanResponse { Success = false, Message = $"The Loan with id {loan.Id} has been return on {loan.ReturnDate}" });
            }

            loan.ReturnDate = DateTime.UtcNow;
            uow.CompleteAsync();

            return Task.FromResult(new EndLoanResponse { Success=true, Message= $"The Loan of {loan.Thing.Description} to {loan.Person.Name} with id {loan.Id} made on {loan.Date} has been returned." });
        }

        public override Task<GetAllLoansResponse> GetAllLoans(Empty request, ServerCallContext context)
        {
            var loans = uow.LoanRepository.GetAll();
            IEnumerable<gPRCLoanFull> listgRPCLoans = loans.Select(l =>
            {
                return new gPRCLoanFull
                {
                    Id= l.Id,
                    //Mejor forma de parsear Fechas?
                    Date=Timestamp.FromDateTime(DateTime.SpecifyKind(l.Date, DateTimeKind.Utc)),
                    ReturnDate = l.ReturnDate is not null?Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)l.ReturnDate,DateTimeKind.Utc)):null,
                    Person = new gPRCPersonBase { Id=l.Person.Id, Name=l.Person.Name,Email=l.Person.Email,PhoneNumber=l.Person.PhoneNumber },
                    Thing = new gPRCThingBase { Id=l.Thing.Id,Category=l.Thing.Category.Id, Description=l.Thing.Description },
                };
            });
            var resp = new GetAllLoansResponse();
            resp.LoanList.AddRange(listgRPCLoans);

            return Task.FromResult(resp);
        }
    }
}
