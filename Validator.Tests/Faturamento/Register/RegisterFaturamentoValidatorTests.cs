using Barber.Application.UseCases.Faturamento;
using Barber.Communication.Enum;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validator.Tests.Faturamento.Register;
public class RegisterFaturamentoValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new FaturamentoValidator();
        var request = RequestRegisterFaturamentoJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();   
    }



    [Fact]
    public void Error_Service_Type()
    {
        //Arrange
        var validator = new FaturamentoValidator();
        var request = RequestRegisterFaturamentoJsonBuilder.Build();
        request.Servicos = (Servicos)10;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }

    [Fact]
    public void Error_Payment_Type()
    {
        //Arrange
        var validator = new FaturamentoValidator(); 
        var request = RequestRegisterFaturamentoJsonBuilder.Build();
        request.FormaPagamento = (FormaPagamento)100;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    [InlineData(-1)]
    [InlineData(-10000)]
    public void Error_Value(decimal valor)
    {
        //Arrange
        var validator = new FaturamentoValidator();
        var request = RequestRegisterFaturamentoJsonBuilder.Build();
        request.Valor = valor;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
    }

}
