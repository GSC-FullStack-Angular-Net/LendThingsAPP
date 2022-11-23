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
    }
}
