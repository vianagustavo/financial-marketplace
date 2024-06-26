// using System.Data;

// using Dapper;

// using FinancialMarketplace.Application.Contracts;

// namespace FinancialMarketplace.Infrastructure.Database.DapperTypes.TypeHandlers;

// sealed class DapperSqlEnumTypeHandler<TEnum> : SqlMapper.TypeHandler<TEnum>
//     where TEnum : Enum
// {
//     public override void SetValue(IDbDataParameter parameter, TEnum? value)
//         => parameter.Value = value?.ToString() ?? null;

//     public override TEnum Parse(object value)
//         => EnumParser.Parse<TEnum>((string)value);
// }
