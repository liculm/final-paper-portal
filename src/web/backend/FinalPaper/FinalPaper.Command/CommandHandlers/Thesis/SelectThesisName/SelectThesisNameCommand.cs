using FinalPaper.Domain.Exceptions;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Command.CommandHandlers.Thesis.SelectThesisName;

public sealed record SelectThesisNameCommand(Guid StudentId, string ThesisName) : IRequest<Unit>;

public sealed record SelectThesisNameCommandHandler : IRequestHandler<SelectThesisNameCommand, Unit>
{
    private readonly FinalPaperDBContext context;

    public SelectThesisNameCommandHandler(FinalPaperDBContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(SelectThesisNameCommand request, CancellationToken cancellationToken)
    {
        var thesis = await context.Thesis
            .FirstOrDefaultAsync(x => x.StudentId == request.StudentId, cancellationToken);

        if (thesis is null)
        {
            throw new NotFoundException("Thesis not found!");
        }

        thesis.Name = request.ThesisName;
        
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}