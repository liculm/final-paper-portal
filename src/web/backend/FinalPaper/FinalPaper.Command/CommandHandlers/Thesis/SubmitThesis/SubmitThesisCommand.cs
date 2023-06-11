using MediatR;
using FinalPaper.Infrastructure;

namespace FinalPaper.Command.CommandHandlers.Thesis.SubmitThesis;

public sealed record SubmitThesisCommand(int courseId, Guid studentId) : IRequest<Unit>;

public sealed record SubmitThesisCommandHandler : IRequestHandler<SubmitThesisCommand, Unit>
{
    private readonly FinalPaperDBContext context;

    public SubmitThesisCommandHandler(FinalPaperDBContext context)
    {
        this.context = context;
    }

    public async Task<Unit> Handle(SubmitThesisCommand request, CancellationToken cancellationToken)
    {
        context.Add(new Domain.Entities.Thesis(
            request.courseId,
            request.studentId
        ));

        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}