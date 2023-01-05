using FluentValidation;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0); // 0'dan büyük olacak
        RuleFor(command => command.Model.PageCount).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        // Bugünden daha küçük olmalı ve boş olmamalı ^
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
    }
}