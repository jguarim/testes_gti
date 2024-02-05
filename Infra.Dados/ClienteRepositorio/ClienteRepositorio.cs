using Dapper;
using Microsoft.Extensions.Configuration;
using Projeto.Domain.Entidades;
using Projeto.Domain.Interfaces;
using Projeto.Domain.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Infra.Dados.ClienteRepositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string connectionString;

        public ClienteRepositorio(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private static DynamicParameters PreencherPametros(Cliente cliente)
        {
            var dynamicParameters = new DynamicParameters();
            if (cliente.ClienteId > 0)
            {
                dynamicParameters.Add("@IdCliente", cliente.ClienteId, DbType.Int32);
                dynamicParameters.Add("@IdEndereco", cliente.Endereco.EnderecoId, DbType.Int32);
            }

            dynamicParameters.Add("@Cpf", cliente.Cpf, DbType.String);
            dynamicParameters.Add("@Nome", cliente.Nome, DbType.String);
            dynamicParameters.Add("@Rg", cliente.Rg, DbType.String);
            dynamicParameters.Add("@DataExpedicao", cliente.DataExpedicao, DbType.DateTime);
            dynamicParameters.Add("@OrgaoExpedicao", cliente.OrgaoExpedicao, DbType.String);
            dynamicParameters.Add("@Uf", cliente.UF, DbType.String);
            dynamicParameters.Add("@DataNascimento", cliente.DataNascimento, DbType.DateTime);
            dynamicParameters.Add("@Sexo", cliente.Sexo, DbType.String);
            dynamicParameters.Add("@EstadoCivil", cliente.EstadoCivil, DbType.String);

            dynamicParameters.Add("@Cep", cliente.Endereco.Cep, DbType.String);
            dynamicParameters.Add("@Logradouro", cliente.Endereco.Logradouro, DbType.String);
            dynamicParameters.Add("@Numero", cliente.Endereco.Numero, DbType.String);
            dynamicParameters.Add("@Complemento", cliente.Endereco.Complemento, DbType.String);
            dynamicParameters.Add("@Bairro", cliente.Endereco.Bairro, DbType.String);
            dynamicParameters.Add("@Cidade", cliente.Endereco.Cidade, DbType.String);
            dynamicParameters.Add("@UF", cliente.Endereco.UF, DbType.String);
            return dynamicParameters;
        }

        public async Task<int> CreateAsync(Cliente cliente)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();

            var parametros = PreencherPametros(cliente);

            try
            {
                string queryCliente = "INSERT INTO Cliente(Cpf,Nome,Rg,DataExpedicao,OrgaoExpedicao,Uf,DataNascimento,Sexo,EstadoCivil) " +
                                                  "VALUES " +
                                                  "(@Cpf,@Nome,@Rg,@DataExpedicao,@OrgaoExpedicao,@Uf,@DataNascimento,@Sexo,@EstadoCivil); " +
                                                  "SELECT CAST(SCOPE_IDENTITY() AS int);";

                var idCliente = await connection.ExecuteScalarAsync<int>(queryCliente, parametros, transaction);

                if (idCliente > 0)
                {
                    parametros.Add("@IdCliente", idCliente, DbType.Int32);
                    string queryEndereco = "INSERT INTO EnderecoCliente(IdCliente,Cep,Logradouro,Numero,Complemento,Bairro,Cidade,Uf) " +
                                                  "VALUES " +
                                                  "(@IdCliente,@Cep,@Logradouro,@Numero,@Complemento,@Bairro,@Cidade,@Uf); ";

                    await connection.ExecuteAsync(queryEndereco, parametros, transaction);
                }

                transaction.Commit();
                return idCliente;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Cliente> GetByIdAsync(int? id)
        {
            using SqlConnection connection = new(connectionString);

            var result = await connection.QueryAsync<Cliente, EnderecoCliente, Cliente>(
                "SELECT C.ID AS ClienteId, C.CPF,C.NOME,C.RG,C.DataExpedicao,C.OrgaoExpedicao,C.UF,C.DataNascimento,C.SEXO,C.EstadoCivil, " +
                "E.Id AS EnderecoId,E.IdCliente,E.CEP,E.Logradouro,E.Numero,E.Complemento,E.Bairro,E.Cidade,E.Uf " +
                "FROM CLIENTE AS C INNER JOIN EnderecoCliente AS E ON E.IDCLIENTE = C.Id WHERE C.ID = @Id",
                map: (cliente, enderecoCliente) =>
                {
                    cliente.Endereco = enderecoCliente;
                    cliente.Endereco.EnderecoId = cliente.EnderecoId;
                    return cliente;
                },
                splitOn: "ClienteId,IdCliente",
                param: new { @Id = id }
                );

            return result.First();
        }

        public async Task<IEnumerable<Cliente>> GetClientesAsync()
        {
            using SqlConnection connection = new(connectionString);

            return await connection.QueryAsync<Cliente, EnderecoCliente, Cliente>(
                "SELECT C.ID AS ClienteId, C.CPF,C.NOME,C.RG,C.DataExpedicao,C.OrgaoExpedicao,C.UF,C.DataNascimento,C.SEXO,C.EstadoCivil, " +
                "E.Id AS EnderecoId,E.IdCliente,E.CEP,E.Logradouro,E.Numero,E.Complemento,E.Bairro,E.Cidade,E.Uf " +
                "FROM CLIENTE AS C INNER JOIN EnderecoCliente AS E ON E.IDCLIENTE = C.Id", map: (cliente, enderecoCliente) =>
                {
                    cliente.Endereco = enderecoCliente;
                    cliente.Endereco.EnderecoId = cliente.EnderecoId;
                    return cliente;
                }, splitOn: "ClienteId,IdCliente");
        }

        public async Task RemoveAsync(int id)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string query = "DELETE FROM ENDERECOCLIENTE WHERE IDCLIENTE = @IdCliente; " +
                               "DELETE FROM CLIENTE WHERE ID = @IdCliente; ";

                await connection.ExecuteAsync(query, param: new { @IdCliente = id }, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            using SqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            using SqlTransaction transaction = connection.BeginTransaction();
            var parametros = PreencherPametros(cliente);

            try
            {
                string queryCliente = "UPDATE Cliente SET Cpf = @Cpf, Nome = @Nome, Rg = @Rg, DataExpedicao = @DataExpedicao, " +
                                      "OrgaoExpedicao = @OrgaoExpedicao, Uf = @Uf, DataNascimento = @DataNascimento, Sexo = @Sexo, EstadoCivil = @EstadoCivil " +
                                      "WHERE ID = @IdCliente ";

                await connection.ExecuteAsync(queryCliente, parametros, transaction);

                string queryEndereco = "UPDATE EnderecoCliente SET Cep = @Cep, Logradouro = @Logradouro, Numero = @Numero, Complemento = @Complemento, Bairro = @Bairro, Cidade = @Cidade, Uf = @Uf " +
                                       "WHERE ID = @IdEndereco";

                await connection.ExecuteAsync(queryEndereco, parametros, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
