using FluentValidation;

namespace ShopFlow.Application.Catalog.Commands.CriarProduto
{
    public class CriarProdutoValidator : AbstractValidator<CriarProdutoCommand>
    {
        public CriarProdutoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descrição é obrigatório.");
            RuleFor(x => x.Moeda).NotEmpty().WithMessage("Moeda é obrigatório.");
            RuleFor(x => x.Preco).GreaterThan(0).WithMessage("Preço deve ser maior que zero.");
            RuleFor(x => x.Estoque).GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo.");
            
        }

    }
}
