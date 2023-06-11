using FinalPaper.Domain.Exceptions;
using MediatR;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Command.CommandHandlers.Thesis.ApproveThesis;

public sealed record SetIsApprovedCommand(int ThesisId, bool isApproved) : IRequest<Unit>;

public sealed record SetIsApprovedCommandHandler : IRequestHandler<SetIsApprovedCommand, Unit>
{
    private readonly FinalPaperDBContext context;

    public SetIsApprovedCommandHandler(FinalPaperDBContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(SetIsApprovedCommand request, CancellationToken cancellationToken)
    {
        var thesis = await context.Thesis
            .FirstOrDefaultAsync(x => x.Id == request.ThesisId, cancellationToken);

        if (thesis is null)
        {
            throw new NotFoundException("Thesis not found!");
        }

        thesis.SetIsApprovedStatus(request.isApproved);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}