using MediatR;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Interfaces;
using ShopFlow.Domain.ValueObjects;

namespace ShopFlow.Application.Catalog.Commands.CriarProduto
{
    public class CriarProdutoCommandHandler : IRequestHandler<CriarProdutoCommand, Guid>
    {
        private readonly IProdutoRepository _produtoRepository;

        public CriarProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Guid> Handle(CriarProdutoCommand command, CancellationToken cancellationToken)
        {
            var preco = new Money(Currency: command.Moeda, Amount: command.Preco);

            var produto = new Produto(command.Nome, command.Descricao, preco, command.Estoque);
       
            await _produtoRepository.AddAsync(produto, cancellationToken);
            return produto.Id;
        }
    }
}
