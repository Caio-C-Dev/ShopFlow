using FluentAssertions;
using NSubstitute;
using ShopFlow.Application.Catalog.Commands.CriarProduto;
using ShopFlow.Domain.Entities;
using ShopFlow.Domain.Interfaces;

namespace ShopFlow.Tests.Catalog;

public class CriarProdutoCommandHandlerTests
{
    private readonly IProdutoRepository _repository;
    private readonly CriarProdutoCommandHandler _handler;

    public CriarProdutoCommandHandlerTests()
    {
        _repository = Substitute.For<IProdutoRepository>();
        _handler = new CriarProdutoCommandHandler(_repository);
    }

    [Fact]
    public async Task Handle_ComDadosValidos_DeveSalvarProdutoERetornarId()
    {
        // Arrange
        var command = new CriarProdutoCommand(
            "Tênis Nike",
            "Tênis esportivo",
            599.90m,
            50,
            "BRL");

        // Act
        var resultado = await _handler.Handle(command, CancellationToken.None);

        // Assert
        resultado.Should().NotBeEmpty();
        await _repository.Received(1).AddAsync(
            Arg.Is<Produto>(p => p.Nome == "Tênis Nike"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ComPrecoNegativo_DeveLancarExcecao()
    {
        // Arrange
        var command = new CriarProdutoCommand(
            "Tênis Nike",
            "Tênis esportivo",
            -1m,
            50,
            "BRL"
            );

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }
}