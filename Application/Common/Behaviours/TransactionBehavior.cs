using Application.Common.Interfaces;
using Application.Common.Models.Transaction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Watt.Backend.Manager.PipelineBehavior
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ITransactionalRequest
    {
        private readonly IApplicationDbContext _context;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(IApplicationDbContext context,
                                    ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            TResponse response = default;

            try
            {
                await _context.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation("Begin transaction {Type}", typeof(TRequest).Name);

                    await _context.BeginTransactionAsync();

                    response = await next();

                    await _context.CommitTransactionAsync();

                    _logger.LogInformation("Committed transaction {Type}", typeof(TRequest).Name);
                });

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation("Rollback transaction executed {Type}", typeof(TRequest).Name);

                _context.RollbackTransaction();

                _logger.LogError(e, "{message}:{stackTrace}", e.Message, e.StackTrace);

                throw;
            }
        }
    }
}