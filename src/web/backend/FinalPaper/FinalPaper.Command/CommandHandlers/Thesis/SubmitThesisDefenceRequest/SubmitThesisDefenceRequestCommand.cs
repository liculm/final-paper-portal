using MediatR;
using FinalPaper.Domain.Entities;
using FinalPaper.Domain.Enums;
using FinalPaper.Domain.Exceptions;
using FinalPaper.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FinalPaper.Command.CommandHandlers.Thesis.SubmitThesisDefenceRequest;

public sealed record SubmitThesisDefenceRequestCommand(int thesisId) : IRequest<Unit>;

public sealed record SubmitThesisDefenceRequestCommandHandler : IRequestHandler<SubmitThesisDefenceRequestCommand, Unit> {
    private readonly FinalPaperDBContext context;

    public SubmitThesisDefenceRequestCommandHandler(FinalPaperDBContext context) {
        this.context = context;
    }

    public async Task<Unit> Handle(SubmitThesisDefenceRequestCommand request, CancellationToken cancellationToken) {
        var thesis = await context.Thesis
            .FirstOrDefaultAsync(x => x.Id == request.thesisId, cancellationToken);
        
        if (thesis is null) 
            throw new NotFoundException("Thesis not found!");

        thesis.SetThesisStatus(ThesisStatusTypes.PendingDefence.Id);

        context.Add(new ThesisDefence(thesis.Id));
        await context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}